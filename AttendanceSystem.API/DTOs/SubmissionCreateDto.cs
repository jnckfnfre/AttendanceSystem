using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    // Author: Hamza Khawaja  4/28/2025
    public class SubmissionCreateDto
    {
        public string Course_Id { get; set; }

        public DateTime Session_Date { get; set; }
        public string Utd_Id { get; set; }
        public int? Quiz_Id { get; set; }
        public string Ip_Address { get; set; } = "0.0.0.0";
        public DateTime Submission_Time { get; set; } = DateTime.Parse("1900-01-01T00:00:00");

        [Required(ErrorMessage = "Answers are required")]
        public string[] Answers { get; set; } = new string[3] { "x", "x", "x" };
        public string Status { get; set; } = "absent";
    }

}
