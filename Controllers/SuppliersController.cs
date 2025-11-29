using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationApi1.Data;
using WebApplicationApi1.Models;

namespace WebApplicationApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SuppliersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/suppliers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _context.Suppliers
                .Include(s => s.Products) 
                .ToListAsync();
            return Ok(suppliers);
        }

        // GET: api/suppliers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _context.Suppliers
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (supplier == null)
                return NotFound(new { message = "Supplier not found" });
            return Ok(supplier);
        }

        // POST: api/suppliers
        /*[HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //_context.Suppliers.Add(supplier);
             _context.Suppliers.AddRangeAsync(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, supplier);
        }*/
        
        [HttpPost("bulk")]
        public async Task<IActionResult> CreateBulk([FromBody] List<Supplier> suppliers)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Suppliers.AddRangeAsync(suppliers); // <-- Add collection
            await _context.SaveChangesAsync();

            return Ok(suppliers);
        }

        // PUT: api/suppliers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Supplier supplier)
        {
            if (id != supplier.Id)
                return BadRequest(new { message = "Supplier ID mismatch" });

            var existing = await _context.Suppliers.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = "Supplier not found" });

            // Update fields
            existing.Name = supplier.Name;
          
            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        // DELETE: api/suppliers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound(new { message = "Supplier not found" });

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
