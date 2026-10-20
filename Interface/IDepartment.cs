using Students_portal.Dtos;
using Students_portal.Models;

namespace Students_portal.Interface;

public interface IDepartment
{
    Task<List<DepartmentResponseDto>> GetDepartments();
    Task<DepartmentResponseDto?> GetDepartment(int id);
    Task<List<CourseResponseDto>> GetDepartmentCourses(int id);
    Task<List<StudentResponseDto>> GetDepartmentStudents(int id);
    Task<Department?> AddDepartment(DepartmentDto departmentdto);
    Task<Department?> UpdateDepartment(int id, DepartmentDto department);
    Task<bool> DeleteDepartment(int id);
}
