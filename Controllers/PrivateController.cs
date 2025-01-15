using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoApi.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class PrivateController : ControllerBase
{
    private readonly MarketplaceContext _context;
    private readonly IMapper _mapper;

    public PrivateController(MarketplaceContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<List<PrivateResponseDTO>> GetProfiles(){
        var list = await _context.Privates
            .Where(x => x.Status == PrivateStatus.Active)
            .Select(x => _mapper.Map<PrivateResponseDTO>(x))
            .ToListAsync();

        return list;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfile(int id){
        var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var privateDbModel = await _context.Privates.FindAsync(id);

        if (privateDbModel == null)
        {
            return NotFound();
        }

        var model = await _context.Privates
            .Where(x => x.Id == id)
            .Where(x => username == privateDbModel.Login || x.Status == PrivateStatus.Active)
            .Select(x => _mapper.Map<PrivateResponseDTO>(x))
            .FirstOrDefaultAsync();

        return Ok(model);
    }


    [Authorize(Roles = "Private")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem([FromBody] Private model, int id)
    {
        var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var privateDbModel = await _context.Privates.FindAsync(id);

        if (privateDbModel == null)
        {
            return NotFound();
        }

        if (username != privateDbModel.Login)
            return Unauthorized();

        privateDbModel.Name = model.Name;
        privateDbModel.Description = model.Description;
        privateDbModel.Telegram = model.Telegram;
        privateDbModel.WhatsApp = model.WhatsApp;
        privateDbModel.CityId = model.CityId;
        privateDbModel.Latitude = model.Latitude;






        // ?? КАК быть с Тегами???






        privateDbModel.Tags = model.Tags;

        await _context.SaveChangesAsync();

        return Ok(privateDbModel);
    }
}