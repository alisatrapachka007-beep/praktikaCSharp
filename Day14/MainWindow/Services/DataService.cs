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

            var patient1 = new Patient { Id = 1, FullName = "Иванов Иван Иванович", Age = 45, CurrentDiagnosis = "Гипертония" };
            var patient2 = new Patient { Id = 2, FullName = "Петрова Анна Сергеевна", Age = 32, CurrentDiagnosis = "Грипп" };
            var patient3 = new Patient { Id = 3, FullName = "Сидоров Петр Алексеевич", Age = 58, CurrentDiagnosis = "Диабет" };

            Patients.Add(patient1);
            Patients.Add(patient2);
            Patients.Add(patient3);

            var record1 = new MedicalRecord { Id = 1, PatientId = 1, Date = DateTime.Now.AddDays(-30), Diagnosis = "Гипертония", DoctorNotes = "Назначен препарат А" };
            var record2 = new MedicalRecord { Id = 2, PatientId = 1, Date = DateTime.Now.AddDays(-2), Diagnosis = "Осмотр", DoctorNotes = "Давление в норме" };
            var record3 = new MedicalRecord { Id = 3, PatientId = 2, Date = DateTime.Now.AddDays(-5), Diagnosis = "Грипп", DoctorNotes = "Постельный режим" };
            var record4 = new MedicalRecord { Id = 4, PatientId = 2, Date = DateTime.Now.AddDays(-1), Diagnosis = "Выздоровление", DoctorNotes = "Температура нормализовалась" };
            var record5 = new MedicalRecord { Id = 5, PatientId = 3, Date = DateTime.Now.AddDays(-15), Diagnosis = "Диабет 2 типа", DoctorNotes = "Назначена диета" };

            MedicalRecords.Add(record1);
            MedicalRecords.Add(record2);
            MedicalRecords.Add(record3);
            MedicalRecords.Add(record4);
            MedicalRecords.Add(record5);

            UpdateLastVisitDates();
        }

        private void UpdateLastVisitDates()
        {
            foreach (var patient in Patients)
            {
                var lastRecord = MedicalRecords
                    .Where(r => r.PatientId == patient.Id)
                    .OrderByDescending(r => r.Date)
                    .FirstOrDefault();

                patient.LastVisitDate = lastRecord?.Date;
            }
        }

        public ObservableCollection<MedicalRecord> GetRecordsForPatient(int patientId)
        {
            var records = MedicalRecords
                .Where(r => r.PatientId == patientId)
                .OrderByDescending(r => r.Date)
                .ToList();

            return new ObservableCollection<MedicalRecord>(records);
        }

        public void AddMedicalRecord(MedicalRecord record)
        {
            record.Id = MedicalRecords.Count + 1;
            MedicalRecords.Add(record);
            UpdateLastVisitDates();
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
            UpdateLastVisitDates();
        }

        public void DeleteMedicalRecord(int recordId)
        {
            var record = MedicalRecords.FirstOrDefault(r => r.Id == recordId);
            if (record != null)
            {
                MedicalRecords.Remove(record);
            }
            UpdateLastVisitDates();
        }

        public void UpdatePatient(Patient updatedPatient)
        {
            var existing = Patients.FirstOrDefault(p => p.Id == updatedPatient.Id);
            if (existing != null)
            {
                existing.FullName = updatedPatient.FullName;
                existing.Age = updatedPatient.Age;
                existing.CurrentDiagnosis = updatedPatient.CurrentDiagnosis;
            }
        }

        public ObservableCollection<Patient> FilterPatientsByLastVisit(DateTime? fromDate, DateTime? toDate)
        {
            var filtered = Patients.AsEnumerable();

            if (fromDate.HasValue)
            {
                filtered = filtered.Where(p => p.LastVisitDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                filtered = filtered.Where(p => p.LastVisitDate <= toDate.Value);
            }

            return new ObservableCollection<Patient>(filtered);
        }
    }
}