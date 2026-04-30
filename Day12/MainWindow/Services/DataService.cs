using MedicalRecordsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MedicalRecordsApp.Services
{
    public class DataService
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<MedicalRecord> MedicalRecords { get; set; }

        public DataService()
        {
            Patients = new ObservableCollection<Patient>
            {
                new Patient { Id = 1, FullName = "Иванов Иван Иванович", Age = 45 },
                new Patient { Id = 2, FullName = "Петрова Анна Сергеевна", Age = 32 }
            };

            MedicalRecords = new ObservableCollection<MedicalRecord>
            {
                new MedicalRecord { Id = 1, PatientId = 1, Date = DateTime.Now.AddDays(-10), Diagnosis = "Гипертония", DoctorNotes = "Назначен препарат А" },
                new MedicalRecord { Id = 2, PatientId = 1, Date = DateTime.Now.AddDays(-2), Diagnosis = "Осмотр", DoctorNotes = "Давление в норме" },
                new MedicalRecord { Id = 3, PatientId = 2, Date = DateTime.Now.AddDays(-5), Diagnosis = "Грипп", DoctorNotes = "Постельный режим" }
            };
        }

        public ObservableCollection<MedicalRecord> GetRecordsForPatient(int patientId)
        {
            return new ObservableCollection<MedicalRecord>(
                MedicalRecords.Where(r => r.PatientId == patientId).ToList()
            );
        }

        public void AddMedicalRecord(MedicalRecord record)
        {
            record.Id = MedicalRecords.Count + 1;
            MedicalRecords.Add(record);
        }
    }
}