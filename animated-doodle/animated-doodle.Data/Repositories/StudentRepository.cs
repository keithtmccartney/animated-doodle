using animated_doodle.Data.Dtos;
using animated_doodle.Data.Interfaces;
using animated_doodle.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace animated_doodle.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly SchoolContext _schoolContext;

    public StudentRepository(SchoolContext context)
    {
        _schoolContext = context;
    }

    public async Task<IEnumerable<StudentDto>> GetStudents()
    {
        return await _schoolContext.Students.Select(student => new StudentDto
        {
            Id = student.Id,
            Forename = student.Forename,
            Surname = student.Surname,
            DateOfBirth = student.DateOfBirth,
            EmailAddress = student.EmailAddress,
            Gender = student.Gender,
            CourseIds = student.Courses.Select(c => c.Id).ToList()
        }).ToListAsync();
    }

    public async Task<StudentDto> GetStudent(int studentId)
    {
        var student = await _schoolContext.Students.Include(s => s.Courses).FirstOrDefaultAsync(e => e.Id == studentId);

        if (student == null)
            return null;

        return new StudentDto
        {
            Id = student.Id,
            Forename = student.Forename,
            Surname = student.Surname,
            DateOfBirth = student.DateOfBirth,
            EmailAddress = student.EmailAddress,
            Gender = student.Gender,
            CourseIds = student.Courses.Select(c => c.Id).ToList()
        };
    }

    public async Task PostStudent(StudentDto studentDto)
    {
        var student = new Student
        {
            Forename = studentDto.Forename,
            Surname = studentDto.Surname,
            DateOfBirth = studentDto.DateOfBirth,
            EmailAddress = studentDto.EmailAddress,
            Gender = studentDto.Gender,
            Courses = new List<Course>()
        };

        if (studentDto.CourseIds != null)
        {
            foreach (var courseId in studentDto.CourseIds)
            {
                var course = await _schoolContext.Courses.FindAsync(courseId);

                if (course != null)
                    student.Courses.Add(course);
            }
        }

        _schoolContext.Students.Add(student);

        await _schoolContext.SaveChangesAsync();
    }

    public async Task PutStudent(int id, StudentDto studentDto)
    {
        var student = await _schoolContext.Students.FindAsync(id);

        if (student != null)
        {
            student.Forename = studentDto.Forename;
            student.Surname = studentDto.Surname;
            student.DateOfBirth = studentDto.DateOfBirth;
            student.EmailAddress = studentDto.EmailAddress;
            student.Gender = studentDto.Gender;

            if (studentDto.CourseIds != null)
            {
                student.Courses.RemoveAll(c => !studentDto.CourseIds.Contains(c.Id));

                foreach (var courseId in studentDto.CourseIds)
                {
                    if (!student.Courses.Any(c => c.Id == courseId))
                    {
                        var course = await _schoolContext.Courses.FindAsync(courseId);

                        if (course != null)
                            student.Courses.Add(course);
                    }
                }
            }

            await _schoolContext.SaveChangesAsync();
        }
    }

    public async Task DeleteStudent(int id)
    {
        var student = await _schoolContext.Students.FindAsync(id);

        if (student != null)
        {
            _schoolContext.Students.Remove(student);

            await _schoolContext.SaveChangesAsync();
        }
    }
}
