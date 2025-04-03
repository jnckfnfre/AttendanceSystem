// Created by Nahyan Munawar 2025-04-02T00:00:00
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