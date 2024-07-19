using animated_doodle.Api.Controllers;
using animated_doodle.Data.Interfaces;
using AutoFixture;
using Moq;

namespace animated_doodle.Tests.animated_doodleApi.Controllers;

[TestClass]
public class StudentControllerTests
{
    public Fixture _fixture;
    public Mock<IStudentRepository> _studentRepository;
    public StudentController _underTest;

    [TestInitialize]
    public void Initialise()
    {
        _fixture = new Fixture();
        _studentRepository = new Mock<IStudentRepository>();
        _underTest = new StudentController(_studentRepository.Object);
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
