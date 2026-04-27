class EmployeeFileReader
{
    string file;
    public EmployeeFileReader(string path) { file = path; }

    public List<Employee> ReadEmployees()
    {
        var list = new List<Employee>();
        foreach (string line in File.ReadAllLines(file))
        {
            if (line.Contains("ФИО") || line.Contains("Должность")) continue;
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] p = line.Split(',');

            if (p.Length == 3)
                list.Add(new Employee(p[0].Trim(), p[1].Trim(), double.Parse(p[2].Trim())));
        }
        return list;
    }
}