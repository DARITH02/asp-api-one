using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationApi1.Data;
using WebApplicationApi1.DTOs;
using WebApplicationApi1.Models;

namespace WebApplicationApi1.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PurchaseDetailsController:ControllerBase
{
    private readonly AppDbContext _context;
    public PurchaseDetailsController(AppDbContext context)
    {
        _context    = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var purchases = await _context.PurchaseDetails
            .Include(p => p.Purchase)
            .Include(p => p.Product)
            .Select(pd => new PurchaseDetailsDTO
            {
                purchaseId = pd.PurchaseId,
                productId = pd.Product.Id,
                quantity = pd.Quantity,
                unitPrice = pd.UnitPrice,
                subTotal = pd.SubTotal

            }).ToListAsync();
        return Ok(purchases);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var detail = await _context.PurchaseDetails
            .Include(p => p.Purchase)
            .Include(p => p.Product)
            .Where(p => p.Id == id)
            .Select(pd=>new PurchaseDetailsDTO
            {
                Id   = pd.Id,
                purchaseId = pd.PurchaseId,
                productId = pd.Product.Id,
                quantity = pd.Quantity,
                subTotal = pd.SubTotal,
                unitPrice = pd.UnitPrice
            })
            .FirstOrDefaultAsync();
        if (detail == null)
            return NotFound(new { message = "Purchase not found" });
        return Ok(detail);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(PurchaseDetails details)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _context.AddAsync(details);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = details.Id }, details);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PurchaseDetails details)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = await _context.PurchaseDetails.FindAsync(id);
        
        if (existing == null)
            return NotFound(new { message = "Purchase not found" });
        
        if(details.PurchaseId != 0 )
            existing.PurchaseId = details.PurchaseId;
            
        if (details.ProductId != 0 )
            existing.ProductId = details.ProductId;
        
        existing.Quantity = details.Quantity;
        existing.UnitPrice = details.UnitPrice;
        existing.SubTotal = details.SubTotal;
        
        
        await _context.SaveChangesAsync();
        return Ok(existing);


    }
[HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

        var detail = await _context.PurchaseDetails.FindAsync(id);
        
        
        if (detail == null)
            return NotFound(new { message = "Purchase not found" });

        _context.PurchaseDetails.Remove(detail);
        await _context.SaveChangesAsync();


        return Ok(detail);


    }
    
    
    
    
    
    
}