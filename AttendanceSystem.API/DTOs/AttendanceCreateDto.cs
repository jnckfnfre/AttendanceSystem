// Created by Nahyan Munawar 4/2/2025
// DTO for creating attendance records

using System;

namespace AttendanceSystem.API.DTOs
{
    public class AttendanceCreateDto
    {
        public string UtdId { get; set; }
        public string Course_Id { get; set; }
        public DateTime SessionDate { get; set; }
    }
} 