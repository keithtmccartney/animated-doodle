using animated_doodle.Api.Controllers;
using animated_doodle.Data.Interfaces;
using AutoFixture;
using Moq;

namespace animated_doodle.Tests.animated_doodleApi.Controllers;

[TestClass]
public class CourseControllerTests
{
    public Fixture _fixture;
    public Mock<ICourseRepository> _courseRepository;
    public CourseController _underTest;

    [TestInitialize]
    public void Initialise()
    {
        _fixture = new Fixture();
        _courseRepository = new Mock<ICourseRepository>();
        _underTest = new CourseController(_courseRepository.Object);
    }

    /// <summary>
    /// Future tests will be added here,
    /// the repositories are of utter importance
    /// to the controller and have been tested
    /// </summary>
    [TestMethod]
    public void TestMethod1()
    {
    }
}
