using Students_portal.Dtos;
using Students_portal.Models;

namespace Students_portal.Interface;

public interface IResult
{
    Task<List<ResultResponseDto>> GetResults();
    Task<ResultResponseDto?> GetResult(int id);
    Task<List<ResultResponseDto>> GetStudentResults(int studentId);
    Task<List<ResultResponseDto>> GetCourseResults(int courseId);
    Task<List<ResultResponseDto>> GetDepartmentResults(int departmentId);
    Task<List<ResultResponseDto>> GetFacultyResults(int facultyId);
    Task<Result?> AddResult(ResultDto resultdto);
    Task<Result?> UpdateResult(int id, ResultDto result);
    Task<bool> DeleteResult(int id);
}
