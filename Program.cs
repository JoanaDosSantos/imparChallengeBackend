using Microsoft.EntityFrameworkCore;
using imparChallenge.Services.Cars;
using imparChallenge.Models.Contexts;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetSection("ConnectionStrings")["SqlServerConnectionString"];

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ImparTestContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

//Dependencies
builder.Services.AddScoped<ICarService, CarsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors("corsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
