using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    // Author: Hamza Khawaja  4/28/2025
    public class SubmissionCreateDto
    {
        [Required] public string Course_Id      { get; set; }
        [Required] public DateTime Session_Date { get; set; }
        [Required] public string Utd_Id         { get; set; }
        [Required] public int    Quiz_Id        { get; set; }

        // bind all checked radios named "Answers"
        [Required] public List<string> Answers { get; set; }

        [Required] public string Status        { get; set; }
    }
}
