using Microsoft.EntityFrameworkCore;
using Students_portal.Data;
using Students_portal.Dtos;
using Students_portal.Interface;
using Students_portal.Models;

namespace Students_portal.Services
{
    public class StudentService : IStudent
    {
        private readonly DataContext _context;

        public StudentService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<StudentResponseDto>> GetStudents()
        {
            var students = await _context.StudentsTable.AsNoTracking().Include(s => s.Department).Include(s => s.Results).ThenInclude(r => r.Course).ToListAsync();
            return students.Select(MaptoDto.MapStudentToResponseDto).ToList();
        }

        public async Task<StudentResponseDto?> GetStudent(int id)
        {
            var student = await _context.StudentsTable.AsNoTracking().Include(s => s.Department).Include(s => s.Results).ThenInclude(r => r.Course).SingleOrDefaultAsync(s => s.Id == id);
            if (student == null)
                return null;

            return MaptoDto.MapStudentToResponseDto(student);
        }

        public async Task<StudentResponseDto?> AddStudent(StudentDto studentdto)
        {
            var department = await _context.DepartmentsTable
                .SingleOrDefaultAsync(d => d.Id == studentdto.DepartmentId);
            if (department == null)
                return null;

            var student = new Student
            {
                Surname = studentdto.Surname,
                Firstname = studentdto.Firstname,
                Middlename = studentdto.Middlename,
                Email = studentdto.Email,
                PhoneNumber = studentdto.PhoneNumber,
                DateOfBirth = studentdto.DateOfBirth,
                Gender = studentdto.Gender,
                StateOfOrigin = studentdto.StateOfOrigin,
                Address = studentdto.Address,
                DepartmentId = studentdto.DepartmentId,
                MatriculationNumber = null,
                EntryYear = studentdto.EntryYear,
                Level = studentdto.Level,
                Status = StudentStatus.Active
            };

            _context.StudentsTable.Add(student);
            await _context.SaveChangesAsync();

            student.MatriculationNumber = $"{DateTime.Now.Year}{department.Code}{student.Id:D3}";
            await _context.SaveChangesAsync();
            return MaptoDto.MapStudentToResponseDto(student);
        }

        public async Task<StudentResponseDto?> UpdateStudent(int id, StudentDto student)
        {
            var existingStudent = await _context.StudentsTable.FindAsync(id);
            if (existingStudent == null)
            {
                return null;
            }

            // make sure the new department actually exists
            var departmentExists = await _context.DepartmentsTable
                .AnyAsync(d => d.Id == student.DepartmentId);
            if (!departmentExists)
            {
                return null;
            }

            existingStudent.Surname = student.Surname;
            existingStudent.Firstname = student.Firstname;
            existingStudent.Middlename = student.Middlename;
            existingStudent.Email = student.Email;
            existingStudent.PhoneNumber = student.PhoneNumber;
            existingStudent.DateOfBirth = student.DateOfBirth;
            existingStudent.Gender = student.Gender;
            existingStudent.StateOfOrigin = student.StateOfOrigin;
            existingStudent.Address = student.Address;
            existingStudent.DepartmentId = student.DepartmentId;
            existingStudent.EntryYear = student.EntryYear;
            existingStudent.Level = student.Level;
            existingStudent.Status = (StudentStatus)student.Status!;

            await _context.SaveChangesAsync();
            return MaptoDto.MapStudentToResponseDto(existingStudent);
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student = await _context.StudentsTable.FindAsync(id);
            if (student == null)
            {
                return false;
            }

            if (await _context.ResultsTable.AnyAsync(r => r.StudentId == id))
                return false;

            _context.StudentsTable.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
    
}
