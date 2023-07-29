using BusinessObjects;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<CompanyProject>("CompanyProjects");
modelBuilder.EntitySet<Department>("Departments");
modelBuilder.EntitySet<Employee>("Employees");
modelBuilder.EntitySet<ParticipatingProject>("ParticipatingProjects");

builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null)
    .AddRouteComponents("odata", modelBuilder.GetEdmModel()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseODataBatching();

app.UseAuthorization();

app.MapControllers();

app.Run();
