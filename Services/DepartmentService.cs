using Microsoft.EntityFrameworkCore;
using Students_portal.Data;
using Students_portal.Dtos;
using Students_portal.Interface;
using Students_portal.Models;

namespace Students_portal.Services;

public class DepartmentService : IDepartment
{
    private readonly DataContext _context;
    public DepartmentService(DataContext context) => _context = context;

    public async Task<List<DepartmentResponseDto>> GetDepartments() => (await _context.DepartmentsTable.AsNoTracking().Include(d => d.Faculty).ToListAsync()).Select(MaptoDto.MapDepartment).ToList();
    public async Task<DepartmentResponseDto?> GetDepartment(int id) => (await _context.DepartmentsTable.AsNoTracking().Include(d => d.Faculty).SingleOrDefaultAsync(d => d.Id == id)) is { } d ? MaptoDto.MapDepartment(d) : null;
    public async Task<List<CourseResponseDto>> GetDepartmentCourses(int id) => (await _context.CoursesTable.AsNoTracking().Include(c => c.Department).Where(c => c.DepartmentId == id).ToListAsync()).Select(MaptoDto.MapCourse).ToList();
    public async Task<List<StudentResponseDto>> GetDepartmentStudents(int id) => (await _context.StudentsTable.AsNoTracking().Include(s => s.Department).Include(s => s.Results).ThenInclude(r => r.Course).Where(s => s.DepartmentId == id).ToListAsync()).Select(MaptoDto.MapStudentToResponseDto).ToList();
    public async Task<Department?> AddDepartment(DepartmentDto departmentdto)
    {
        if (!await _context.FacultiesTable.AnyAsync(f => f.Id == departmentdto.FacultyId)) return null;
        var department = new Department
        {
            Name = departmentdto.Name,
            Code = departmentdto.Code,
            FacultyId = departmentdto.FacultyId
        };
        department.Id = 0;
        _context.DepartmentsTable.Add(department);
        await _context.SaveChangesAsync();
        return department;
    }
    public async Task<Department?> UpdateDepartment(int id, DepartmentDto department)
    {
        var existing = await _context.DepartmentsTable.FindAsync(id);
        if (existing == null || !await _context.FacultiesTable.AnyAsync(f => f.Id == department.FacultyId)) return null;
        existing.Name = department.Name;
        existing.Code = department.Code;
        existing.FacultyId = department.FacultyId;
        await _context.SaveChangesAsync();
        return existing;
    }
    public async Task<bool> DeleteDepartment(int id)
    {
        var existing = await _context.DepartmentsTable.FindAsync(id);
        if (existing == null || await _context.StudentsTable.AnyAsync(s => s.DepartmentId == id) || await _context.CoursesTable.AnyAsync(c => c.DepartmentId == id)) return false;
        _context.DepartmentsTable.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
