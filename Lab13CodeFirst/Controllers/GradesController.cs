using Lab13CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab13CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public GradesController(SchoolContext context)
        {
            _context = context;
        }

    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGrades()
        {
            return await _context.Grades
                .Where(g => g.Active)
                .ToListAsync();
        }

     
        [HttpGet("{id}")]
        public async Task<ActionResult<Grade>> GetGrade(int id)
        {
            var grade = await _context.Grades
                .FirstOrDefaultAsync(g => g.idGrade == id && g.Active);

            if (grade == null)
                return NotFound();

            return grade;
        }

        [HttpPost]
        public async Task<ActionResult<Grade>> PostGrade(Grade grade)
        {
            grade.Active = true;
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGrade), new { id = grade.idGrade }, grade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrade(int id, Grade grade)
        {
            if (id != grade.idGrade)
                return BadRequest();

            _context.Entry(grade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Grades.Any(e => e.idGrade == id && e.Active))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = await _context.Grades
                .FirstOrDefaultAsync(g => g.idGrade == id && g.Active);

            if (grade == null)
                return NotFound();

            grade.Active = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}