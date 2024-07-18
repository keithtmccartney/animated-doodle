using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace animated_doodle.Data.Dtos;

public class StudentDto
{
    public int Id { get; set; }

    public string? Forename { get; set; }

    public string? Surname { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? EmailAddress { get; set; }

    public string? Gender { get; set; }

    public List<int>? CourseIds { get; set; }
}
