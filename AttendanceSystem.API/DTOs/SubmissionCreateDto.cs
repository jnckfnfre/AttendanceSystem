using System.ComponentModel.DataAnnotations;
//Nahyan Munawar April 2

namespace AttendanceSystem.API.DTOs
{
    // DTO for creating new submissions
    // This DTO matches the structure of the Submission model but excludes navigation properties
    // and auto-generated fields like Submission_Id
    public class SubmissionCreateDto
    {
        [Required]
        public string Course_Id { get; set; }

        [Required]
        public DateTime SessionDate { get; set; }

        [Required]
        public string Utd_Id { get; set; }

        [Required]
        public int? Quiz_Id { get; set; }

        [Required]
        public string Ip_Address { get; set; }

        // Changed from TimeSpan to DateTime to match the Submission model and database schema
        // This ensures consistent type handling across the entire application
        // When making POST requests, this should be a full datetime string (e.g., "2025-04-02T14:30:00")
        [Required]
        public DateTime Submission_Time { get; set; }

        [Required]
        public string Answer_1 { get; set; }

        [Required]
        public string Answer_2 { get; set; }

        [Required]
        public string Answer_3 { get; set; }

        [Required]
        public string Status { get; set; }
    }
} 