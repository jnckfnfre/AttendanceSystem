using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Nahyan Munawar 3/21/2025
// Model which maps to the class_session table
[Table("class_session")]
public class ClassSession
{
    [Column("SESSION_DATE")]
    public DateTime SessionDate { get; set; } // Composite PK part 1 - date of the session

    [Column("COURSE_ID")]
    public string CourseId { get; set; } // Composite PK part 2 - course ID

    [Column("PASSWORD")]
    public string Password { get; set; } // Quiz password for the session

    [Column("QUIZ_ID")]
    public int QuizId { get; set; } // Associated quiz for the session

    // Composite keys are using Fluent API inside DbContext.OnModelCreating
}
