using Students_portal.Models;
namespace Students_portal.Dtos;
public record CourseDto(string Code, string Title, int CreditUnits, int Level, Semester Semester, int DepartmentId);
