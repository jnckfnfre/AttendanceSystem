using AttendanceSystem.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Nahyan Munawar 3/20/25
//  Adds controller support to the services container

builder.Services.AddControllers(); // Add services to the container.

builder.Services.AddControllersWithViews(); // Add MVC support
builder.Services.AddSession(); // Add Razor Pages support

// Registers AttendanceDbContext and configures EF Core to use MySQL
// Registers AttendanceDbContext and configures EF Core to use MySQL
builder.Services.AddDbContext<AttendanceDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0)) // or 8.0.13 if your server version is newer
    )
);

// Enables authorization middleware
// Enables authorization middleware
builder.Services.AddAuthorization();

// Adds Swagger for API testing
// Adds Swagger for API testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🌐 Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpMethodOverride(new HttpMethodOverrideOptions
{
    FormFieldName = "_method" // explicitly allow form field
});  // Allows HTTP method override
app.UseHttpsRedirection(); //redirects HTTP requests to HTTPS
app.UseStaticFiles(); // Add static files support
app.UseRouting(); // Add routing support
app.UseSession(); // Add session support

app.UseAuthorization(); // Ensure authorization is active
app.UseAuthorization(); // Ensure authorization is active

app.MapControllerRoute( // Add default MVC route
    name: "default",
    pattern: "{controller=QuizLogin}/{action=Index}/{id?}");
app.MapControllerRoute( // Add default MVC route
    name: "default",
    pattern: "{controller=QuizLogin}/{action=Index}/{id?}");

app.Run(); // ✅ Starts the web server
