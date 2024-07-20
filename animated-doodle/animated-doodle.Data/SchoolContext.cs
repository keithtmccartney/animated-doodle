using animated_doodle.Data.Extensions;
using animated_doodle.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace animated_doodle.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }

    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
}
