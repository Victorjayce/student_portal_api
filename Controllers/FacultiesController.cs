using Microsoft.AspNetCore.Mvc;
using Students_portal.Interface;
using Students_portal.Models;
using Students_portal.Dtos;

namespace Students_portal.Controllers;

[ApiController, Route("api/faculties")]
public class FacultiesController : ControllerBase
{
    private readonly IFaculty _service;
    public FacultiesController(IFaculty service) => _service = service;
    [HttpGet("get-faculties")] public async Task<ActionResult<IEnumerable<FacultyResponseDto>>> GetFaculties() => await _service.GetFaculties();
    [HttpGet("get-faculty/{id}")] public async Task<ActionResult<FacultyResponseDto>> GetFaculty(int id) => await _service.GetFaculty(id) is { } item ? item : NotFound("Faculty not found");
    [HttpGet("get-faculty-departments/{id}")] public async Task<ActionResult<IEnumerable<DepartmentResponseDto>>> GetFacultyDepartments(int id) => await _service.GetFacultyDepartments(id);
    [HttpPost("add-faculty")] public async Task<ActionResult<Faculty>> AddFaculty(FacultyDto item) { var created = await _service.AddFaculty(item); if (created is null) return BadRequest(); return CreatedAtAction(nameof(GetFaculty), new { id = created.Id }, created); }
    [HttpPut("update-faculty/{id}")] public async Task<IActionResult> UpdateFaculty(int id, FacultyDto item) => await _service.UpdateFaculty(id, item) is not null ? NoContent() : NotFound("Faculty not found");
    [HttpDelete("delete-faculty/{id}")] public async Task<IActionResult> DeleteFaculty(int id) => await _service.DeleteFaculty(id) ? NoContent() : Conflict("Faculty not found or still has departments");
}
