using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AttendanceSystem.API.Models
{
    // Nahyan Munawar 3/21/2025
    // Model which maps to the Student table
    // This tells EF Core that this model maps to "Student" table of the database
    [Table("Student")]
    public class Student
    {
        // Marks UtdId as the primary key of the table.
        // It maps to the "UTD_ID" column in the Database
        [Key]
        [Column("UTD_ID")]
        public string Utd_Id { get; set; }

        // EF Core automatically matches property names to column names if they are the same,
        // otherwise we can use [Column()] as below

        // Hamza Khawaja 4/17/2025
        // add courseId column, want to dynamically asign quiz
        // student based on the course they are taking
        // [Column("COURSE_ID")]
        // public string Course_Id {get; set;}
        

        [Column("FIRST_NAME")]
        public string First_Name { get; set; }  // Student's first name

        [Column("LAST_NAME")]
        public string Last_Name { get; set; }   // Student's last name

        [Column("NET_ID")]
        public string? Net_Id { get; set; }      // Student's UTD NetID used for login

        // Navigation properties since submission and attendedby have foreign keys to this model
        // initialize with empty list to avoid null error
        [JsonIgnore]
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
        [JsonIgnore]
        public ICollection<AttendedBy> AttendanceRecords { get; set; } = new List<AttendedBy>();
    }
}
