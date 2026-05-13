using System;

namespace Day15.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? PatientId { get; set; }
    }

    public enum UserRole
    {
        Doctor = 0,
        Patient = 1
    }
}