namespace TestApp.Models;
public class Developer : IEmployer
{
    public string Discriminator => nameof(Developer);
    public int Hours { get; set; }
}
