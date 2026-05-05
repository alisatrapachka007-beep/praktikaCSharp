using System;

namespace MedicalRecordsApp.Models
{
    public class Patient
    {
        private DateTime? _lastVisitDate;

        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string CurrentDiagnosis { get; set; }

        public DateTime? LastVisitDate
        {
            get { return _lastVisitDate; }
            set
            {
                _lastVisitDate = value;
                OnPropertyChanged(nameof(LastVisitInfo));
            }
        }

        public string LastVisitInfo
        {
            get
            {
                if (LastVisitDate == null)
                    return " Нет приемов";
                return $" Последний прием: {LastVisitDate.Value:dd.MM.yyyy}";
            }
        }

        public Patient()
        {
            FullName = "";
            CurrentDiagnosis = "";
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}