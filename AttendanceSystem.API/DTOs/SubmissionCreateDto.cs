using System.ComponentModel.DataAnnotations;
//Nahyan Munawar April 2

namespace AttendanceSystem.API.DTOs
{
    // DTO for creating new submissions
    // This DTO matches the structure of the Submission model but excludes navigation properties
    // and auto-generated fields like Submission_Id
    public class SubmissionCreateDto
    {
        public string Course_Id { get; set; }
        public DateTime SessionDate { get; set; }
        public string Utd_Id { get; set; }

        public int? Quiz_Id { get; set; }

        public string Ip_Address { get; set; } = "0.0.0.0";
        public DateTime Submission_Time { get; set; } = DateTime.Parse("1900-01-01T00:00:00");

        public string Answer_1 { get; set; } = "x";
        public string Answer_2 { get; set; } = "x";
        public string Answer_3 { get; set; } = "x";
        public string Status { get; set; } = "absent";
    }

} 