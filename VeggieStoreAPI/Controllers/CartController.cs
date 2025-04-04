using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeggieStoreAPI.Data;
using VeggieStoreAPI.Models;

namespace VeggieStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly VeggieStoreContext _context;

        public CartController(VeggieStoreContext context)
        {
            _context = context;
        }

        // GET: api/Cart/user/1
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartItems(int userId)
        {
            return await _context.Carts.Where(c => c.UserId == userId).Include(c => c.Vegetable).ToListAsync();
        }

        // POST: api/Cart
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCartItem(Cart cartItem)
        {
            _context.Carts.Add(cartItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCartItems), new { userId = cartItem.UserId }, cartItem);
        }

        // PUT: api/Cart/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartItem(int id, Cart cartItem)
        {
            if (id != cartItem.Id) return BadRequest();
            _context.Entry(cartItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItem = await _context.Carts.FindAsync(id);
            if (cartItem == null) return NotFound();
            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
