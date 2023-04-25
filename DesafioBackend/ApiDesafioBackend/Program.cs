using DesafioBackend;
using DesafioBackend.DataBase;
using DesafioBackend.Indicators;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using MediatR;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = new SqliteConnection("Filename=:memory:");
connection.Open();
builder.Services.AddDbContext<DesafioBackendContext>(o => o.UseSqlite(connection)); // using in memory Sqlite server

//builder.Services.AddDbContext<DesafioBackendContext>(optionsBuilder =>
//{
//    optionsBuilder.UseSqlServer("Data Source=localhost;Database=cqrs-local;User ID=SA;Password=qtbcu@2018;MultipleActiveResultSets=True;TrustServerCertificate=True");
//});



builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations()); ;

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DesafioBackendEntryPoint).Assembly));


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

// *** Adding initial data to the memory DB
static void AddInitialData(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<DesafioBackendContext>();


    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    var indicator1 = new Indicator(name: "indicator sum", resultType: EnumResult.Sum);
    indicator1.AddDataCollectionPoint(DateTime.Today.AddDays(-5), 100);
    indicator1.AddDataCollectionPoint(DateTime.Today.AddDays(-4), 50);

    var indicator2 = new Indicator(name: "indicator average", resultType: EnumResult.Average);
    indicator2.AddDataCollectionPoint(DateTime.Today.AddDays(-2), 11.6);
    indicator2.AddDataCollectionPoint(DateTime.Today.AddDays(-3), 5.9);

    db.Indicators.Add(indicator1);
    db.Indicators.Add(indicator2);

    db.SaveChanges();
}

AddInitialData(app);

// *** initial data added to the memory DB

app.Run();
