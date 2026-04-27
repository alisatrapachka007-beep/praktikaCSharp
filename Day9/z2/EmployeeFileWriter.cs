class EmployeeFileWriter
{
    private string filePath;

    public EmployeeFileWriter(string path)
    {
        filePath = path;
    }

    public void WriteEmployeesWithHeader(List<Employee> employees)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("ФИО, Должность,Зарплата");

            foreach (var emp in employees)
            {
                writer.WriteLine($"{emp.Name}, {emp.Position}, {emp.Salary}");
            }
        }

        Console.WriteLine($"Данные записаны в {filePath}");
    }
}
