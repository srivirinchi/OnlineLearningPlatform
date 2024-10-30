// Controllers/StudentProgressController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLearningPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentProgressController : ControllerBase
    {
        private readonly LearningPlatformContext _context;

        public StudentProgressController(LearningPlatformContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all student progress records.
        /// </summary>
        /// <returns>A list of student progress records.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<StudentProgress>> GetStudentProgresses()
        {
            return _context.StudentProgresses.ToList();
        }

        /// <summary>
        /// Retrieves a specific student progress record by ID.
        /// </summary>
        /// <param name="id">The ID of the student progress record.</param>
        /// <returns>The student progress record if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public ActionResult<StudentProgress> GetStudentProgress(int id)
        {
            var studentProgress = _context.StudentProgresses.Find(id);
            if (studentProgress == null)
            {
                return NotFound();
            }
            return studentProgress;
        }

        /// <summary>
        /// Creates a new student progress record.
        /// </summary>
        /// <param name="studentProgress">The student progress record to create.</param>
        /// <returns>The created student progress record.</returns>
        [HttpPost]
        public ActionResult<StudentProgress> CreateStudentProgress(StudentProgress studentProgress)
        {
            _context.StudentProgresses.Add(studentProgress);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStudentProgress), new { id = studentProgress.Id }, studentProgress);
        }

        /// <summary>
        /// Updates an existing student progress record.
        /// </summary>
        /// <param name="id">The ID of the student progress record to update.</param>
        /// <param name="studentProgress">The updated student progress record.</param>
        /// <returns>NoContent if the update is successful; otherwise, BadRequest.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateStudentProgress(int id, StudentProgress studentProgress)
        {
            if (id != studentProgress.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentProgress).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific student progress record by ID.
        /// </summary>
        /// <param name="id">The ID of the student progress record to delete.</param>
        /// <returns>NoContent if the deletion is successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteStudentProgress(int id)
        {
            var studentProgress = _context.StudentProgresses.Find(id);
            if (studentProgress == null)
            {
                return NotFound();
            }

            _context.StudentProgresses.Remove(studentProgress);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
