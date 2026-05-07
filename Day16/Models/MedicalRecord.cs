using System;

namespace Day15.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public string Diagnosis { get; set; }
        public string DoctorNotes { get; set; }

        public MedicalRecord()
        {
            Diagnosis = "";
            DoctorNotes = "";
        }
    }
}