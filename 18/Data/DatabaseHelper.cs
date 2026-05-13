using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Day15.Models;

namespace Day15.Data
{
    public class DatabaseHelper
    {
        private readonly string _databasePath;
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appFolder = Path.Combine(appData, "Day15");

            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);

            _databasePath = Path.Combine(appFolder, "medical.db");
            _connectionString = $"Data Source={_databasePath};Version=3;";

            InitializeDatabase();
        }

        public string ConnectionString => _connectionString;

        private void InitializeDatabase()
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                // Таблица пациентов
                string createPatients = @"CREATE TABLE IF NOT EXISTS Patients (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName TEXT NOT NULL,
                    Age INTEGER NOT NULL,
                    Diagnosis TEXT,
                    LastVisitDate TEXT)";

                using (var cmd = new SQLiteCommand(createPatients, conn))
                    cmd.ExecuteNonQuery();

                // Таблица медицинских записей
                string createRecords = @"CREATE TABLE IF NOT EXISTS MedicalRecords (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    PatientId INTEGER NOT NULL,
                    Date TEXT NOT NULL,
                    Diagnosis TEXT NOT NULL,
                    DoctorNotes TEXT,
                    FOREIGN KEY(PatientId) REFERENCES Patients(Id) ON DELETE CASCADE)";

                using (var cmd = new SQLiteCommand(createRecords, conn))
                    cmd.ExecuteNonQuery();

                // Таблица пользователей (для авторизации)
                string createUsers = @"CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Login TEXT NOT NULL UNIQUE,
                    PasswordHash TEXT NOT NULL,
                    FullName TEXT NOT NULL,
                    Role INTEGER NOT NULL,
                    PatientId INTEGER,
                    CreatedAt TEXT)";

                using (var cmd = new SQLiteCommand(createUsers, conn))
                    cmd.ExecuteNonQuery();

                // Таблица сообщений чата
                string createMessages = @"CREATE TABLE IF NOT EXISTS ChatMessages (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FromUserId INTEGER NOT NULL,
                    FromUserName TEXT NOT NULL,
                    ToUserId INTEGER NOT NULL,
                    ToUserName TEXT,
                    Message TEXT NOT NULL,
                    Timestamp TEXT NOT NULL,
                    IsRead INTEGER NOT NULL)";

                using (var cmd = new SQLiteCommand(createMessages, conn))
                    cmd.ExecuteNonQuery();

                // Проверка и добавление тестовых данных
                string checkUsers = "SELECT COUNT(*) FROM Users";
                long userCount;
                using (var cmd = new SQLiteCommand(checkUsers, conn))
                    userCount = (long)cmd.ExecuteScalar();

                if (userCount == 0)
                {
                    // Хэш для пароля "123" (SHA256)
                    string passwordHash = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=";

                    // Добавляем врача
                    string insertDoctor = @"INSERT INTO Users (Login, PasswordHash, FullName, Role, CreatedAt) 
                                           VALUES ('doctor', @PasswordHash, 'Трепачко Алиса Павловна', 0, datetime('now'))";
                    using (var cmd = new SQLiteCommand(insertDoctor, conn))
                    {
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        cmd.ExecuteNonQuery();
                    }

                    // Добавляем пациентов
                    string insertPatients = @"INSERT INTO Patients (FullName, Age, Diagnosis) VALUES
                        ('Иванов Иван Иванович', 45, 'Гипертония'),
                        ('Петрова Анна Сергеевна', 32, 'Грипп'),
                        ('Сидоров Петр Алексеевич', 58, 'Диабет')";
                    using (var cmd = new SQLiteCommand(insertPatients, conn))
                        cmd.ExecuteNonQuery();

                    // Добавляем пользователей-пациентов
                    string insertUser1 = @"INSERT INTO Users (Login, PasswordHash, FullName, Role, PatientId, CreatedAt) 
                                           VALUES ('patient1', @PasswordHash, 'Иванов Иван', 1, 1, datetime('now'))";
                    using (var cmd = new SQLiteCommand(insertUser1, conn))
                    {
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        cmd.ExecuteNonQuery();
                    }

                    string insertUser2 = @"INSERT INTO Users (Login, PasswordHash, FullName, Role, PatientId, CreatedAt) 
                                           VALUES ('patient2', @PasswordHash, 'Петрова Анна', 1, 2, datetime('now'))";
                    using (var cmd = new SQLiteCommand(insertUser2, conn))
                    {
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        cmd.ExecuteNonQuery();
                    }

                    // Добавляем тестовые записи
                    string insertRecords = @"INSERT INTO MedicalRecords (PatientId, Date, Diagnosis, DoctorNotes) VALUES
                        (1, datetime('now', '-30 days'), 'Гипертония', 'Назначен препарат А'),
                        (1, datetime('now', '-2 days'), 'Осмотр', 'Давление в норме'),
                        (2, datetime('now', '-5 days'), 'Грипп', 'Постельный режим'),
                        (2, datetime('now', '-1 days'), 'Выздоровление', 'Температура нормализовалась'),
                        (3, datetime('now', '-15 days'), 'Диабет 2 типа', 'Назначена диета')";
                    using (var cmd = new SQLiteCommand(insertRecords, conn))
                        cmd.ExecuteNonQuery();
                }
            }
        }

        // Пациенты
        public List<Patient> GetAllPatients()
        {
            var patients = new List<Patient>();
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, FullName, Age, Diagnosis, LastVisitDate FROM Patients ORDER BY FullName";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var patient = new Patient
                        {
                            Id = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            Age = reader.GetInt32(2),
                            Diagnosis = reader.IsDBNull(3) ? "" : reader.GetString(3)
                        };
                        if (!reader.IsDBNull(4) && DateTime.TryParse(reader.GetString(4), out DateTime lastVisit))
                            patient.LastVisitDate = lastVisit;
                        patients.Add(patient);
                    }
                }
            }
            return patients;
        }

        public Patient GetPatientById(int id)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, FullName, Age, Diagnosis, LastVisitDate FROM Patients WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var patient = new Patient
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Age = reader.GetInt32(2),
                                Diagnosis = reader.IsDBNull(3) ? "" : reader.GetString(3)
                            };
                            if (!reader.IsDBNull(4) && DateTime.TryParse(reader.GetString(4), out DateTime lastVisit))
                                patient.LastVisitDate = lastVisit;
                            return patient;
                        }
                    }
                }
            }
            return null;
        }

        public void AddPatient(Patient patient)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Patients (FullName, Age, Diagnosis, LastVisitDate) VALUES (@Name, @Age, @Diagnosis, @LastVisitDate)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", patient.FullName);
                    cmd.Parameters.AddWithValue("@Age", patient.Age);
                    cmd.Parameters.AddWithValue("@Diagnosis", patient.Diagnosis ?? "");
                    cmd.Parameters.AddWithValue("@LastVisitDate", patient.LastVisitDate?.ToString("yyyy-MM-dd") ?? "");
                    cmd.ExecuteNonQuery();
                    patient.Id = (int)conn.LastInsertRowId;
                }
            }
        }

        public void UpdatePatient(Patient patient)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Patients SET FullName = @Name, Age = @Age, Diagnosis = @Diagnosis, LastVisitDate = @LastVisitDate WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", patient.Id);
                    cmd.Parameters.AddWithValue("@Name", patient.FullName);
                    cmd.Parameters.AddWithValue("@Age", patient.Age);
                    cmd.Parameters.AddWithValue("@Diagnosis", patient.Diagnosis ?? "");
                    cmd.Parameters.AddWithValue("@LastVisitDate", patient.LastVisitDate?.ToString("yyyy-MM-dd") ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletePatient(int id)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Patients WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Медицинские записи
        public List<MedicalRecord> GetRecordsByPatientId(int patientId)
        {
            var records = new List<MedicalRecord>();
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, PatientId, Date, Diagnosis, DoctorNotes FROM MedicalRecords WHERE PatientId = @PatientId ORDER BY Date DESC";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PatientId", patientId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new MedicalRecord
                            {
                                Id = reader.GetInt32(0),
                                PatientId = reader.GetInt32(1),
                                Date = DateTime.Parse(reader.GetString(2)),
                                Diagnosis = reader.GetString(3),
                                DoctorNotes = reader.IsDBNull(4) ? "" : reader.GetString(4)
                            });
                        }
                    }
                }
            }
            return records;
        }

        public void AddRecord(MedicalRecord record)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO MedicalRecords (PatientId, Date, Diagnosis, DoctorNotes) VALUES (@PatientId, @Date, @Diagnosis, @DoctorNotes)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PatientId", record.PatientId);
                    cmd.Parameters.AddWithValue("@Date", record.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Diagnosis", record.Diagnosis);
                    cmd.Parameters.AddWithValue("@DoctorNotes", record.DoctorNotes ?? "");
                    cmd.ExecuteNonQuery();
                    record.Id = (int)conn.LastInsertRowId;
                }
            }
        }

        public void UpdateRecord(MedicalRecord record)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE MedicalRecords SET Date = @Date, Diagnosis = @Diagnosis, DoctorNotes = @DoctorNotes WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", record.Id);
                    cmd.Parameters.AddWithValue("@Date", record.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Diagnosis", record.Diagnosis);
                    cmd.Parameters.AddWithValue("@DoctorNotes", record.DoctorNotes ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRecord(int id)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM MedicalRecords WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateLastVisitDate(int patientId, DateTime? lastVisitDate)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Patients SET LastVisitDate = @LastVisitDate WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", patientId);
                    cmd.Parameters.AddWithValue("@LastVisitDate", lastVisitDate?.ToString("yyyy-MM-dd") ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Пользователи
        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Login, PasswordHash, FullName, Role, PatientId FROM Users";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt32(0),
                            Login = reader.GetString(1),
                            PasswordHash = reader.GetString(2),
                            FullName = reader.GetString(3),
                            Role = (UserRole)reader.GetInt32(4),
                            PatientId = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5)
                        });
                    }
                }
            }
            return users;
        }

        public void SaveUsers(List<User> users)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                foreach (var user in users)
                {
                    string query = @"INSERT OR REPLACE INTO Users (Id, Login, PasswordHash, FullName, Role, PatientId) 
                                    VALUES (@Id, @Login, @PasswordHash, @FullName, @Role, @PatientId)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", user.Id);
                        cmd.Parameters.AddWithValue("@Login", user.Login);
                        cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                        cmd.Parameters.AddWithValue("@FullName", user.FullName);
                        cmd.Parameters.AddWithValue("@Role", (int)user.Role);
                        cmd.Parameters.AddWithValue("@PatientId", user.PatientId ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Сообщения чата
        public List<ChatMessage> GetAllMessages()
        {
            var messages = new List<ChatMessage>();
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, FromUserId, FromUserName, ToUserId, ToUserName, Message, Timestamp, IsRead FROM ChatMessages ORDER BY Timestamp";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        messages.Add(new ChatMessage
                        {
                            Id = reader.GetInt32(0),
                            FromUserId = reader.GetInt32(1),
                            FromUserName = reader.GetString(2),
                            ToUserId = reader.GetInt32(3),
                            ToUserName = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            Message = reader.GetString(5),
                            Timestamp = DateTime.Parse(reader.GetString(6)),
                            IsRead = reader.GetInt32(7) == 1
                        });
                    }
                }
            }
            return messages;
        }

        public void SaveMessages(List<ChatMessage> messages)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                foreach (var message in messages)
                {
                    string checkQuery = "SELECT COUNT(*) FROM ChatMessages WHERE Id = @Id";
                    using (var checkCmd = new SQLiteCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Id", message.Id);
                        long exists = (long)checkCmd.ExecuteScalar();

                        if (exists == 0)
                        {
                            string insertQuery = @"INSERT INTO ChatMessages (Id, FromUserId, FromUserName, ToUserId, ToUserName, Message, Timestamp, IsRead) 
                                                  VALUES (@Id, @FromUserId, @FromUserName, @ToUserId, @ToUserName, @Message, @Timestamp, @IsRead)";
                            using (var insertCmd = new SQLiteCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@Id", message.Id);
                                insertCmd.Parameters.AddWithValue("@FromUserId", message.FromUserId);
                                insertCmd.Parameters.AddWithValue("@FromUserName", message.FromUserName);
                                insertCmd.Parameters.AddWithValue("@ToUserId", message.ToUserId);
                                insertCmd.Parameters.AddWithValue("@ToUserName", message.ToUserName ?? "");
                                insertCmd.Parameters.AddWithValue("@Message", message.Message);
                                insertCmd.Parameters.AddWithValue("@Timestamp", message.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
                                insertCmd.Parameters.AddWithValue("@IsRead", message.IsRead ? 1 : 0);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
    }
}