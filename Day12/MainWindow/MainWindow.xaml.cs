using System.Windows;
using MedicalRecordsApp.Models;
using MedicalRecordsApp.Services;

namespace MedicalRecordsApp
{
    public partial class MainWindow : Window
    {
        private DataService _dataService;
        private Patient _selectedPatient;

        public MainWindow()
        {
            InitializeComponent();
            _dataService = new DataService();
            PatientsListBox.ItemsSource = _dataService.Patients;
        }

        private void PatientsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedPatient = PatientsListBox.SelectedItem as Patient;
            if (_selectedPatient != null)
            {
                var records = _dataService.GetRecordsForPatient(_selectedPatient.Id);
                HistoryDataGrid.ItemsSource = records;
            }
            else
            {
                HistoryDataGrid.ItemsSource = null;
            }
        }

        private void AddRecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedPatient == null)
            {
                MessageBox.Show("Сначала выберите пациента из списка.", "Внимание",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = new AddRecordDialog(_selectedPatient.Id);
            if (dialog.ShowDialog() == true)
            {
                _dataService.AddMedicalRecord(dialog.NewRecord);

                var updatedRecords = _dataService.GetRecordsForPatient(_selectedPatient.Id);
                HistoryDataGrid.ItemsSource = updatedRecords;
            }
        }
    }
}