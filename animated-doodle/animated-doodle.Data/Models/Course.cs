using System.ComponentModel.DataAnnotations;

namespace animated_doodle.Data.Models;

public class Course
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public List<Student>? Students { get; set; }
}
