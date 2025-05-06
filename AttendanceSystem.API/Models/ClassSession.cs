using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Nahyan Munawar 3/21/2025 
// Model which maps to the class_session table
namespace AttendanceSystem.API.Models{ //Hamza Khawaja 4/11/2025 - Fixed some of the namespace issues. Wasnt able to reference the Quiz model in ClassSession
    [Table("class_session")]
    public class ClassSession{
        [Key]
        [Column("SESSION_DATE")]
        public DateTime SessionDate { get; set; }

        [Key]
        [Column("COURSE_ID")]
        public string Course_Id { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("QUIZ_ID")]
        public int? QuizId { get; set; }

        [ForeignKey("Course_Id")]
        public Course Course { get; set; }

        [ForeignKey("QuizId")]
        public Quiz? Quiz { get; set; }

        public ICollection<Submission> Submissions { get; set; }
        public ICollection<AttendedBy> AttendanceRecords { get; set; }
        
    }
}
