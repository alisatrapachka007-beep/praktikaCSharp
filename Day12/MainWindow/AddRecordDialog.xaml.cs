using System;
using System.Windows;
using MedicalRecordsApp.Models;

namespace MedicalRecordsApp
{
    public partial class AddRecordDialog : Window
    {
        public MedicalRecord NewRecord { get; private set; }
        private int _patientId;

        public AddRecordDialog(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DiagnosisTextBox.Text))
            {
                MessageBox.Show("Введите диагноз.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewRecord = new MedicalRecord
            {
                PatientId = _patientId,
                Date = DateTime.Now,
                Diagnosis = DiagnosisTextBox.Text.Trim(),
                DoctorNotes = DoctorNotesTextBox.Text.Trim()
            };

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}