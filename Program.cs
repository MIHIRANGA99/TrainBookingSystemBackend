using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
using TrainBookingBackend.Models.DBConfig;
using TrainBookingBackend.Models.Interfaces;
using TrainBookingBackend.Services;
using TrainBookingBackend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// User Service
builder.Services.Configure<UserDBSettings>(builder.Configuration.GetSection("UserDBSettings"));
builder.Services.AddSingleton<IUserDBSettings>(sp =>
    sp.GetRequiredService<IOptions<UserDBSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(dbSettings =>
    new MongoClient(builder.Configuration.GetValue<string>("UserDBSettings:ConnectionString")));

builder.Services.AddScoped<IUserService, UserService>();

// Reservation Service
builder.Services.Configure<SystemDBSettings>(builder.Configuration.GetSection("ReservationDBSettings"));
builder.Services.AddSingleton<ISystemDBSettings>(sp =>
    sp.GetRequiredService<IOptions<SystemDBSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(dbSettings =>
    new MongoClient(builder.Configuration.GetValue<string>("ReservationDBSettings:ConnectionString")));

builder.Services.AddScoped<IReservationService, ReservationService>();

// Train Service
builder.Services.Configure<TrainDBSettings>(builder.Configuration.GetSection("TrainDBSettings"));
builder.Services.AddSingleton<ITrainDBSettings>(sp =>
    sp.GetRequiredService<IOptions<TrainDBSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(dbSettings =>
    new MongoClient(builder.Configuration.GetValue<string>("TrainDBSettings:TrainConnectionString")));

builder.Services.AddScoped<ITrainService, TrainService>();

// Schedule Service
builder.Services.Configure<ScheduleDBSettings>(builder.Configuration.GetSection("ScheduleDBSettings"));
builder.Services.AddSingleton<IScheduleDBSettings>(sp =>
    sp.GetRequiredService<IOptions<ScheduleDBSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(dbSettings =>
    new MongoClient(builder.Configuration.GetValue<string>("ScheduleDBSettings:ScheduleConnectionString")));

builder.Services.AddScoped<IScheduleService, ScheduleService>();

builder.Services.AddControllers();

//CORS
builder.Services.AddCors(options =>
 {
     options.AddPolicy("AllowLocalhost3000",
         builder => builder.WithOrigins("http://localhost:3000")
             .AllowAnyMethod()
             .AllowAnyHeader());
 });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Authentication Scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost3000");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
