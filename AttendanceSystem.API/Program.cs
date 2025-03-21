using AttendanceSystem.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Nahyan Munawar 3/20/25
// Registers AttendanceDbContext in the ASP.NET Core dependency injection system 
// and configures it to use a MySQL Server database. This will allow us to create instances of
// AttendanceDbContext
builder.Services.AddDbContext<AttendanceDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 5, 9))
    ));
