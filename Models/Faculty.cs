using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Students_portal.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class Faculty
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(10)]
        public string Code { get; set; } = null!;

        public ICollection<Department> Departments { get; set; } = new List<Department>();
    }
}