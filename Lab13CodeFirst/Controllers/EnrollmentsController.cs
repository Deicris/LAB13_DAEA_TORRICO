using Lab13CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab13CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public EnrollmentsController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet("by-course")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetByCourse([FromQuery] string? courseName)
        {
            var query = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(courseName))
                query = query.Where(e => e.Course != null && e.Course.Name.Contains(courseName));

            return await query
                .OrderBy(e => e.Course!.Name)
                .ThenBy(e => e.Student!.LastName)
                .ToListAsync();
        }


        [HttpGet("by-grade")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetByGrade([FromQuery] string? gradeName)
        {
            var query = _context.Enrollments
                .Include(e => e.Student)
                    .ThenInclude(s => s!.Grade)
                .Include(e => e.Course)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(gradeName))
                query = query.Where(e => e.Student != null &&
                                         e.Student.Grade != null &&
                                         e.Student.Grade.Name.Contains(gradeName));

            return await query
                .OrderBy(e => e.Course!.Name)
                .ThenBy(e => e.Student!.LastName)
                .ToListAsync();
        }
    }
}