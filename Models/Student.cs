using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Students_portal.Models
{
    [Index(nameof(MatriculationNumber), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public string Firstname { get; set; } = null!;
        public string? Middlename { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Phone, MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }
        [MaxLength(100)]
        public string? StateOfOrigin { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }
        [Required]
        public string? MatriculationNumber { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int EntryYear { get; set; }

        [Required, Range(100, 600)]
        public string Level { get; set; } = null!;
        [Required]
        public StudentStatus Status { get; set; } = StudentStatus.Active;
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; } = null!;

        public ICollection<Result> Results { get; set; } = new List<Result>();

    }

    
}
