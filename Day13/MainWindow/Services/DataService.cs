using System;
using System.Collections.ObjectModel;
using System.Linq;
using MedicalRecordsApp.Models;

namespace MedicalRecordsApp.Services
{
    public class DataService
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<MedicalRecord> MedicalRecords { get; set; }

        public DataService()
        {
            Patients = new ObservableCollection<Patient>();
            MedicalRecords = new ObservableCollection<MedicalRecord>();

            // Тестовые данные
            Patients.Add(new Patient { Id = 1, FullName = "Иванов Иван Иванович", Age = 45 });
            Patients.Add(new Patient { Id = 2, FullName = "Петрова Анна Сергеевна", Age = 32 });

            MedicalRecords.Add(new MedicalRecord
            {
                Id = 1,
                PatientId = 1,
                Date = DateTime.Now.AddDays(-10),
                Diagnosis = "Гипертония",
                DoctorNotes = "Назначен препарат А"
            });
            MedicalRecords.Add(new MedicalRecord
            {
                Id = 2,
                PatientId = 1,
                Date = DateTime.Now.AddDays(-2),
                Diagnosis = "Осмотр",
                DoctorNotes = "Давление в норме"
            });
            MedicalRecords.Add(new MedicalRecord
            {
                Id = 3,
                PatientId = 2,
                Date = DateTime.Now.AddDays(-5),
                Diagnosis = "Грипп",
                DoctorNotes = "Постельный режим"
            });
        }

        public ObservableCollection<MedicalRecord> GetRecordsForPatient(int patientId)
        {
            var records = MedicalRecords.Where(r => r.PatientId == patientId).ToList();
            return new ObservableCollection<MedicalRecord>(records);
        }

        public void AddMedicalRecord(MedicalRecord record)
        {
            record.Id = MedicalRecords.Count + 1;
            MedicalRecords.Add(record);
        }

        public void AddPatient(Patient patient)
        {
            patient.Id = Patients.Count + 1;
            Patients.Add(patient);
        }

        public void UpdateMedicalRecord(MedicalRecord updatedRecord)
        {
            var existing = MedicalRecords.FirstOrDefault(r => r.Id == updatedRecord.Id);
            if (existing != null)
            {
                existing.Date = updatedRecord.Date;
                existing.Diagnosis = updatedRecord.Diagnosis;
                existing.DoctorNotes = updatedRecord.DoctorNotes;
            }
        }

        public void DeleteMedicalRecord(int recordId)
        {
            var record = MedicalRecords.FirstOrDefault(r => r.Id == recordId);
            if (record != null)
            {
                MedicalRecords.Remove(record);
            }
        }
    }
}