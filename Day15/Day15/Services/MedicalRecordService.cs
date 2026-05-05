using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Day15.Models;

namespace Day15.Services
{
    public class MedicalRecordService
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<MedicalRecord> MedicalRecords { get; set; }

        public MedicalRecordService()
        {
            Patients = new ObservableCollection<Patient>();
            MedicalRecords = new ObservableCollection<MedicalRecord>();
            LoadTestData();
        }

        private void LoadTestData()
        {
            var patient1 = new Patient { Id = 1, FullName = "Иванов Иван Иванович", Age = 45, Diagnosis = "Гипертония" };
            var patient2 = new Patient { Id = 2, FullName = "Петрова Анна Сергеевна", Age = 32, Diagnosis = "Грипп" };
            var patient3 = new Patient { Id = 3, FullName = "Сидоров Петр Алексеевич", Age = 58, Diagnosis = "Диабет" };

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

        public async Task<ObservableCollection<Patient>> GetPatientsAsync()
        {
            await Task.Delay(1000);
            return Patients;
        }

        public async Task<ObservableCollection<MedicalRecord>> GetPatientHistoryAsync(int patientId)
        {
            await Task.Delay(2000);
            var records = MedicalRecords
                .Where(r => r.PatientId == patientId)
                .OrderByDescending(r => r.Date)
                .ToList();
            return new ObservableCollection<MedicalRecord>(records);
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await Task.Delay(500);
            patient.Id = Patients.Count + 1;
            Patients.Add(patient);
        }

        public async Task AddMedicalRecordAsync(MedicalRecord record)
        {
            await Task.Delay(500);
            record.Id = MedicalRecords.Count + 1;
            MedicalRecords.Add(record);
            UpdateLastVisitDates();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            await Task.Delay(500);
            var existing = Patients.FirstOrDefault(p => p.Id == patient.Id);
            if (existing != null)
            {
                existing.FullName = patient.FullName;
                existing.Age = patient.Age;
                existing.Diagnosis = patient.Diagnosis;
            }
        }

        public async Task UpdateMedicalRecordAsync(MedicalRecord record)
        {
            await Task.Delay(500);
            var existing = MedicalRecords.FirstOrDefault(r => r.Id == record.Id);
            if (existing != null)
            {
                existing.Date = record.Date;
                existing.Diagnosis = record.Diagnosis;
                existing.DoctorNotes = record.DoctorNotes;
            }
            UpdateLastVisitDates();
        }

        public async Task DeleteMedicalRecordAsync(int recordId)
        {
            await Task.Delay(500);
            var record = MedicalRecords.FirstOrDefault(r => r.Id == recordId);
            if (record != null)
            {
                MedicalRecords.Remove(record);
            }
            UpdateLastVisitDates();
        }

        public ObservableCollection<Patient> FilterPatients(DateTime? fromDate, DateTime? toDate)
        {
            var filtered = Patients.AsEnumerable();
            if (fromDate.HasValue)
                filtered = filtered.Where(p => p.LastVisitDate >= fromDate.Value);
            if (toDate.HasValue)
                filtered = filtered.Where(p => p.LastVisitDate <= toDate.Value);
            return new ObservableCollection<Patient>(filtered);
        }
    }
}