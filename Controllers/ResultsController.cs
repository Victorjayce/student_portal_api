using Microsoft.AspNetCore.Mvc;
using Students_portal.Interface;
using Students_portal.Models;
using Students_portal.Dtos;
using IResult = Students_portal.Interface.IResult;

namespace Students_portal.Controllers;

[ApiController, Route("api/results")]
public class ResultsController : ControllerBase
{
    private readonly IResult _service;
    public ResultsController(IResult service) => _service = service;
    [HttpGet("get-results")] public async Task<ActionResult<IEnumerable<ResultResponseDto>>> GetResults() => await _service.GetResults();
    [HttpGet("get-result/{id}")] public async Task<ActionResult<ResultResponseDto>> GetResult(int id) => await _service.GetResult(id) is { } item ? item : NotFound("Result not found");
    [HttpGet("get-student-results/{studentId}")] public async Task<ActionResult<IEnumerable<ResultResponseDto>>> GetStudentResults(int studentId) => await _service.GetStudentResults(studentId);
    [HttpGet("get-course-results/{courseId}")] public async Task<ActionResult<IEnumerable<ResultResponseDto>>> GetCourseResults(int courseId) => await _service.GetCourseResults(courseId);
    [HttpGet("get-department-results/{departmentId}")] public async Task<ActionResult<IEnumerable<ResultResponseDto>>> GetDepartmentResults(int departmentId) => await _service.GetDepartmentResults(departmentId);
    [HttpGet("get-faculty-results/{facultyId}")] public async Task<ActionResult<IEnumerable<ResultResponseDto>>> GetFacultyResults(int facultyId) => await _service.GetFacultyResults(facultyId);
    [HttpPost("add-result")] public async Task<ActionResult<Result>> AddResult(ResultDto item) { var created = await _service.AddResult(item); if (created is null) return BadRequest("Student or Course not found"); return CreatedAtAction(nameof(GetResult), new { id = created.Id }, created); }
    [HttpPut("update-result/{id}")] public async Task<IActionResult> UpdateResult(int id, ResultDto item) => await _service.UpdateResult(id, item) is not null ? NoContent() : NotFound("Result, Student, or Course not found");
    [HttpDelete("delete-result/{id}")] public async Task<IActionResult> DeleteResult(int id) => await _service.DeleteResult(id) ? NoContent() : NotFound("Result not found");
}
