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

        public string Answer_1 { get; set; } = "x";
        public string Answer_2 { get; set; } = "x";
        public string Answer_3 { get; set; } = "x";
        public string Status { get; set; } = "absent";
    }

}
