namespace animated_doodle.Data.Dtos;

public class CourseDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public List<int>? StudentIds { get; set; }
}
