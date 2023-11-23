using Microsoft.EntityFrameworkCore;
using TrainManagement.Data;
using TrainManagement.Helpers;
using TrainManagement.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();//.AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbContext"));
});

builder.Services.AddScoped<ITrainRepo, TrainRepo>();
builder.Services.AddScoped<ICarReception, CarReceptionManager>();
builder.Services.AddScoped<IReportFileGenerator, ExcelReportManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await PrepareDb.PreparePopulation(app);

app.Run();