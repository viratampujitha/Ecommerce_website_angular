using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeggieStoreAPI.Data;
using VeggieStoreAPI.Models;

namespace VeggieStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VegetablesController : ControllerBase
    {
        private readonly VeggieStoreContext _context;

        public VegetablesController(VeggieStoreContext context)
        {
            _context = context;
        }

        // GET: api/Vegetables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vegetable>>> GetVegetables()
        {
            return await _context.Vegetables.ToListAsync();
        }

        // GET: api/Vegetables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vegetable>> GetVegetable(int id)
        {
            var vegetable = await _context.Vegetables.FindAsync(id);

            if (vegetable == null)
            {
                return NotFound();
            }

            return vegetable;
        }

        // POST: api/Vegetables
        [HttpPost]
        public async Task<ActionResult<Vegetable>> PostVegetable(Vegetable vegetable)
        {
            _context.Vegetables.Add(vegetable);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVegetable), new { id = vegetable.Id }, vegetable);
        }

        // PUT: api/Vegetables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVegetable(int id, Vegetable vegetable)
        {
            if (id != vegetable.Id)
            {
                return BadRequest();
            }

            _context.Entry(vegetable).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Vegetables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVegetable(int id)
        {
            var vegetable = await _context.Vegetables.FindAsync(id);
            if (vegetable == null)
            {
                return NotFound();
            }

            _context.Vegetables.Remove(vegetable);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
