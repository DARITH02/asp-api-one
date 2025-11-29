

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationApi1.Data;
using WebApplicationApi1.DTOs;
using WebApplicationApi1.Models;

namespace WebApplicationApi1.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    
public class PurchasesController:ControllerBase
{
    private readonly AppDbContext _context;
    
    public PurchasesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        /*var purchases = await _context.Purchases
            .Include(p => p.Supplier)
            .ToListAsync();*/

        var purchases = await _context.Purchases
            .Include(p => p.Supplier)
            .Select(p => new PurchaseDTO
            {
                Id = p.Id,
                SupplierId = p.SupplierId,
                Date = p.Date,
                TotalAmount = p.TotalAmount

            }).ToListAsync();
        
        
        return Ok(purchases);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        /*var purchase = await _context.Purchases
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.Id == id);*/

        var purchase = await _context.Purchases
            .Include(p => p.Supplier)
            .Where(p => p.Id == id)
            .Select(p=>new PurchaseDTO
            {
                Id = p.Id,
                SupplierId = p.Supplier.Id,
                Date = p.Date,
                TotalAmount = p.TotalAmount
            }).FirstOrDefaultAsync();
        if (purchase == null)
        {
            return NotFound();
        }
        return Ok(purchase);
    }
    [HttpPost]
    public async Task<IActionResult> Create (Purchase purchase)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Add(purchase);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetById),new {id = purchase.Id},purchase);
    }
    
    
    //update 

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id , Purchase purchase)
    {
        if (id != purchase.Id)
        {
            return BadRequest(new{message="Purchase id does not match"});
        }

        var existing = await _context.Purchases.FindAsync(id);
        if (existing == null)
        {
            return NotFound(new {message ="Purchase not found..!"});
        }

        //update data in field
        existing.SupplierId = purchase.SupplierId;
        existing.Date=purchase.Date;
        existing.TotalAmount=purchase.TotalAmount;
        
        await _context.SaveChangesAsync();
        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var purchase = await _context.Purchases.FindAsync(id);

        if (purchase == null)
        {
            return NotFound(new {message = "Purchase not found..!"});
        }

        _context.Purchases.Remove(purchase);

        await _context.SaveChangesAsync();

        return Ok(purchase);
    }
    
}