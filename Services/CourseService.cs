using Microsoft.EntityFrameworkCore;
using Students_portal.Data;
using Students_portal.Dtos;
using Students_portal.Interface;
using Students_portal.Models;

namespace Students_portal.Services;

public class CourseService : ICourse
{
    private readonly DataContext _context;
    public CourseService(DataContext context) => _context = context;

    public async Task<List<CourseResponseDto>> GetCourses() => (await _context.CoursesTable.AsNoTracking().Include(c => c.Department).ToListAsync()).Select(MaptoDto.MapCourse).ToList();
    public async Task<CourseResponseDto?> GetCourse(int id) => (await _context.CoursesTable.AsNoTracking().Include(c => c.Department).SingleOrDefaultAsync(c => c.Id == id)) is { } c ? MaptoDto.MapCourse(c) : null;
    public async Task<Course?> AddCourse(CourseDto coursedto)
    {
        if (!await _context.DepartmentsTable.AnyAsync(d => d.Id == coursedto.DepartmentId)) return null;
        var course = new Course
        {
            Code = coursedto.Code,
            Title = coursedto.Title,
            CreditUnits = coursedto.CreditUnits,
            Level = coursedto.Level,
            Semester = coursedto.Semester,
            DepartmentId = coursedto.DepartmentId
        };
        course.Id = 0;
        _context.CoursesTable.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }
    public async Task<Course?> UpdateCourse(int id, CourseDto course)
    {
        var existing = await _context.CoursesTable.FindAsync(id);
        if (existing == null || !await _context.DepartmentsTable.AnyAsync(d => d.Id == course.DepartmentId)) return null;
        existing.Code = course.Code;
        existing.Title = course.Title;
        existing.CreditUnits = course.CreditUnits;
        existing.Level = course.Level;
        existing.Semester = course.Semester;
        existing.DepartmentId = course.DepartmentId;
        await _context.SaveChangesAsync();
        return existing;
    }
    public async Task<bool> DeleteCourse(int id)
    {
        var existing = await _context.CoursesTable.FindAsync(id);
        if (existing == null || await _context.ResultsTable.AnyAsync(r => r.CourseId == id)) return false;
        _context.CoursesTable.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
