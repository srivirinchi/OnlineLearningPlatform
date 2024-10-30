// Controllers/AssignmentController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Models;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLearningPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentController : ControllerBase
    {
        private readonly LearningPlatformContext _context;

        public AssignmentController(LearningPlatformContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Assignment>> GetAssignments()
        {
            var assignments = _context.Assignments.ToList();
            return Ok(assignments);
        }

        [HttpGet("{id}")]
        public ActionResult<Assignment> GetAssignment(int id)
        {
            var assignment = _context.Assignments.Find(id);
            if (assignment == null)
            {
                return NotFound();
            }
            return Ok(assignment);
        }

        [HttpPost]
        public ActionResult<Assignment> CreateAssignment(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAssignment), new { id = assignment.Id }, assignment);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAssignment(int id, Assignment assignment)
        {
            if (id != assignment.Id)
            {
                return BadRequest();
            }

            _context.Entry(assignment).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAssignment(int id)
        {
            var assignment = _context.Assignments.Find(id);
            if (assignment == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignment);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
