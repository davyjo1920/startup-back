using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using BCrypt.Net;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly MarketplaceContext _context;
    private readonly JwtService _jwtService;

    public AuthController(MarketplaceContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("private/register")]
    public async Task<PrivateRegisterResponseDTO> RegisterPrivate([FromBody] PrivateRegisterRequestDTO privateRegisterRequestDTO)
    {
        // попробуем убрать, без него в постмане чет не заводилось
        privateRegisterRequestDTO.BirthDate = DateTime.SpecifyKind(privateRegisterRequestDTO.BirthDate, DateTimeKind.Utc);

        if (_context.Privates.Any(p => p.Login == privateRegisterRequestDTO.Login))
            return new PrivateRegisterResponseDTO{ Success = false, Message = "Логин занят" };

        if (DateTime.Now.Year - privateRegisterRequestDTO.BirthDate.Year < 18 ){
            return new PrivateRegisterResponseDTO{ Success = false, Message = "Малая еще" };
        }

        try {
            _context.Privates.Add(new Private
            {
                Login = privateRegisterRequestDTO.Login,
                PasswordHash = privateRegisterRequestDTO.Password,
                BirthDate = privateRegisterRequestDTO.BirthDate
            });
            await _context.SaveChangesAsync();

            return new PrivateRegisterResponseDTO{ Success = true };
        }
        catch {
            return new PrivateRegisterResponseDTO{ Success = false };
        }
    }

    [HttpPost("login")]
    public async Task<PrivateLoginResponseDTO> Login([FromBody] PrivateLoginRequestDTO model)
    {
        var user = _context.Privates.SingleOrDefault(u => u.Login == model.Login);
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            return new PrivateLoginResponseDTO{ Success = false };

        var token = _jwtService.GenerateJwtToken(user.Login);
        return new PrivateLoginResponseDTO{ Success = true, Token = token };
    }
}