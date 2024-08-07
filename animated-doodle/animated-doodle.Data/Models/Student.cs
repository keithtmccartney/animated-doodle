﻿using System.ComponentModel.DataAnnotations;

namespace animated_doodle.Data.Models;

public class Student
{
    [Key]
    public int Id { get; set; }

    public string? Forename { get; set; }

    public string? Surname { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? EmailAddress { get; set; }

    public string? Gender { get; set; }

    public List<Course>? Courses { get; set; }
}
