using animated_doodle.Data.Interfaces;
using animated_doodle.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using animated_doodle.Api.Extensions;

namespace animated_doodle.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;

    public StudentController(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    [HttpGet]
    [SwaggerOperation("Get Students")]
    [ProducesResponseType(typeof(IEnumerable<StudentDto>), 200)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> GetStudents()
    {
        try
        {
            var students = await _studentRepository.GetStudents();

            return Ok(students);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet("{id}", Name = Routes.GetStudent)]
    [SwaggerOperation("Get Student")]
    [ProducesResponseType(typeof(StudentDto), 200)]
    [SwaggerResponse(404)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> GetStudent(int id)
    {
        try
        {
            var student = await _studentRepository.GetStudent(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPost(Name = Routes.PostStudent)]
    [SwaggerOperation("Post Student")]
    [ProducesResponseType(typeof(StudentDto), 201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> PostStudent([FromBody] StudentDto student)
    {
        try
        {
            if (student == null)
                return BadRequest();

            await _studentRepository.PostStudent(student);

            return CreatedAtRoute(Routes.PostStudent, new { Id = student.Id }, student);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Put Student")]
    [ProducesResponseType(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(404)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> PutStudent(int id, [FromBody] StudentDto student)
    {
        try
        {
            if (student == null)
                return BadRequest();

            var existing = await _studentRepository.GetStudent(id);

            if (existing == null)
                return NotFound();

            await _studentRepository.PutStudent(id, student);

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete Student")]
    [ProducesResponseType(204)]
    [SwaggerResponse(404)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        try
        {
            var student = await _studentRepository.GetStudent(id);

            if (student == null)
                return NotFound();

            await _studentRepository.DeleteStudent(id);

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
