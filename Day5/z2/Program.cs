using System;

class Pharmacy
{
    public string Name;
    public Pharmacy(string name) { Name = name; }
    public void Supply() { Console.WriteLine($"Аптека {Name} поставляет лекарства"); }
}

class Doctor
{
    public string Name;
    public Doctor(string name) { Name = name; }
    public void Treat() { Console.WriteLine($"Доктор {Name} лечит"); }
}

class MedicalRecord
{
    public MedicalRecord() { Console.WriteLine("Медкарта создана"); }
}

class Hospital
{
    public Doctor[] Doctors;
    public MedicalRecord Record;
    public Pharmacy Pharmacy;

    public Hospital(Doctor[] doctors, Pharmacy pharmacy)
    {
        Doctors = doctors;
        Pharmacy = pharmacy;
        Record = new MedicalRecord();
    }

    public void TreatPatients()
    {
        foreach (var d in Doctors) d.Treat();
        Pharmacy.Supply();
        Console.WriteLine("Пациенты вылечены!");
    }
}

class Program
{
    static void Main()
    {
        Doctor doctor1 = new Doctor("Иванов");
        Doctor doctor2 = new Doctor("Петрова");

        Pharmacy pharmacy1 = new Pharmacy("Здоровье");
        Pharmacy pharmacy2 = new Pharmacy("Фармленд");

        Hospital[] hospitals = new Hospital[]
        {
            new Hospital(new Doctor[] { doctor1, doctor2 }, pharmacy1),
            new Hospital(new Doctor[] { doctor1 }, pharmacy2)
        };

        foreach (var h in hospitals) h.TreatPatients();
    }
}