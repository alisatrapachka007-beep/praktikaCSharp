using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Day15.Models;
using Newtonsoft.Json;

namespace Day15.Services
{
    public class DataService
    {
        private readonly string _dataFolder;
        private readonly string _patientsFile;
        private readonly string _recordsFile;
        private readonly string _usersFile;
        private readonly string _messagesFile;

        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<MedicalRecord> MedicalRecords { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<ChatMessage> ChatMessages { get; set; }

        public DataService()
        {
            _dataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MedicalRecordsApp");

            if (!Directory.Exists(_dataFolder))
                Directory.CreateDirectory(_dataFolder);

            _patientsFile = Path.Combine(_dataFolder, "medical.json");
            _recordsFile = Path.Combine(_dataFolder, "records.json");
            _usersFile = Path.Combine(_dataFolder, "users.json");
            _messagesFile = Path.Combine(_dataFolder, "messages.json");

            Patients = new ObservableCollection<Patient>();
            MedicalRecords = new ObservableCollection<MedicalRecord>();
            Users = new ObservableCollection<User>();
            ChatMessages = new ObservableCollection<ChatMessage>();

            LoadData();
        }

        private void LoadData()
        {
            LoadPatients();
            LoadRecords();
            LoadUsers();
            LoadMessages();
        }

        private void LoadPatients()
        {
            if (File.Exists(_patientsFile))
            {
                var json = File.ReadAllText(_patientsFile);
                var patients = JsonConvert.DeserializeObject<ObservableCollection<Patient>>(json);
                if (patients != null)
                {
                    Patients.Clear();
                    foreach (var p in patients)
                        Patients.Add(p);
                }
            }
            else
            {
                CreateTestPatients();
            }
        }

        private void CreateTestPatients()
        {
            var testPatients = new[]
            {
                new Patient { Id = 1, FullName = "Иванов Иван Иванович", Age = 45, Diagnosis = "Гипертония" },
                new Patient { Id = 2, FullName = "Петрова Анна Сергеевна", Age = 32, Diagnosis = "Грипп" },
                new Patient { Id = 3, FullName = "Сидоров Петр Алексеевич", Age = 58, Diagnosis = "Диабет" }
            };
            foreach (var p in testPatients)
                Patients.Add(p);
            SavePatients();
        }

        private void LoadRecords()
        {
            if (File.Exists(_recordsFile))
            {
                var json = File.ReadAllText(_recordsFile);
                var records = JsonConvert.DeserializeObject<ObservableCollection<MedicalRecord>>(json);
                if (records != null)
                {
                    MedicalRecords.Clear();
                    foreach (var r in records)
                        MedicalRecords.Add(r);
                }
            }
            else
            {
                CreateTestRecords();
            }
        }

        private void CreateTestRecords()
        {
            var testRecords = new[]
            {
                new MedicalRecord { Id = 1, PatientId = 1, Date = DateTime.Now.AddDays(-30), Diagnosis = "Гипертония", DoctorNotes = "Назначен препарат А" },
                new MedicalRecord { Id = 2, PatientId = 1, Date = DateTime.Now.AddDays(-2), Diagnosis = "Осмотр", DoctorNotes = "Давление в норме" },
                new MedicalRecord { Id = 3, PatientId = 2, Date = DateTime.Now.AddDays(-5), Diagnosis = "Грипп", DoctorNotes = "Постельный режим" },
                new MedicalRecord { Id = 4, PatientId = 2, Date = DateTime.Now.AddDays(-1), Diagnosis = "Выздоровление", DoctorNotes = "Температура нормализовалась" },
                new MedicalRecord { Id = 5, PatientId = 3, Date = DateTime.Now.AddDays(-15), Diagnosis = "Диабет 2 типа", DoctorNotes = "Назначена диета" }
            };
            foreach (var r in testRecords)
                MedicalRecords.Add(r);
            SaveRecords();
        }

        private void LoadUsers()
        {
            if (File.Exists(_usersFile))
            {
                var json = File.ReadAllText(_usersFile);
                var users = JsonConvert.DeserializeObject<ObservableCollection<User>>(json);
                if (users != null)
                {
                    Users.Clear();
                    foreach (var u in users)
                        Users.Add(u);
                }
            }
            else
            {
                CreateDefaultUsers();
            }
        }

        private void CreateDefaultUsers()
        {
            var defaultUsers = new[]
            {
                new User { Id = 1, Login = "doctor", PasswordHash = ComputeHash("123"),
                           FullName = "Трепачко Алиса Павловна", Role = UserRole.Doctor },
                new User { Id = 2, Login = "patient1", PasswordHash = ComputeHash("123"),
                           FullName = "Иванов Иван", Role = UserRole.Patient, PatientId = 1 },
                new User { Id = 3, Login = "patient2", PasswordHash = ComputeHash("123"),
                           FullName = "Петрова Анна", Role = UserRole.Patient, PatientId = 2 }
            };
            foreach (var u in defaultUsers)
                Users.Add(u);
            SaveUsers();
        }

        private void LoadMessages()
        {
            if (File.Exists(_messagesFile))
            {
                var json = File.ReadAllText(_messagesFile);
                var messages = JsonConvert.DeserializeObject<ObservableCollection<ChatMessage>>(json);
                if (messages != null)
                {
                    ChatMessages.Clear();
                    foreach (var m in messages)
                        ChatMessages.Add(m);
                }
            }
        }

        public void SavePatients()
        {
            var json = JsonConvert.SerializeObject(Patients, Formatting.Indented);
            File.WriteAllText(_patientsFile, json);
        }

        public void SaveRecords()
        {
            var json = JsonConvert.SerializeObject(MedicalRecords, Formatting.Indented);
            File.WriteAllText(_recordsFile, json);
        }

        public void SaveUsers()
        {
            var json = JsonConvert.SerializeObject(Users, Formatting.Indented);
            File.WriteAllText(_usersFile, json);
        }

        public void SaveMessages()
        {
            var json = JsonConvert.SerializeObject(ChatMessages, Formatting.Indented);
            File.WriteAllText(_messagesFile, json);
        }

        public string ComputeHash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public bool VerifyPassword(string password, string hash)
        {
            return ComputeHash(password) == hash;
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
            patient.Id = Patients.Count + 1;
            Patients.Add(patient);
            SavePatients();
        }

        public async Task AddMedicalRecordAsync(MedicalRecord record)
        {
            await Task.Delay(500);
            record.Id = MedicalRecords.Count + 1;
            MedicalRecords.Add(record);
            SaveRecords();
            UpdateLastVisitDates();
        }

        private void UpdateLastVisitDates()
        {
            foreach (var patient in Patients)
            {
                var lastRecord = MedicalRecords.Where(r => r.PatientId == patient.Id).OrderByDescending(r => r.Date).FirstOrDefault();
                if (lastRecord != null)
                    patient.LastVisitDate = lastRecord.Date;
            }
            SavePatients();
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
                SavePatients();
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
                SaveRecords();
                UpdateLastVisitDates();
            }
        }

        public async Task DeleteMedicalRecordAsync(int recordId)
        {
            await Task.Delay(500);
            var record = MedicalRecords.FirstOrDefault(r => r.Id == recordId);
            if (record != null)
            {
                MedicalRecords.Remove(record);
                SaveRecords();
                UpdateLastVisitDates();
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
    }
}