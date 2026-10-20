using Students_portal.Models;
namespace Students_portal.Dtos;
public record StudentDto(string Surname, string Firstname, string? Middlename, string Email, string? PhoneNumber, DateTime DateOfBirth, Gender Gender, string? StateOfOrigin, string? Address, int DepartmentId, int EntryYear, string Level, StudentStatus? Status);
