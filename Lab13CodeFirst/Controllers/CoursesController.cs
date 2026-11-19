using Lab13CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab13CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses
                .Where(c => c.Active)
                .ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.idCourse == id && c.Active);

            if (course == null)
                return NotFound();

            return course;
        }

        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            course.Active = true;
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.idCourse }, course);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.idCourse)
                return BadRequest();

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Courses.Any(e => e.idCourse == id && e.Active))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.idCourse == id && c.Active);

            if (course == null)
                return NotFound();

            course.Active = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}