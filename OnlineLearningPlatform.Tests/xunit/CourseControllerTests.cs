// OnlineLearningPlatform.Tests/CourseControllerTests.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineLearningPlatform.Controllers;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OnlineLearningPlatform.Tests
{
    public class CourseControllerTests
    {
        private readonly CourseController _controller;
        private readonly Mock<LearningPlatformContext> _mockContext;

        public CourseControllerTests()
        {
            var options = new DbContextOptionsBuilder<LearningPlatformContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _mockContext = new Mock<LearningPlatformContext>(options);
            _controller = new CourseController(_mockContext.Object);
        }

        [Fact]
        public void GetCourses_ReturnsAllCourses()
        {
            // Arrange
            var courses = new List<Course>
            {
                new Course { Id = 1, CourseName = "Course 1", Description = "Description 1", Credits = 3 },
                new Course { Id = 2, CourseName = "Course 2", Description = "Description 2", Credits = 4 }
            };
            _mockContext.Setup(c => c.Courses).ReturnsDbSet(courses);

            // Act
            var result = _controller.GetCourses();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Course>>>(result);
            var returnValue = Assert.IsType<List<Course>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        //Add more test cases for other methods
        [Fact]
        public void GetCourse_ReturnsCourseById()
        {
            // Arrange
            var courses = new List<Course>
            {
                new Course { Id = 1, CourseName = "Course 1", Description = "Description 1", Credits = 3 },
                new Course { Id = 2, CourseName = "Course 2", Description = "Description 2", Credits = 4 }
            };
            _mockContext.Setup(c => c.Courses.Find(1)).Returns(courses[0]);

            // Act
            var result = _controller.GetCourse(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Course>>(result);
            var returnValue = Assert.IsType<Course>(actionResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
        [Fact]
        public void CreateCourse_ReturnsCreatedCourse()
        {
            // Arrange
            var course = new Course { Id = 1, CourseName = "Course 1", Description = "Description 1", Credits = 3 };

            // Act
            var result = _controller.CreateCourse(course);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Course>>(result);
            var returnValue = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(201, returnValue.StatusCode);
        }
        [Fact]
        public void UpdateCourse_ReturnsNoContent()
        {
            // Arrange
            var course = new Course { Id = 1, CourseName = "Course 1", Description = "Description 1", Credits = 3 };

            // Act
            var result = _controller.UpdateCourse(1, course);

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, actionResult.StatusCode);
        }
        [Fact]
        public void DeleteCourse_ReturnsNoContent()
        {
            // Arrange
            var courses = new List<Course>
            {
                new Course { Id = 1, CourseName = "Course 1", Description = "Description 1", Credits = 3 },
                new Course { Id = 2, CourseName = "Course 2", Description = "Description 2", Credits = 4 }
            };
            _mockContext.Setup(c => c.Courses.Find(1)).Returns(courses[0]);

            // Act
            var result = _controller.DeleteCourse(1);

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, actionResult.StatusCode);
        }
        [Fact]
        public void UpdateCourse_ReturnsBadRequest()
        {
            // Arrange
            var course = new Course { Id = 1, CourseName = "Course 1", Description = "Description 1", Credits = 3 };

            // Act
            var result = _controller.UpdateCourse(2, course);

            // Assert
            var actionResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, actionResult.StatusCode);
        }
        [Fact]
        public void DeleteCourse_ReturnsNotFound()
        {
            // Arrange
            _mockContext.Setup(c => c.Courses.Find(1)).Returns((Course)null);

            // Act
            var result = _controller.DeleteCourse(1);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, actionResult.StatusCode);
        }
        [Fact]
        public void CreateCourse_ReturnsBadRequest()
        {
            // Arrange
            var course = new Course { Id = 1, CourseName = "Course 1", Description = "Description 1", Credits = 3 };

            // Act
            var result = _controller.CreateCourse(course);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, actionResult.StatusCode);
        }
        [Fact]
        public void CreateCourse_ReturnsBadRequest_WhenCourseIsNull()
        {
            // Arrange
            Course course = null;

            // Act
            var result = _controller.CreateCourse(course);

            // Assert
            var actionResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, actionResult.StatusCode);
        }
    }
}
