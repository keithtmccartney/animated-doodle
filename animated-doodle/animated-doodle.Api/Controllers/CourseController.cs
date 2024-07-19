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
public class CourseController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;

    public CourseController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [HttpGet]
    [SwaggerOperation("Get Courses")]
    [ProducesResponseType(typeof(IEnumerable<CourseDto>), 200)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> GetCourses()
    {
        try
        {
            var courses = await _courseRepository.GetCourses();

            return Ok(courses);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet("{id}", Name = Routes.GetCourse)]
    [SwaggerOperation("Get Course")]
    [ProducesResponseType(typeof(CourseDto), 200)]
    [SwaggerResponse(404)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> GetCourse(int id)
    {
        try
        {
            var course = await _courseRepository.GetCourse(id);

            if (course == null)
                return NotFound();

            return Ok(course);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPost(Name = Routes.PostCourse)]
    [SwaggerOperation("Post Course")]
    [ProducesResponseType(typeof(CourseDto), 201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> PostCourse([FromBody] CourseDto course)
    {
        try
        {
            if (course == null)
                return BadRequest();

            await _courseRepository.PostCourse(course);

            return CreatedAtRoute(Routes.PostCourse, new { Id = course.Id }, course);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Put Course")]
    [ProducesResponseType(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(404)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> PutCourse(int id, [FromBody] CourseDto course)
    {
        try
        {
            if (course == null)
                return BadRequest();

            var existing = await _courseRepository.GetCourse(id);

            if (existing == null)
                return NotFound();

            await _courseRepository.PutCourse(id, course);

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete Course")]
    [ProducesResponseType(204)]
    [SwaggerResponse(404)]
    [SwaggerResponse(500)]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        try
        {
            var course = await _courseRepository.GetCourse(id);

            if (course == null)
                return NotFound();

            await _courseRepository.DeleteCourse(id);

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
