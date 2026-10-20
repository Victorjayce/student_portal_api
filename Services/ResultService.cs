using Microsoft.EntityFrameworkCore;
using Students_portal.Data;
using Students_portal.Dtos;
using Students_portal.Models;
using IResult = Students_portal.Interface.IResult;

namespace Students_portal.Services;

public class ResultService : IResult
{
    private readonly DataContext _context;
    public ResultService(DataContext context) => _context = context;

    private IQueryable<Result> ResultQuery() => _context.ResultsTable.AsNoTracking().Include(r => r.Student).Include(r => r.Course);
    public async Task<List<ResultResponseDto>> GetResults() => (await ResultQuery().ToListAsync()).Select(MaptoDto.MapResult).ToList();
    public async Task<ResultResponseDto?> GetResult(int id) => (await ResultQuery().SingleOrDefaultAsync(r => r.Id == id)) is { } r ? MaptoDto.MapResult(r) : null;
    public async Task<List<ResultResponseDto>> GetStudentResults(int studentId) => (await ResultQuery().Where(r => r.StudentId == studentId).ToListAsync()).Select(MaptoDto.MapResult).ToList();
    public async Task<List<ResultResponseDto>> GetCourseResults(int courseId) => (await ResultQuery().Where(r => r.CourseId == courseId).ToListAsync()).Select(MaptoDto.MapResult).ToList();
    public async Task<List<ResultResponseDto>> GetDepartmentResults(int departmentId) => (await ResultQuery().Where(r => r.Course.DepartmentId == departmentId).ToListAsync()).Select(MaptoDto.MapResult).ToList();
    public async Task<List<ResultResponseDto>> GetFacultyResults(int facultyId) => (await ResultQuery().Where(r => r.Course.Department.FacultyId == facultyId).ToListAsync()).Select(MaptoDto.MapResult).ToList();
    private static string GetGrade(decimal score) => score >= 70 ? "A" : score >= 60 ? "B" : score >= 55 ? "C" : score >= 45 ? "D" : score >= 40 ? "E" : "F";
    public async Task<Result?> AddResult(ResultDto resultdto)
    {
        var result = new Result
        {
            StudentId = resultdto.StudentId,
            CourseId = resultdto.CourseId,
            Session = resultdto.Session,
            Semester = resultdto.Semester,
            Score = resultdto.Score
        };
        if (!await _context.StudentsTable.AnyAsync(s => s.Id == result.StudentId) || !await _context.CoursesTable.AnyAsync(c => c.Id == result.CourseId)) return null;
        result.Id = 0;
        result.Grade = GetGrade(result.Score);
        _context.ResultsTable.Add(result);
        await _context.SaveChangesAsync();
        return result;
    }
    public async Task<Result?> UpdateResult(int id, ResultDto result)
    {
        var existing = await _context.ResultsTable.FindAsync(id);
        if (existing == null || !await _context.StudentsTable.AnyAsync(s => s.Id == result.StudentId) || !await _context.CoursesTable.AnyAsync(c => c.Id == result.CourseId)) return null;
        existing.StudentId = result.StudentId;
        existing.CourseId = result.CourseId;
        existing.Session = result.Session;
        existing.Semester = result.Semester;
        existing.Score = result.Score;
        existing.Grade = GetGrade(result.Score);
        await _context.SaveChangesAsync();
        return existing;
    }
    public async Task<bool> DeleteResult(int id)
    {
        var existing = await _context.ResultsTable.FindAsync(id);
        if (existing == null) return false;
        _context.ResultsTable.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
