using Microsoft.AspNetCore.Mvc;
using Students_portal.Interface;
using Students_portal.Models;
using Students_portal.Dtos;

namespace Students_portal.Controllers;

[ApiController, Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly ICourse _service;
    public CoursesController(ICourse service) => _service = service;
    [HttpGet("get-courses")] public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetCourses() => await _service.GetCourses();
    [HttpGet("get-course/{id}")] public async Task<ActionResult<CourseResponseDto>> GetCourse(int id) => await _service.GetCourse(id) is { } item ? item : NotFound("Course not found");
    [HttpPost("add-course")] public async Task<ActionResult<Course>> AddCourse(CourseDto item) { var created = await _service.AddCourse(item); if (created is null) return BadRequest("Department not found"); return CreatedAtAction(nameof(GetCourse), new { id = created.Id }, created); }
    [HttpPut("update-course/{id}")] public async Task<IActionResult> UpdateCourse(int id, CourseDto item) => await _service.UpdateCourse(id, item) is not null ? NoContent() : NotFound("Course or Department not found");
    [HttpDelete("delete-course/{id}")] public async Task<IActionResult> DeleteCourse(int id) => await _service.DeleteCourse(id) ? NoContent() : Conflict("Course not found or still has results referencing it");
}
