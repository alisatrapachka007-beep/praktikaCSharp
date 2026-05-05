using System;
using System.Windows;
using MedicalRecordsApp.Models;

namespace MedicalRecordsApp.Views
{
    public partial class AddPatientDialog : Window
    {
        public Patient NewPatient { get; private set; }
        public string InitialDiagnosis { get; private set; }
        public string InitialNotes { get; private set; }

        public AddPatientDialog()
        {
            InitializeComponent();
            InitialDiagnosis = "";
            InitialNotes = "";
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Введите ФИО пациента.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int age;
            if (!int.TryParse(AgeTextBox.Text, out age) || age <= 0 || age > 150)
            {
                MessageBox.Show("Введите корректный возраст (1-150).", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewPatient = new Patient();
            NewPatient.Id = 0;
            NewPatient.FullName = FullNameTextBox.Text.Trim();
            NewPatient.Age = age;

            InitialDiagnosis = DiagnosisTextBox.Text.Trim();
            InitialNotes = NotesTextBox.Text.Trim();

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