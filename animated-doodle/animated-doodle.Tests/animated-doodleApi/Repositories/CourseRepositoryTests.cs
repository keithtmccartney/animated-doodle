using animated_doodle.Data;
using animated_doodle.Data.Dtos;
using animated_doodle.Data.Models;
using animated_doodle.Data.Repositories;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace animated_doodle.Tests.animated_doodleApi.Repositories;

[TestClass]
public class CourseRepositoryTests
{
    public Fixture _fixture;
    public SchoolContext _schoolContext;
    public CourseRepository _underTest;

    [TestInitialize]
    public void Initialize()
    {
        _fixture = new Fixture();

        var options = new DbContextOptionsBuilder<SchoolContext>().UseInMemoryDatabase(databaseName: "SchoolTest").Options;

        _schoolContext = new SchoolContext(options);
        _underTest = new CourseRepository(_schoolContext);
    }

    /// <summary>
    /// Returns all courses
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task GetCourses_EnsureCorrect()
    {
        // Arrange
        var students = _fixture.Build<Student>().Without(s => s.Courses).CreateMany(3).ToList();
        var courses = _fixture.Build<Course>().Without(c => c.Students).CreateMany(5).ToList();

        _schoolContext.Students.AddRange(students);
        _schoolContext.Courses.AddRange(courses);

        await _schoolContext.SaveChangesAsync();

        var comparison = courses.Select(course => new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            StudentIds = new List<int>()
        }).ToList();

        // Act
        var result = await _underTest.GetCourses();

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(comparison.Count);
        result.ElementAt(2).ShouldBeEquivalentTo(comparison[2]);
    }

    /// <summary>
    /// Returns course with students
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task GetCourse_EnsureCorrect()
    {
        // Arrange
        var students = _fixture.Build<Student>().Without(s => s.Courses).CreateMany(3).ToList();
        var course = _fixture.Build<Course>().Without(c => c.Students).Create();

        course.Students = students;

        _schoolContext.Students.AddRange(students);
        _schoolContext.Courses.Add(course);

        await _schoolContext.SaveChangesAsync();

        var comparison = new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            StudentIds = course.Students.Select(s => s.Id).ToList()
        };

        // Act
        var result = await _underTest.GetCourse(course.Id);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeEquivalentTo(comparison);
    }

    /// <summary>
    /// Adds course with students
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task PostCourse_EnsureCorrect()
    {
        // Arrange
        var students = _fixture.Build<Student>().Without(s => s.Courses).CreateMany(3).ToList();

        _schoolContext.Students.AddRange(students);

        await _schoolContext.SaveChangesAsync();

        var courseDto = _fixture.Build<CourseDto>().With(c => c.StudentIds, students.Select(s => s.Id).ToList()).Create();

        // Act
        await _underTest.PostCourse(courseDto);

        // Assert
        var result = await _schoolContext.Courses.Include(c => c.Students).FirstOrDefaultAsync(c => c.Name == courseDto.Name);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(courseDto.Name);
        result.Students.Count.ShouldBe(courseDto.StudentIds.Count);
        result.Students.Select(s => s.Id).ShouldBe(courseDto.StudentIds, ignoreOrder: true);
    }

    /// <summary>
    /// Updates course with students
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task PutCourse_EnsureCorrect()
    {
        // Arrange
        var students = _fixture.Build<Student>().Without(s => s.Courses).CreateMany(3).ToList();
        var course = _fixture.Build<Course>().Without(c => c.Students).Create();

        course.Students = students;

        _schoolContext.Students.AddRange(students);
        _schoolContext.Courses.Add(course);

        await _schoolContext.SaveChangesAsync();

        var comparison = _fixture.Build<Student>().Without(s => s.Courses).CreateMany(2).ToList();

        _schoolContext.Students.AddRange(comparison);

        await _schoolContext.SaveChangesAsync();

        var courseDto = _fixture.Build<CourseDto>().With(c => c.StudentIds, comparison.Select(s => s.Id).ToList()).Create();

        // Act
        await _underTest.PutCourse(course.Id, courseDto);

        // Assert
        var result = await _schoolContext.Courses.Include(c => c.Students).FirstOrDefaultAsync(c => c.Id == course.Id);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(courseDto.Name);
        result.Students.Count.ShouldBe(courseDto.StudentIds.Count);
        result.Students.Select(s => s.Id).ShouldBe(courseDto.StudentIds, ignoreOrder: true);
    }

    /// <summary>
    /// Removes course
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task DeleteCourse_EnsureCorrect()
    {
        // Arrange
        var course = _fixture.Build<Course>().Without(c => c.Students).Create();

        _schoolContext.Courses.Add(course);

        await _schoolContext.SaveChangesAsync();

        // Act
        await _underTest.DeleteCourse(course.Id);

        // Assert
        var deletedCourse = await _schoolContext.Courses.FindAsync(course.Id);

        deletedCourse.ShouldBeNull();
    }
}
