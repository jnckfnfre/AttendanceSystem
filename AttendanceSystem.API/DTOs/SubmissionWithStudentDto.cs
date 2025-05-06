/*
    David Sajdak 4/17/2025
    DTO created specifically for reporting
    Want to display student name with submission info
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs{
    public class SubmissionWithStudentDto {
        [Required]
        public int Submission_Id { get; set; }

        [Required]
        public string Course_Id { get; set; }

        [Required]
        public DateTime SessionDate { get; set; }

        [Required]
        public string Utd_Id { get; set; }

        // added to include student name in submission display within reporting
        [Required]
        public string Student_Name { get; set; }

        [Required]
        public int? Quiz_Id { get; set; }

        [Required]
        public string Ip_Address { get; set; }

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