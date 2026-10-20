using Microsoft.AspNetCore.Mvc;
using Students_portal.Interface;
using Students_portal.Models;
using Students_portal.Dtos;

namespace Students_portal.Controllers;

[ApiController, Route("api/departments")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartment _service;
    public DepartmentsController(IDepartment service) => _service = service;
    [HttpGet("get-departments")] public async Task<ActionResult<IEnumerable<DepartmentResponseDto>>> GetDepartments() => await _service.GetDepartments();
    [HttpGet("get-department/{id}")] public async Task<ActionResult<DepartmentResponseDto>> GetDepartment(int id) => await _service.GetDepartment(id) is { } item ? item : NotFound("Department not found");
    [HttpGet("get-department-courses/{id}")] public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetDepartmentCourses(int id) => await _service.GetDepartmentCourses(id);
    [HttpGet("get-department-students/{id}")] public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetDepartmentStudents(int id) => await _service.GetDepartmentStudents(id);
    [HttpPost("add-department")] public async Task<ActionResult<Department>> AddDepartment(DepartmentDto item) { var created = await _service.AddDepartment(item); if (created is null) return BadRequest("Faculty not found"); return CreatedAtAction(nameof(GetDepartment), new { id = created.Id }, created); }
    [HttpPut("update-department/{id}")] public async Task<IActionResult> UpdateDepartment(int id, DepartmentDto item) => await _service.UpdateDepartment(id, item) is not null ? NoContent() : NotFound("Department or Faculty not found");
    [HttpDelete("delete-department/{id}")] public async Task<IActionResult> DeleteDepartment(int id) => await _service.DeleteDepartment(id) ? NoContent() : Conflict("Department not found or still has students/courses");
}
