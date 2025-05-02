using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    // Author: Hamza Khawaja  4/28/2025
    public class SubmissionCreateDto
    {
        [Required] public string CourseId      { get; set; }
        [Required] public DateTime SessionDate { get; set; }
        [Required] public string UtdId         { get; set; }
        [Required] public int    QuizId        { get; set; }

        // bind all checked radios named "Answers"
        [Required] public List<string> Answers { get; set; }

        [Required] public string Status        { get; set; }
    }
}
