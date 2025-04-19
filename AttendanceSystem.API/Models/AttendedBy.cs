// Created by Nahyan Munawar 4/2/2025
// Model which maps to the Attended_By table

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AttendanceSystem.API.Models;

[Table("Attended_By")]
public class AttendedBy
{
    [Key]
    [Column("ATTENDANCE_ID")]
    public int AttendanceId { get; set; } // Unique attendance record ID

    [Column("SESSION_DATE")]
    public DateTime SessionDate { get; set; } // Date of the session

    [Column("COURSE_ID")]
    public string Course_Id { get; set; } // Course attended

    [Column("UTD_ID")]
    public string UtdId { get; set; } // Student who attended

    [JsonIgnore]
    [ForeignKey("UtdId")]
    public Student Student { get; set; }

    [JsonIgnore]
    [ForeignKey("Course_Id, SessionDate")]
    public ClassSession ClassSession { get; set; }
}
