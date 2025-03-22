using AttendanceSystem.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Nahyan Munawar 3/20/25
// ✅ Adds controller support to the services container
builder.Services.AddControllers();

// ✅ Registers AttendanceDbContext and configures EF Core to use MySQL
builder.Services.AddDbContext<AttendanceDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0)) // or 8.0.13 if your server version is newer
    )
);

// ✅ Enables authorization middleware
builder.Services.AddAuthorization();

// ✅ Adds Swagger for API testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🌐 Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); // ✅ Ensure authorization is active

app.MapControllers(); // ✅ Maps attribute-routed controllers (like /api/students)

app.Run(); // ✅ Starts the web server
