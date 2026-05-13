using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Day15.Data;
using Day15.Models;

namespace Day15.Services
{
    public class DataService
    {
        private readonly DatabaseHelper _db;

        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<MedicalRecord> MedicalRecords { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<ChatMessage> ChatMessages { get; set; }

        public DataService()
        {
            _db = new DatabaseHelper();

            Patients = new ObservableCollection<Patient>();
            MedicalRecords = new ObservableCollection<MedicalRecord>();
            Users = new ObservableCollection<User>();
            ChatMessages = new ObservableCollection<ChatMessage>();

            LoadData();
        }

        private void LoadData()
        {
            LoadPatients();
            LoadMedicalRecords();
            LoadUsers();
            LoadChatMessages();
        }

        private void LoadPatients()
        {
            var patients = _db.GetAllPatients();
            Patients.Clear();
            foreach (var p in patients)
                Patients.Add(p);
        }

        private void LoadMedicalRecords()
        {
            MedicalRecords.Clear();
            foreach (var patient in Patients)
            {
                var records = _db.GetRecordsByPatientId(patient.Id);
                foreach (var r in records)
                {
                    if (!MedicalRecords.Any(m => m.Id == r.Id))
                        MedicalRecords.Add(r);
                }
            }
        }

        private void LoadUsers()
        {
            var users = _db.GetAllUsers();
            Users.Clear();
            foreach (var u in users)
                Users.Add(u);
        }

        private void LoadChatMessages()
        {
            var messages = _db.GetAllMessages();
            ChatMessages.Clear();
            foreach (var m in messages)
                ChatMessages.Add(m);
        }

        public async Task<ObservableCollection<Patient>> GetPatientsAsync()
        {
            await Task.Delay(500);
            return Patients;
        }

        public async Task<ObservableCollection<MedicalRecord>> GetPatientHistoryAsync(int patientId)
        {
            await Task.Delay(500);
            var records = MedicalRecords.Where(r => r.PatientId == patientId).OrderByDescending(r => r.Date).ToList();
            return new ObservableCollection<MedicalRecord>(records);
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await Task.Delay(500);
            _db.AddPatient(patient);
            Patients.Add(patient);
        }

        public async Task AddMedicalRecordAsync(MedicalRecord record)
        {
            await Task.Delay(500);
            _db.AddRecord(record);
            MedicalRecords.Add(record);
            UpdateLastVisitDate(record.PatientId);
        }

        private void UpdateLastVisitDate(int patientId)
        {
            var lastRecord = MedicalRecords
                .Where(r => r.PatientId == patientId)
                .OrderByDescending(r => r.Date)
                .FirstOrDefault();

            if (lastRecord != null)
            {
                var patient = Patients.FirstOrDefault(p => p.Id == patientId);
                if (patient != null)
                {
                    patient.LastVisitDate = lastRecord.Date;
                    _db.UpdateLastVisitDate(patientId, lastRecord.Date);
                }
            }
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            await Task.Delay(500);
            _db.UpdatePatient(patient);
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
            _db.UpdateRecord(record);
            var existing = MedicalRecords.FirstOrDefault(r => r.Id == record.Id);
            if (existing != null)
            {
                existing.Date = record.Date;
                existing.Diagnosis = record.Diagnosis;
                existing.DoctorNotes = record.DoctorNotes;
            }
            UpdateLastVisitDate(record.PatientId);
        }

        public async Task DeleteMedicalRecordAsync(int recordId)
        {
            await Task.Delay(500);
            var record = MedicalRecords.FirstOrDefault(r => r.Id == recordId);
            if (record != null)
            {
                _db.DeleteRecord(recordId);
                MedicalRecords.Remove(record);
                UpdateLastVisitDate(record.PatientId);
            }
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

        public void SaveUsers()
        {
            _db.SaveUsers(Users.ToList());
        }

        public void SaveMessages()
        {
            _db.SaveMessages(ChatMessages.ToList());
        }

        public string ComputeHash(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public bool VerifyPassword(string password, string hash)
        {
            return ComputeHash(password) == hash;
        }
    }
}