using Students_portal.Dtos;
using Students_portal.Models;

namespace Students_portal.Interface
{
    public interface IStudent
    {
        Task<List<StudentResponseDto>> GetStudents();
        Task<StudentResponseDto?> GetStudent(int id);
        Task<StudentResponseDto?> AddStudent(StudentDto studentdto);
        Task<StudentResponseDto?> UpdateStudent(int id, StudentDto student);
        Task<bool> DeleteStudent(int id);
    }
}
