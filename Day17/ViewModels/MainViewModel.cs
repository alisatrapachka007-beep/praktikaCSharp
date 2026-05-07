using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using Day15.Commands;
using Day15.Models;
using Day15.Services;
using Day15.Views;

namespace Day15.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DataService _service;
        private readonly AuthService _authService;
        private readonly NotificationService _notificationService;
        private Patient _selectedPatient;
        private MedicalRecord _selectedRecord;
        private DateTime? _filterFromDate;
        private DateTime? _filterToDate;
        private bool _isLoading;
        private bool _isLoadingHistory;
        private string _loadingMessage;

        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<MedicalRecord> CurrentPatientHistory { get; set; }

        // Событие для открытия чата
        public event Action OpenChatRequested;

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public bool IsLoadingHistory
        {
            get => _isLoadingHistory;
            set { _isLoadingHistory = value; OnPropertyChanged(); }
        }

        public string LoadingMessage
        {
            get => _loadingMessage;
            set { _loadingMessage = value; OnPropertyChanged(); }
        }

        public Patient SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                OnPropertyChanged();
                if (value != null)
                    _ = LoadPatientHistoryAsync();
                (AddRecordCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public MedicalRecord SelectedRecord
        {
            get => _selectedRecord;
            set
            {
                _selectedRecord = value;
                OnPropertyChanged();
                (EditRecordCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteRecordCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public DateTime? FilterFromDate
        {
            get => _filterFromDate;
            set { _filterFromDate = value; OnPropertyChanged(); ApplyFilter(); }
        }

        public DateTime? FilterToDate
        {
            get => _filterToDate;
            set { _filterToDate = value; OnPropertyChanged(); ApplyFilter(); }
        }

        public bool IsDoctor => _authService.IsDoctor;
        public string CurrentUserName => _authService.CurrentUser?.FullName ?? "";

        public ICommand AddPatientCommand { get; private set; }
        public ICommand AddRecordCommand { get; private set; }
        public ICommand EditRecordCommand { get; private set; }
        public ICommand DeleteRecordCommand { get; private set; }
        public ICommand SavePatientChangesCommand { get; private set; }
        public ICommand ClearFilterCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        // Новая команда для открытия чата
        public ICommand OpenChatCommand { get; private set; }

        public MainViewModel(DataService service, AuthService authService, NotificationService notificationService)
        {
            _service = service;
            _authService = authService;
            _notificationService = notificationService;

            Patients = new ObservableCollection<Patient>();
            CurrentPatientHistory = new ObservableCollection<MedicalRecord>();

            AddPatientCommand = new RelayCommand(async (p) => await AddPatientAsync(), (p) => IsDoctor);
            AddRecordCommand = new RelayCommand(async (p) => await AddRecordAsync(), (p) => SelectedPatient != null && IsDoctor);
            EditRecordCommand = new RelayCommand(async (p) => await EditRecordAsync(), (p) => SelectedRecord != null && IsDoctor);
            DeleteRecordCommand = new RelayCommand(async (p) => await DeleteRecordAsync(), (p) => SelectedRecord != null && IsDoctor);
            SavePatientChangesCommand = new RelayCommand(async (p) => await SavePatientChangesAsync(), (p) => SelectedPatient != null && IsDoctor);
            ClearFilterCommand = new RelayCommand(ClearFilter);
            RefreshCommand = new RelayCommand(async (p) => await LoadPatientsAsync());
            ExitCommand = new RelayCommand(ExitApplication);

            // Инициализация команды для открытия чата
            OpenChatCommand = new RelayCommand(OpenChat);

            _ = LoadPatientsAsync();
        }

        private async Task LoadPatientsAsync()
        {
            IsLoading = true;
            LoadingMessage = "Загрузка списка пациентов...";
            var patients = await _service.GetPatientsAsync();
            Patients.Clear();
            foreach (var patient in patients)
                Patients.Add(patient);
            IsLoading = false;
            LoadingMessage = "";
        }

        private async Task LoadPatientHistoryAsync()
        {
            if (SelectedPatient == null) return;
            IsLoadingHistory = true;
            LoadingMessage = "Загрузка истории болезни...";
            var history = await _service.GetPatientHistoryAsync(SelectedPatient.Id);
            CurrentPatientHistory.Clear();
            foreach (var record in history)
                CurrentPatientHistory.Add(record);
            IsLoadingHistory = false;
            LoadingMessage = "";
        }

        private async Task AddPatientAsync()
        {
            if (!IsDoctor)
            {
                MessageBox.Show("Только врачи могут добавлять пациентов!", "Доступ запрещен",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = new AddPatientDialog();
            if (dialog.ShowDialog() == true && dialog.NewPatient != null)
            {
                IsLoading = true;
                LoadingMessage = "Сохранение данных пациента...";
                await _service.AddPatientAsync(dialog.NewPatient);

                if (!string.IsNullOrWhiteSpace(dialog.InitialDiagnosis))
                {
                    var record = new MedicalRecord
                    {
                        PatientId = dialog.NewPatient.Id,
                        Date = DateTime.Now,
                        Diagnosis = dialog.InitialDiagnosis,
                        DoctorNotes = dialog.InitialNotes ?? ""
                    };
                    await _service.AddMedicalRecordAsync(record);

                    _notificationService.SendNotification($"Новая запись: пациент {dialog.NewPatient.FullName} добавлен в систему");
                }

                await LoadPatientsAsync();
                IsLoading = false;
                MessageBox.Show("Пациент добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async Task AddRecordAsync()
        {
            if (!IsDoctor)
            {
                MessageBox.Show("Только врачи могут добавлять записи!", "Доступ запрещен",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedPatient == null) return;
            var dialog = new AddRecordDialog(SelectedPatient.FullName);
            if (dialog.ShowDialog() == true && dialog.NewRecord != null)
            {
                IsLoadingHistory = true;
                LoadingMessage = "Добавление записи...";
                dialog.NewRecord.PatientId = SelectedPatient.Id;
                dialog.NewRecord.Date = DateTime.Now;
                await _service.AddMedicalRecordAsync(dialog.NewRecord);
                await LoadPatientHistoryAsync();
                await LoadPatientsAsync();
                IsLoadingHistory = false;

                _notificationService.SendNotification($"Новая медицинская запись для {SelectedPatient.FullName}");

                MessageBox.Show("Запись добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async Task EditRecordAsync()
        {
            if (SelectedRecord == null) return;
            var dialog = new AddRecordDialog(SelectedPatient.FullName, SelectedRecord);
            if (dialog.ShowDialog() == true && dialog.NewRecord != null)
            {
                IsLoadingHistory = true;
                LoadingMessage = "Сохранение изменений...";
                dialog.NewRecord.Id = SelectedRecord.Id;
                dialog.NewRecord.PatientId = SelectedPatient.Id;
                await _service.UpdateMedicalRecordAsync(dialog.NewRecord);
                await LoadPatientHistoryAsync();
                await LoadPatientsAsync();
                IsLoadingHistory = false;

                _notificationService.SendNotification($"Изменена запись для {SelectedPatient.FullName}");

                MessageBox.Show("Запись обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async Task DeleteRecordAsync()
        {
            if (SelectedRecord == null) return;
            var result = MessageBox.Show($"Удалить запись от {SelectedRecord.Date:dd.MM.yyyy}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                IsLoadingHistory = true;
                LoadingMessage = "Удаление записи...";
                await _service.DeleteMedicalRecordAsync(SelectedRecord.Id);
                await LoadPatientHistoryAsync();
                await LoadPatientsAsync();
                IsLoadingHistory = false;

                _notificationService.SendNotification($"Удалена запись для {SelectedPatient.FullName}");
            }
        }

        private async Task SavePatientChangesAsync()
        {
            if (SelectedPatient == null) return;
            IsLoading = true;
            LoadingMessage = "Сохранение данных...";
            await _service.UpdatePatientAsync(SelectedPatient);
            IsLoading = false;

            _notificationService.SendNotification($"Обновлены данные пациента {SelectedPatient.FullName}");

            MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ApplyFilter()
        {
            var filtered = _service.FilterPatients(FilterFromDate, FilterToDate);
            Patients.Clear();
            foreach (var patient in filtered)
                Patients.Add(patient);
        }

        private void ClearFilter(object parameter)
        {
            FilterFromDate = null;
            FilterToDate = null;
            Patients.Clear();
            foreach (var patient in _service.Patients)
                Patients.Add(patient);
        }

        private void ExitApplication(object parameter)
        {
            var result = MessageBox.Show("Выйти из программы?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        // Метод для открытия чата
        private void OpenChat(object parameter)
        {
            OpenChatRequested?.Invoke();
        }
    }
}