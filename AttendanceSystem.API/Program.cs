using AttendanceSystem.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Nahyan Munawar 3/20/25
// Registers AttendanceDbContext in the ASP.NET Core dependency injection system 
// and configures it to use a SQL Server database. This will allow us to create instances
// AttendanceDbContext
builder.Services.AddDbContext<AttendanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
