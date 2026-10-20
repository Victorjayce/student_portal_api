using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students_portal.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class Course
    {
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string Code { get; set; } = null!;

        [Required, MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required, Range(1, 5)]
        public int CreditUnits { get; set; }

        [Required, Range(100, 600)]
        public int Level { get; set; }

        [Required]
        public Semester Semester { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; } = null!;

        public ICollection<Result> Results { get; set; } = new List<Result>();
    }
}