using Microsoft.EntityFrameworkCore;
using Students_portal.Data;
using Students_portal.Dtos;
using Students_portal.Interface;
using Students_portal.Models;

namespace Students_portal.Services;

public class FacultyService : IFaculty
{
    private readonly DataContext _context;
    public FacultyService(DataContext context) => _context = context;

    public async Task<List<FacultyResponseDto>> GetFaculties() => (await _context.FacultiesTable.AsNoTracking().ToListAsync()).Select(MaptoDto.MapFaculty).ToList();
    public async Task<FacultyResponseDto?> GetFaculty(int id) => (await _context.FacultiesTable.AsNoTracking().SingleOrDefaultAsync(f => f.Id == id)) is { } f ? MaptoDto.MapFaculty(f) : null;
    public async Task<List<DepartmentResponseDto>> GetFacultyDepartments(int id) => (await _context.DepartmentsTable.AsNoTracking().Include(d => d.Faculty).Where(d => d.FacultyId == id).ToListAsync()).Select(MaptoDto.MapDepartment).ToList();
    public async Task<Faculty?> AddFaculty(FacultyDto facultydto)
    {
        var faculty = new Faculty
        {
            Name = facultydto.Name,
            Code = facultydto.Code
        };
        faculty.Id = 0;
        _context.FacultiesTable.Add(faculty);
        await _context.SaveChangesAsync();
        return faculty;
    }
    public async Task<Faculty?> UpdateFaculty(int id, FacultyDto faculty)
    {
        var existing = await _context.FacultiesTable.FindAsync(id);
        if (existing == null) return null;
        existing.Name = faculty.Name;
        existing.Code = faculty.Code;
        await _context.SaveChangesAsync();
        return existing;
    }
    public async Task<bool> DeleteFaculty(int id)
    {
        var existing = await _context.FacultiesTable.FindAsync(id);
        if (existing == null || await _context.DepartmentsTable.AnyAsync(d => d.FacultyId == id)) return false;
        _context.FacultiesTable.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
