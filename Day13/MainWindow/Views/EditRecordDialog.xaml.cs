using System;
using System.Windows;
using MedicalRecordsApp.Models;

namespace MedicalRecordsApp.Views
{
    public partial class EditRecordDialog : Window
    {
        public MedicalRecord UpdatedRecord { get; private set; }
        private MedicalRecord _originalRecord;

        public EditRecordDialog(MedicalRecord record)
        {
            InitializeComponent();
            _originalRecord = record;

            DatePicker.SelectedDate = record.Date;
            DiagnosisTextBox.Text = record.Diagnosis;
            NotesTextBox.Text = record.DoctorNotes;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DiagnosisTextBox.Text))
            {
                MessageBox.Show("Введите диагноз.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UpdatedRecord = new MedicalRecord();
            UpdatedRecord.Id = _originalRecord.Id;
            UpdatedRecord.PatientId = _originalRecord.PatientId;
            UpdatedRecord.Date = DatePicker.SelectedDate ?? DateTime.Now;
            UpdatedRecord.Diagnosis = DiagnosisTextBox.Text.Trim();
            UpdatedRecord.DoctorNotes = NotesTextBox.Text.Trim();

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