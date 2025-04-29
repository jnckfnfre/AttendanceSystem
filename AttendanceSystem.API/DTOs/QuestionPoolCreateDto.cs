/*
    Nahyan Munawar 4/2/2025
    DTO for creating new question pools
    This DTO matches the structure of the QuestionPool model but excludes navigation properties
    and auto-generated fields like PoolId
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    public class QuestionPoolCreateDto
    {
        // The name of the question pool
        // This is required as every pool must have a name to identify it
        [Required]
        public string PoolName { get; set; }

        [Required]
        public string Course_Id { get; set; } // Foreign key added - Eduardo Zamora 4/28/2025
    }
} 