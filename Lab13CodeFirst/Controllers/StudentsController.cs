using Lab13CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab13CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Student>>> SearchStudents(
            [FromQuery] string? firstName,
            [FromQuery] string? lastName,
            [FromQuery] string? email)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(s => s.FirstName.Contains(firstName));

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(s => s.LastName.Contains(lastName));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(s => s.Email.Contains(email));

            return await query
                .OrderByDescending(s => s.LastName)
                .ToListAsync();
        }

        [HttpGet("search-by-grade")]
        public async Task<ActionResult<IEnumerable<Student>>> SearchByNameAndGrade(
            [FromQuery] string? firstName,
            [FromQuery] string? gradeName)
        {
            var query = _context.Students
                .Include(s => s.Grade)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(s => s.FirstName.Contains(firstName));

            if (!string.IsNullOrWhiteSpace(gradeName))
                query = query.Where(s => s.Grade != null && s.Grade.Name.Contains(gradeName));

            return await query
                .OrderByDescending(s => s.Grade!.Name)
                .ToListAsync();
        }
    }
}