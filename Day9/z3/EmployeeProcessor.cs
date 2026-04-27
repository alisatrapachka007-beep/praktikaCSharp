class EmployeeProcessor
{
    public List<Employee> FilterBySalary(List<Employee> emps, double min)
    {
        return emps.Where(e => e.Salary >= min).ToList();
    }
}
