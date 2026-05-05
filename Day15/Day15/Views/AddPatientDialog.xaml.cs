using System.Windows;
using Day15.Models;

namespace Day15.Views
{
    public partial class AddPatientDialog : Window
    {
        public Patient NewPatient { get; private set; }
        public string InitialDiagnosis { get; private set; }
        public string InitialNotes { get; private set; }

        public AddPatientDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Введите ФИО пациента.");
                return;
            }

            if (!int.TryParse(AgeTextBox.Text, out int age) || age <= 0 || age > 150)
            {
                MessageBox.Show("Введите корректный возраст.");
                return;
            }

            NewPatient = new Patient
            {
                FullName = FullNameTextBox.Text.Trim(),
                Age = age,
                Diagnosis = DiagnosisTextBox.Text.Trim()
            };

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