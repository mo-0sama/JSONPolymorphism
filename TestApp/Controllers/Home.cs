namespace TestApp.Controllers;
[ApiController]
[Route("[controller]")]
public class Home : ControllerBase
{
    public Home() { }
    [HttpGet]
    public string Get() => "test";
    [HttpPost]
    public List<IEmployer> Post(List<IEmployer> dto) => dto;
}