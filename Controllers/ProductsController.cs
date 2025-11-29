
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationApi1.Data;
using WebApplicationApi1.DTOs;
using WebApplicationApi1.Models;

namespace WebApplicationApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductsController:ControllerBase
    {


        private readonly AppDbContext _context;   


        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _context.products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    CategoryId = p.Category != null ? p.Category.Id : null,
                    SupplierId = p.Supplier != null ? p.Supplier.Id : null
                })
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest(new { message = "Product ID mismatch" });

            var existing = await _context.products.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = "Product not found" });

            // Update fields
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Stock = product.Stock;
            existing.Category_id = product.Category_id;
            existing.Supplier_id = product.Supplier_id;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            _context.products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

