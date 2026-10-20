using Students_portal.Models;

namespace Students_portal.Dtos;

public static class MaptoDto
{
    public static ResultResponseDto MapResult(Result r) => new(r.Id, r.StudentId,
        r.Student == null ? string.Empty : $"{r.Student.Firstname} {r.Student.Surname}", r.Student?.MatriculationNumber,
        r.CourseId, r.Course?.Code ?? string.Empty, r.Course?.Title ?? string.Empty,
        r.Session, r.Semester, r.Score, r.Grade);
    public static FacultyResponseDto MapFaculty(Faculty f) => new(f.Id, f.Name, f.Code);
    public static DepartmentResponseDto MapDepartment(Department d) => new(d.Id, d.Name, d.Code, d.FacultyId, d.Faculty?.Name ?? string.Empty);
    public static CourseResponseDto MapCourse(Course c) => new(c.Id, c.Code, c.Title, c.CreditUnits, c.Level, c.Semester, c.DepartmentId, c.Department?.Name ?? string.Empty);
    public static StudentResponseDto MapStudentToResponseDto(Student s) => new(s.Id, s.Surname, s.Firstname, s.Middlename,
        s.Email, s.PhoneNumber, s.DateOfBirth, s.Gender, s.StateOfOrigin, s.Address, s.MatriculationNumber,
        s.DepartmentId, s.Department?.Name ?? string.Empty, s.EntryYear, s.Level, s.Status,
        s.Results.Select(MapResult).ToList());
}
