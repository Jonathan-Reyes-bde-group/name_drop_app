using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProjectNameDrop.Helpers;
using ProjectNameDrop.Services;
using ProjectNameDrop.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
// Configuration setup
builder.Configuration.AddJsonFile("appsettings.json");
// Add logger
var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Default",
        policy => policy
            .WithOrigins("http://localhost:4200") // Angular dev server
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IGenderizeServices, GenderizeServices>();
builder.Services.AddScoped<INationalizeServices, NationalizeServices>();
builder.Services.AddScoped<IAgifyServices, AgifyServices>();

builder.Services.AddHttpClient("Genderize", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Genderize:BaseUrl"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30); // Set a timeout for the request
});

builder.Services.AddHttpClient("Nationalize", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Nationalize:BaseUrl"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30); // Set a timeout for the request
});

builder.Services.AddHttpClient("Agify", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Agify:BaseUrl"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30); // Set a timeout for the request
});

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ApiKeyClaimsInjectionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


RetryPolicyHelper.Initialize(loggerFactory);
app.UseHttpsRedirection();
app.UseCors("Default");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
