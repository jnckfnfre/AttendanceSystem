using Microsoft.EntityFrameworkCore;  // Imports EF Core to interact with the database
using AttendanceSystem.API.Models;

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

        // DbSet properties: each represents a table
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<ClassSession> ClassSessions { get; set; }
        public DbSet<AttendedBy> AttendedBy { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<QuestionPool> QuestionPools { get; set; }
        public DbSet<Question> Questions { get; set; }

        // this method is needed to implement our composite key which exists in ClassSession
        // and foreign keys that map to it in AttendedBy and Submission
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Student entity
            modelBuilder.Entity<Student>()
                .ToTable("Student")
                .HasKey(s => s.UtdId);

            modelBuilder.Entity<Student>()
                .Property(s => s.UtdId)
                .HasColumnName("UTD_ID");

            modelBuilder.Entity<Student>()
                .Property(s => s.FirstName)
                .HasColumnName("FIRST_NAME");

            modelBuilder.Entity<Student>()
                .Property(s => s.LastName)
                .HasColumnName("LAST_NAME");

            modelBuilder.Entity<Student>()
                .Property(s => s.Net_Id)
                .HasColumnName("NET_ID");

            // Composite Primary Key for class_session
            modelBuilder.Entity<ClassSession>()
                .HasKey(cs => new { cs.SessionDate, cs.Course_Id });

            // Composite Foreign Key for Attended_By -> class_session
            modelBuilder.Entity<AttendedBy>()
                .HasOne(a => a.ClassSession)
                .WithMany(cs => cs.AttendanceRecords)
                .HasForeignKey(a => new { a.SessionDate, a.Course_Id });

            // Composite Foreign Key for Submissions -> class_session
            modelBuilder.Entity<Submission>()
                .HasOne(s => s.ClassSession)
                .WithMany(cs => cs.Submissions)
                .HasForeignKey(s => new { s.SessionDate, s.Course_Id });

            // Configure unique constraints
            modelBuilder.Entity<AttendedBy>()
                .HasIndex(a => new { a.SessionDate, a.Course_Id, a.UtdId })
                .IsUnique();

            modelBuilder.Entity<Submission>()
                .HasIndex(s => new { s.Course_Id, s.SessionDate, s.Utd_Id, s.Quiz_Id })
                .IsUnique();
        }
    }
}


