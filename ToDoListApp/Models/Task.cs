using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")] // Maksimal uzunlik
        [Required(ErrorMessage = "Name is required.")] // Required validatsiyasi
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")] // Maksimal uzunlik
        public string Description { get; set; } = string.Empty;

        public string? ImagePath { get; set; } // Fayl yo'li nullable

        [DataType(DataType.Date)] // Sana turi
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime Deadline { get; set; } = DateTime.Now;
        public bool IsCompleted { get; set; } = false;

        [Required(ErrorMessage = "Priority is required.")] // Required validatsiyasi
        public string Priority { get; set; } = "Low"; // Standart qiymat
    }
}
