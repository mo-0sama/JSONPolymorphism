using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options =>
               options.JsonSerializerOptions.AddDiscriminatorConverterForHierarchy<IEmployer>());
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();