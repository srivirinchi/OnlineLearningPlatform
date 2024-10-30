// OnlineLearningPlatform.Tests/AssignmentControllerTests.cs
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
    public class AssignmentControllerTests
    {
        private readonly AssignmentController _controller;
        private readonly Mock<LearningPlatformContext> _mockContext;

        public AssignmentControllerTests()
        {
            var options = new DbContextOptionsBuilder<LearningPlatformContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _mockContext = new Mock<LearningPlatformContext>(options);
            _controller = new AssignmentController(_mockContext.Object);
        }

        [Fact]
        public void GetAssignments_ReturnsAllAssignments()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                new Assignment { Id = 1, Title = "Assignment 1", Description = "Description 1", CourseId = 1 },
                new Assignment { Id = 2, Title = "Assignment 2", Description = "Description 2", CourseId = 2 }
            };
            _mockContext.Setup(c => c.Assignments).ReturnsDbSet(assignments);

            // Act
            var result = _controller.GetAssignments();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Assignment>>>(result);
            var returnValue = Assert.IsType<List<Assignment>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        //Add more test cases for other methods
        [Fact]
        public void GetAssignment_ReturnsAssignmentById()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                new Assignment { Id = 1, Title = "Assignment 1", Description = "Description 1", CourseId = 1 },
                new Assignment { Id = 2, Title = "Assignment 2", Description = "Description 2", CourseId = 2 }
            };
            _mockContext.Setup(c => c.Assignments.Find(1)).Returns(assignments[0]);

            // Act
            var result = _controller.GetAssignment(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Assignment>>(result);
            var returnValue = Assert.IsType<Assignment>(actionResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
        [Fact]
        public void DeleteAssignment_ReturnsNoContent()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                new Assignment { Id = 1, Title = "Assignment 1", Description = "Description 1", CourseId = 1 },
                new Assignment { Id = 2, Title = "Assignment 2", Description = "Description 2", CourseId = 2 }
            };
            _mockContext.Setup(c => c.Assignments.Find(1)).Returns(assignments[0]);

            // Act
            var result = _controller.DeleteAssignment(1);

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void UpdateAssignment_ReturnsBadRequest()
        {
            // Arrange
            var assignment = new Assignment { Id = 1, Title = "Assignment 1", Description = "Description 1", CourseId = 1 };

            // Act
            var result = _controller.UpdateAssignment(2, assignment);

            // Assert
            var actionResult = Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public void DeleteAssignment_ReturnsNotFound()
        {
            // Arrange
            _mockContext.Setup(c => c.Assignments.Find(1)).Returns((Assignment)null);

            // Act
            var result = _controller.DeleteAssignment(1);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void CreateAssignment_ReturnsCreatedAssignment()
        {
            // Arrange
            var assignment = new Assignment { Id = 1, Title = "Assignment 1", Description = "Description 1", CourseId = 1 };

            // Act
            var result = _controller.CreateAssignment(assignment);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Assignment>>(result);
            var returnValue = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(201, returnValue.StatusCode);
        }
        [Fact]
        public void UpdateAssignment_ReturnsNoContent()
        {
            // Arrange
            var assignment = new Assignment { Id = 1, Title = "Assignment 1", Description = "Description 1", CourseId = 1 };

            // Act
            var result = _controller.UpdateAssignment(1, assignment);

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, actionResult.StatusCode);
        }
    }
}
