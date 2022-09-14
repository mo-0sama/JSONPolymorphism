namespace TestApp.Models;
public class Manager : IEmployer
{
    public string Discriminator => nameof(Manager);
    public string DepartmentName { get; set; }
    public double Salary { get; set; }
}