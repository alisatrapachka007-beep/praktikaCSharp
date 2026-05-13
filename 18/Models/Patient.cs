using System;
using System.ComponentModel;

namespace Day15.Models
{
    public class Patient : INotifyPropertyChanged
    {
        private string _fullName;
        private int _age;
        private string _diagnosis;
        private DateTime? _lastVisitDate;

        public int Id { get; set; }

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public string Diagnosis
        {
            get => _diagnosis;
            set
            {
                _diagnosis = value;
                OnPropertyChanged(nameof(Diagnosis));
            }
        }

        public DateTime? LastVisitDate
        {
            get => _lastVisitDate;
            set
            {
                _lastVisitDate = value;
                OnPropertyChanged(nameof(LastVisitDate));
                OnPropertyChanged(nameof(LastVisitInfo));
            }
        }

        public string LastVisitInfo => LastVisitDate == null
            ? "Нет приемов"
            : $"Последний прием: {LastVisitDate.Value:dd.MM.yyyy}";

        public Patient()
        {
            FullName = "";
            Diagnosis = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}