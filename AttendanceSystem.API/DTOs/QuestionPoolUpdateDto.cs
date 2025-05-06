/*
    Maha Shaikh 4/4/2025
    DTO for updating existing question pools
    This DTO includes the PoolId to identify which pool to update
    and allows updating fields like PoolName
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    public class QuestionPoolUpdateDto
    {
        // The unique identifier of the question pool to be updated
        [Required]
        public int Pool_Id { get; set; }

        // The updated name of the question pool
        // This is required for updating the pool's identifying name
        [Required]
        public string Pool_Name { get; set; }

        [Required]
        public string Course_Id { get; set; } // Foreign key added - Eduardo Zamora 4/28/2025
    }
}
