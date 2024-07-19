using animated_doodle.Data.Dtos;

namespace animated_doodle.Data.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<CourseDto>> GetCourses();

    Task<CourseDto> GetCourse(int courseId);

    Task PostCourse(CourseDto course);

    Task PutCourse(int id, CourseDto course);

    Task DeleteCourse(int id);
}
