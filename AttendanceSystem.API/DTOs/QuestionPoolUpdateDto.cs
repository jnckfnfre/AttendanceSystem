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
        public int PoolId { get; set; }

        // The updated name of the question pool
        // This is required for updating the pool's identifying name
        [Required]
        public string PoolName { get; set; }
    }
}
