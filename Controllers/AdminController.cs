using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoApi.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly MarketplaceContext _context;
    private readonly IMapper _mapper;

    public AdminController(MarketplaceContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("privates")]
    public async Task<List<PrivateResponseDTO>> GetProfiles(PrivateStatus? status){
        var list = await _context.Privates
            .Where(x => status == null || x.Status == status)
            .Select(x => _mapper.Map<PrivateResponseDTO>(x))
            .ToListAsync();

        return list;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("private/{id}")]
    public async Task<IActionResult> GetProfile(int id){
        var privateDbModel = await _context.Privates.FindAsync(id);

        if (privateDbModel == null)
        {
            return NotFound();
        }

        var model = await _context.Privates
            .Where(x => x.Id == id)
            .Select(x => _mapper.Map<PrivateResponseDTO>(x))
            .FirstOrDefaultAsync();

        return Ok(model);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("private/{id}")]
    public async Task<IActionResult> UpdateProfile([FromBody] bool approve, int id)
    {
        var privateDbModel = await _context.Privates
            .Include(x => x.Tags)
            .Include(x => x.Subways)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (privateDbModel == null)
        {
            return NotFound();
        }

        privateDbModel.Status = approve ? PrivateStatus.Active : PrivateStatus.Declined;

        await _context.SaveChangesAsync();

        return Ok();
    }
}