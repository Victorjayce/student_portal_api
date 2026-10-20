using Students_portal.Dtos;
using Students_portal.Models;

namespace Students_portal.Interface;

public interface IFaculty
{
    Task<List<FacultyResponseDto>> GetFaculties();
    Task<FacultyResponseDto?> GetFaculty(int id);
    Task<List<DepartmentResponseDto>> GetFacultyDepartments(int id);
    Task<Faculty?> AddFaculty(FacultyDto facultydto);
    Task<Faculty?> UpdateFaculty(int id, FacultyDto faculty);
    Task<bool> DeleteFaculty(int id);
}
