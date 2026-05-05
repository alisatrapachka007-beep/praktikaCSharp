using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MedicalRecordsApp.Commands;
using MedicalRecordsApp.Models;
using MedicalRecordsApp.Services;
using MedicalRecordsApp.Views;

namespace MedicalRecordsApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private DataService _dataService;
        private Patient _selectedPatient;
        private MedicalRecord _selectedRecord;
        private DateTime? _filterFromDate;
        private DateTime? _filterToDate;

        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<MedicalRecord> CurrentPatientRecords { get; set; }

        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged();
                LoadRecordsForSelectedPatient();

                if (EditRecordCommand is RelayCommand)
                {
                    (EditRecordCommand as RelayCommand).RaiseCanExecuteChanged();
                }
                if (DeleteRecordCommand is RelayCommand)
                {
                    (DeleteRecordCommand as RelayCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public MedicalRecord SelectedRecord
        {
            get { return _selectedRecord; }
            set
            {
                _selectedRecord = value;
                OnPropertyChanged();

                if (EditRecordCommand is RelayCommand)
                {
                    (EditRecordCommand as RelayCommand).RaiseCanExecuteChanged();
                }
                if (DeleteRecordCommand is RelayCommand)
                {
                    (DeleteRecordCommand as RelayCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public DateTime? FilterFromDate
        {
            get { return _filterFromDate; }
            set
            {
                _filterFromDate = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        public DateTime? FilterToDate
        {
            get { return _filterToDate; }
            set
            {
                _filterToDate = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        public ICommand RegisterPatientCommand { get; private set; }
        public ICommand EditRecordCommand { get; private set; }
        public ICommand DeleteRecordCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand SavePatientChangesCommand { get; private set; }
        public ICommand ClearFilterCommand { get; private set; }

        public MainViewModel()
        {
            _dataService = new DataService();
            Patients = _dataService.Patients;
            CurrentPatientRecords = new ObservableCollection<MedicalRecord>();

            RegisterPatientCommand = new RelayCommand(RegisterPatient);
            EditRecordCommand = new RelayCommand(EditRecord, CanEditRecord);
            DeleteRecordCommand = new RelayCommand(DeleteRecord, CanDeleteRecord);
            ExitCommand = new RelayCommand(ExitApplication);
            SavePatientChangesCommand = new RelayCommand(SavePatientChanges);
            ClearFilterCommand = new RelayCommand(ClearFilter);
        }

        private bool CanEditRecord(object parameter)
        {
            return SelectedRecord != null;
        }

        private bool CanDeleteRecord(object parameter)
        {
            return SelectedRecord != null;
        }

        private void LoadRecordsForSelectedPatient()
        {
            CurrentPatientRecords.Clear();
            if (SelectedPatient != null)
            {
                var records = _dataService.GetRecordsForPatient(SelectedPatient.Id);
                foreach (var record in records)
                {
                    CurrentPatientRecords.Add(record);
                }
            }
        }

        private void RegisterPatient(object parameter)
        {
            var dialog = new AddPatientDialog();
            if (dialog.ShowDialog() == true && dialog.NewPatient != null)
            {
                _dataService.AddPatient(dialog.NewPatient);
                Patients.Add(dialog.NewPatient);

                if (!string.IsNullOrWhiteSpace(dialog.InitialDiagnosis))
                {
                    var record = new MedicalRecord();
                    record.PatientId = dialog.NewPatient.Id;
                    record.Date = DateTime.Now;
                    record.Diagnosis = dialog.InitialDiagnosis;
                    record.DoctorNotes = dialog.InitialNotes ?? "";

                    _dataService.AddMedicalRecord(record);

                    if (SelectedPatient != null && SelectedPatient.Id == dialog.NewPatient.Id)
                    {
                        LoadRecordsForSelectedPatient();
                    }
                }
            }
        }

        private void EditRecord(object parameter)
        {
            if (SelectedRecord == null) return;

            var dialog = new EditRecordDialog(SelectedRecord);
            if (dialog.ShowDialog() == true && dialog.UpdatedRecord != null)
            {
                _dataService.UpdateMedicalRecord(dialog.UpdatedRecord);
                LoadRecordsForSelectedPatient();
            }
        }

        private void DeleteRecord(object parameter)
        {
            if (SelectedRecord == null) return;

            var result = MessageBox.Show(
                $"Вы действительно хотите удалить запись от {SelectedRecord.Date:dd.MM.yyyy} с диагнозом \"{SelectedRecord.Diagnosis}\"?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _dataService.DeleteMedicalRecord(SelectedRecord.Id);
                LoadRecordsForSelectedPatient();
            }
        }

        private void SavePatientChanges(object parameter)
        {
            if (SelectedPatient != null)
            {
                _dataService.UpdatePatient(SelectedPatient);
                MessageBox.Show("Данные пациента сохранены!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ApplyFilter()
        {
            var filtered = _dataService.FilterPatientsByLastVisit(FilterFromDate, FilterToDate);
            Patients.Clear();
            foreach (var patient in filtered)
            {
                Patients.Add(patient);
            }
        }

        private void ClearFilter(object parameter)
        {
            FilterFromDate = null;
            FilterToDate = null;
            Patients.Clear();
            foreach (var patient in _dataService.Patients)
            {
                Patients.Add(patient);
            }
        }

        private void ExitApplication(object parameter)
        {
            var result = MessageBox.Show("Выйти из программы?", "Выход",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}