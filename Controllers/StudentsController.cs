using Microsoft.AspNetCore.Mvc;
using Students_portal.Models;
using Students_portal.Interface;
using Students_portal.Dtos;

[Route("api/students")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudent _student;
    public StudentsController(IStudent student)
    {
        _student = student;
    }

    [HttpGet("get-students")]
    public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetStudents()
    {
        var students = await _student.GetStudents();
        return students;
    }
    [HttpGet("get-student/{id}")]
    public async Task<ActionResult<StudentResponseDto>> GetStudent(int id)
    {
        var student = await _student.GetStudent(id);

        if (student is null)
        {
            return NotFound("Student not found");
        }

        return student;
    }

    [HttpPost("add-student")]
    public async Task<ActionResult<Student>> PostStudents(StudentDto students)
    {
        var created = await _student.AddStudent(students);
        if (created is null)
        {
            return BadRequest("Department not found");
        }

        return CreatedAtAction(nameof(GetStudent), new { id = created.Id }, created);
    }

    [HttpPut("edit-student/{id}")]
    public async Task<IActionResult> EditStudents(int id, StudentDto students)
    {
        var updated = await _student.UpdateStudent(id, students);
        if (updated is null)
            return NotFound("Student or Department not found");

        return NoContent();
    }




    [HttpDelete("delete-student/{id}")]
    public async Task<IActionResult> DeleteStudents(int id)
    {
        var success = await _student.DeleteStudent(id);
        if (!success)
        {
            return NotFound("Student not found");
        }

        return NoContent();
    }
}
