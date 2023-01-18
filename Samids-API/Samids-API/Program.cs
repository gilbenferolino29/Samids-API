global using Microsoft.EntityFrameworkCore;
global using static Samids_API.Constants;
using MQTTnet.Client;
using Samids_API.Data;
using Samids_API.MQTT_Utils;
using Samids_API.Services.Impl;
using Samids_API.Services.Interface;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.

builder.Services.AddDbContext<SamidsDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Services used
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IFacultyService, FacultyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IConfigService, ConfigService>();
builder.Services.AddScoped<IAuthService, AuthService>();
var app = builder.Build();
//var server = MQTT_Server.CreateHostBuilder(args).Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.CreateDbIfNotExists();

//MQTT_Server.Start_MqttServer(); // MQTT Server broker // only uncomment if no other broker or no local network connected IoT
await MQTT_Client.StartClient();

Console.WriteLine("Running API");
app.Run();
