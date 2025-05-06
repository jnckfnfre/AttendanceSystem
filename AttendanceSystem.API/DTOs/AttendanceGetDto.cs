// Created by Nahyan Munawar 4/2/2025
// DTO for getting attendance records

namespace AttendanceSystem.API.DTOs
{
    public class AttendanceGetDto
    {
        public int Attendance_Id { get; set; }
        public DateTime Session_Date { get; set; }
        public string Course_Id { get; set; }
        public string Utd_Id { get; set; }
    }
} 