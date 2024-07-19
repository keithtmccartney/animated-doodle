using animated_doodle.Data.Dtos;

namespace animated_doodle.Data.Interfaces;

public interface IStudentRepository
{
    Task<IEnumerable<StudentDto>> GetStudents();

    Task<StudentDto> GetStudent(int studentId);

    Task PostStudent(StudentDto student);

    Task PutStudent(int id, StudentDto student);

    Task DeleteStudent(int id);
}
