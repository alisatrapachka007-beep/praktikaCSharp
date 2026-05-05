using System;
using System.Windows;
using Day15.Models;

namespace Day15.Views
{
    public partial class AddRecordDialog : Window
    {
        public MedicalRecord NewRecord { get; private set; }
        private MedicalRecord _editingRecord;

        public AddRecordDialog(string patientName, MedicalRecord editingRecord = null)
        {
            InitializeComponent();
            PatientNameText.Text = patientName;
            DatePicker.SelectedDate = DateTime.Now;
            _editingRecord = editingRecord;

            if (editingRecord != null)
            {
                Title = "Редактирование записи";
                DatePicker.SelectedDate = editingRecord.Date;
                DiagnosisTextBox.Text = editingRecord.Diagnosis;
                NotesTextBox.Text = editingRecord.DoctorNotes;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DiagnosisTextBox.Text))
            {
                MessageBox.Show("Введите диагноз.");
                return;
            }

            if (!DatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Выберите дату.");
                return;
            }

            NewRecord = new MedicalRecord
            {
                Id = _editingRecord?.Id ?? 0,
                Date = DatePicker.SelectedDate.Value,
                Diagnosis = DiagnosisTextBox.Text.Trim(),
                DoctorNotes = NotesTextBox.Text.Trim()
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