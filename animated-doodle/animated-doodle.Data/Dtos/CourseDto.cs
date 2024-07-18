using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace animated_doodle.Data.Dtos;

public class CourseDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public List<int>? StudentIds { get; set; }
}
