using animated_doodle.Data;
using animated_doodle.Data.Dtos;
using animated_doodle.Data.Models;
using animated_doodle.Data.Repositories;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace animated_doodle.Tests.animated_doodleApi.Repositories;

[TestClass]
public class StudentRepositoryTests
{
    public Fixture _fixture;
    public SchoolContext _schoolContext;
    public StudentRepository _underTest;

    [TestInitialize]
    public void Initialize()
    {
        _fixture = new Fixture();

        var options = new DbContextOptionsBuilder<SchoolContext>().UseInMemoryDatabase(databaseName: "SchoolTest").Options;

        _schoolContext = new SchoolContext(options);
        _underTest = new StudentRepository(_schoolContext);
    }

    /// <summary>
    /// Returns all students
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task GetStudents_EnsureCorrect()
    {
        // Arrange
        var courses = _fixture.Build<Course>().Without(c => c.Students).CreateMany(3).ToList();
        var students = _fixture.Build<Student>().Without(s => s.Courses).CreateMany(5).ToList();

        _schoolContext.Students.AddRange(students);
        _schoolContext.Courses.AddRange(courses);

        await _schoolContext.SaveChangesAsync();

        var comparison = students.Select(student => new StudentDto
        {
            Id = student.Id,
            Forename = student.Forename,
            Surname = student.Surname,
            DateOfBirth = student.DateOfBirth,
            EmailAddress = student.EmailAddress,
            Gender = student.Gender,
            CourseIds = new List<int>()
        }).ToList();

        // Act
        var result = await _underTest.GetStudents();

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(comparison.Count);
        result.ElementAt(2).ShouldBeEquivalentTo(comparison[2]);
    }

    /// <summary>
    /// Returns student with courses
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task GetStudent_EnsureCorrect()
    {
        // Arrange
        var courses = _fixture.Build<Course>().Without(c => c.Students).CreateMany(3).ToList();
        var student = _fixture.Build<Student>().Without(s => s.Courses).Create();

        student.Courses = courses;

        _schoolContext.Students.Add(student);
        _schoolContext.Courses.AddRange(courses);

        await _schoolContext.SaveChangesAsync();

        var comparison = new StudentDto
        {
            Id = student.Id,
            Forename = student.Forename,
            Surname = student.Surname,
            DateOfBirth = student.DateOfBirth,
            EmailAddress = student.EmailAddress,
            Gender = student.Gender,
            CourseIds = student.Courses.Select(c => c.Id).ToList()
        };

        // Act
        var result = await _underTest.GetStudent(student.Id);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEquivalentTo(comparison);
    }

    /// <summary>
    /// Adds student with courses
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task PostStudent_EnsureCorrect()
    {
        // Arrange
        var courses = _fixture.Build<Course>().Without(c => c.Students).CreateMany(3).ToList();

        _schoolContext.Courses.AddRange(courses);

        await _schoolContext.SaveChangesAsync();

        var studentDto = _fixture.Build<StudentDto>().With(s => s.CourseIds, courses.Select(c => c.Id).ToList()).Create();

        // Act
        await _underTest.PostStudent(studentDto);

        // Assert
        var result = await _schoolContext.Students.Include(s => s.Courses).FirstOrDefaultAsync(s => s.Forename == studentDto.Forename);

        result.ShouldNotBeNull();
        result.Forename.ShouldBe(studentDto.Forename);
        result.Surname.ShouldBe(studentDto.Surname);
        result.DateOfBirth.ShouldBe(studentDto.DateOfBirth);
        result.EmailAddress.ShouldBe(studentDto.EmailAddress);
        result.Gender.ShouldBe(studentDto.Gender);
        result.Courses.Count.ShouldBe(studentDto.CourseIds.Count);
        result.Courses.Select(c => c.Id).ShouldBe(studentDto.CourseIds, ignoreOrder: true);
    }

    /// <summary>
    /// Updates student with courses
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task PutStudent_EnsureCorrect()
    {
        // Arrange
        var courses = _fixture.Build<Course>().Without(c => c.Students).CreateMany(3).ToList();
        var student = _fixture.Build<Student>().Without(s => s.Courses).Create();

        student.Courses = courses;

        _schoolContext.Students.Add(student);
        _schoolContext.Courses.AddRange(courses);

        await _schoolContext.SaveChangesAsync();

        var comparison = _fixture.Build<Course>().Without(c => c.Students).CreateMany(2).ToList();

        _schoolContext.Courses.AddRange(comparison);

        await _schoolContext.SaveChangesAsync();

        var studentDto = _fixture.Build<StudentDto>().With(s => s.CourseIds, comparison.Select(c => c.Id).ToList()).Create();

        // Act
        await _underTest.PutStudent(student.Id, studentDto);

        // Assert
        var result = await _schoolContext.Students.Include(s => s.Courses).FirstOrDefaultAsync(s => s.Id == student.Id);

        result.ShouldNotBeNull();
        result.Forename.ShouldBe(studentDto.Forename);
        result.Surname.ShouldBe(studentDto.Surname);
        result.DateOfBirth.ShouldBe(studentDto.DateOfBirth);
        result.EmailAddress.ShouldBe(studentDto.EmailAddress);
        result.Gender.ShouldBe(studentDto.Gender);
        result.Courses.Count.ShouldBe(studentDto.CourseIds.Count);
        result.Courses.Select(c => c.Id).ShouldBe(studentDto.CourseIds, ignoreOrder: true);
    }

    /// <summary>
    /// Removes student
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task DeleteStudent_EnsureCorrect()
    {
        // Arrange
        var student = _fixture.Build<Student>().Without(s => s.Courses).Create();

        _schoolContext.Students.Add(student);

        await _schoolContext.SaveChangesAsync();

        // Act
        await _underTest.DeleteStudent(student.Id);

        // Assert
        var deletedStudent = await _schoolContext.Students.FindAsync(student.Id);

        deletedStudent.ShouldBeNull();
    }
}
