using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationApi1.Data;
using WebApplicationApi1.Models;

namespace WebApplicationApi1.Controllers;

    [ApiController] 
    [Route("api/[controller]")]
    
public class CategoriesController:ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
     var categories=await   _context.Categories.Include(c=>c.Products).ToListAsync();
     return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound(new { message = "Category not found" });
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Category category)
    {
        if (id != category.Id)
            return BadRequest(new { message = "Category ID mismatch" });

        var existing = await _context.Categories.FindAsync(id);
        if (existing == null)
            return NotFound(new { message = "Category not found" });

        existing.Name = category.Name;
        existing.Description = category.Description;

        await _context.SaveChangesAsync();
        return Ok(existing);
    }

    // DELETE: api/categories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return NotFound(new { message = "Category not found" });

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}