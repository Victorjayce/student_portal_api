using Students_portal.Dtos;
using Students_portal.Models;

namespace Students_portal.Interface;

public interface ICourse
{
    Task<List<CourseResponseDto>> GetCourses();
    Task<CourseResponseDto?> GetCourse(int id);
    Task<Course?> AddCourse(CourseDto coursedto);
    Task<Course?> UpdateCourse(int id, CourseDto course);
    Task<bool> DeleteCourse(int id);
}
