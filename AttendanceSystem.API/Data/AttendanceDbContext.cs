using Microsoft.EntityFrameworkCore;  // Imports EF Core to interact with the database
using AttendanceSystem.API.Models;   // Imports the Attendance model

namespace AttendanceSystem.API.Data 
{
    //Nahyan Munawar 3/20/2025
    
    /// <summary>
    /// This class inherits from DbContext, which provides built-in database operations
    /// like querying, inserting, updating, etc so that we do not need to write raw SQL
    /// </summary>
    public class AttendanceDbContext : DbContext
    {

        // Constructor for `AttendanceDbContext`, which receives configuration settings
        // from Program.cs and passes them to base `DbContext` class so that we can interact with
        // the database correctly
        public AttendanceDbContext(DbContextOptions<AttendanceDbContext> options) 
            : base(options) 
        {
        }

        // This property represents the `Attendances` table in the database.
        // DbSet<Attendance> allows us to query and modify attendance records.
        /// Each `Attendance` object corresponds to a row in the database table.
         public DbSet<Attendance> Attendances { get; set; }
    }
}


