using Students_portal.Models;

namespace Students_portal.Dtos;

public record FacultyResponseDto(int Id, string Name, string Code);
public record DepartmentResponseDto(int Id, string Name, string Code, int FacultyId, string FacultyName);
public record CourseResponseDto(int Id, string Code, string Title, int CreditUnits, int Level, Semester Semester, int DepartmentId, string DepartmentName);
public record ResultResponseDto(int Id, int StudentId, string StudentName, string? MatriculationNumber, int CourseId, string CourseCode, string CourseTitle, string Session, Semester Semester, decimal Score, string Grade);
public record StudentResponseDto(int Id, string Surname, string Firstname, string? Middlename, string Email, string? PhoneNumber, DateTime DateOfBirth, Gender Gender, string? StateOfOrigin, string? Address, string? MatriculationNumber, int DepartmentId, string DepartmentName, int EntryYear, string Level, StudentStatus Status, IReadOnlyList<ResultResponseDto> Results);
