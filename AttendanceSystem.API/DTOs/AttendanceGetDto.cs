// Created by Nahyan Munawar 4/2/2025
// DTO for getting attendance records

namespace AttendanceSystem.API.DTOs
{
    public class AttendanceGetDto
    {
        public int AttendanceId { get; set; }
        public DateTime SessionDate { get; set; }
        public string Course_Id { get; set; }
        public string UtdId { get; set; }
    }
} 