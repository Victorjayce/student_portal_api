using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students_portal.Models
{
    // one result per student, per course, per session
    [Index(nameof(StudentId), nameof(CourseId), nameof(Session), IsUnique = true)]
    public class Result
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = null!;

        [Required]
        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; } = null!;

        [Required, MaxLength(9)]
        [RegularExpression(@"^\d{4}/\d{4}$", ErrorMessage = "Session must look like 2025/2026")]
        public string Session { get; set; } = null!;

        [Required]
        public Semester Semester { get; set; }

        [Required, Range(0, 100)]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Score { get; set; }

        [Required, MaxLength(2)]
        public string Grade { get; set; } = null!;
    }
}