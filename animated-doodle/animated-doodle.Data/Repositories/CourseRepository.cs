using animated_doodle.Data.Dtos;
using animated_doodle.Data.Interfaces;
using animated_doodle.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace animated_doodle.Data.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly SchoolContext _schoolContext;

    public CourseRepository(SchoolContext context)
    {
        _schoolContext = context;
    }

    public async Task<IEnumerable<CourseDto>> GetCourses()
    {
        return await _schoolContext.Courses.Select(course => new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            StudentIds = course.Students.Select(s => s.Id).ToList()
        }).ToListAsync();
    }

    public async Task<CourseDto> GetCourse(int courseId)
    {
        var course = await _schoolContext.Courses.Include(c => c.Students).FirstOrDefaultAsync(e => e.Id == courseId);

        if (course == null)
            return null;

        return new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            StudentIds = course.Students.Select(s => s.Id).ToList()
        };
    }

    public async Task PostCourse(CourseDto courseDto)
    {
        var course = new Course
        {
            Name = courseDto.Name,
            Students = new List<Student>()
        };

        if (courseDto.StudentIds != null)
        {
            foreach (var studentId in courseDto.StudentIds)
            {
                var student = await _schoolContext.Students.FindAsync(studentId);

                if (student != null)
                    course.Students.Add(student);
            }
        }

        _schoolContext.Courses.Add(course);

        await _schoolContext.SaveChangesAsync();
    }

    public async Task PutCourse(int id, CourseDto courseDto)
    {
        var course = await _schoolContext.Courses.Include(c => c.Students).FirstOrDefaultAsync(c => c.Id == id);

        if (course != null)
        {
            course.Name = courseDto.Name;

            if (courseDto.StudentIds != null)
            {
                course.Students.RemoveAll(s => !courseDto.StudentIds.Contains(s.Id));

                foreach (var studentId in courseDto.StudentIds)
                {
                    if (!course.Students.Any(s => s.Id == studentId))
                    {
                        var student = await _schoolContext.Students.FindAsync(studentId);

                        if (student != null)
                            course.Students.Add(student);
                    }
                }
            }

            await _schoolContext.SaveChangesAsync();
        }
    }

    public async Task DeleteCourse(int id)
    {
        var course = await _schoolContext.Courses.FindAsync(id);

        if (course != null)
        {
            _schoolContext.Courses.Remove(course);

            await _schoolContext.SaveChangesAsync();
        }
    }
}
