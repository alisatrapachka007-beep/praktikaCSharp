using System;

namespace MedicalRecordsApp.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public string Diagnosis { get; set; } = "";
        public string DoctorNotes { get; set; } = "";
    }
}