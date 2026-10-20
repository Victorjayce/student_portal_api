using Students_portal.Models;
namespace Students_portal.Dtos;
public record ResultDto(int StudentId, int CourseId, string Session, Semester Semester, decimal Score);
