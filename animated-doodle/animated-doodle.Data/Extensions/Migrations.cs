using animated_doodle.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace animated_doodle.Data.Extensions;

public static class Migrations
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(new
        {
            Id = 1,
            Name = "Course 1"
        }, new
        {
            Id = 2,
            Name = "Course 2"
        });

        modelBuilder.Entity<Student>().HasData(new
        {
            Id = 1,
            Forename = "John",
            Surname = "Doe",
            DateOfBirth = new DateTime(2000, 1, 1),
            EmailAddress = "john.doe@example.com",
            Gender = "Male"
        }, new
        {
            Id = 2,
            Forename = "Jane",
            Surname = "Doe",
            DateOfBirth = new DateTime(2001, 2, 2),
            EmailAddress = "jane.doe@example.com",
            Gender = "Female"
        });

        modelBuilder.Entity("CourseStudent").HasData(new
        {
            CoursesId = 1,
            StudentsId = 1
        }, new
        {
            CoursesId = 1,
            StudentsId = 2
        }, new
        {
            CoursesId = 2,
            StudentsId = 1
        });
    }
}
