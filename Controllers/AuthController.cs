using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;

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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(privateRegisterRequestDTO.Password),
                BirthDate = privateRegisterRequestDTO.BirthDate,
                Status = PrivateStatus.New
            });
            await _context.SaveChangesAsync();

            return new PrivateRegisterResponseDTO{ Success = true };
        }
        catch {
            return new PrivateRegisterResponseDTO{ Success = false };
        }
    }

    [HttpPost("private/login")]
    public async Task<PrivateLoginResponseDTO> LoginPrivate([FromBody] PrivateLoginRequestDTO model)
    {
        var user = _context.Privates.SingleOrDefault(u => u.Login == model.Login);
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            return new PrivateLoginResponseDTO{ Success = false };

        var token = _jwtService.GenerateJwtToken(user.Login, "Private");
        return new PrivateLoginResponseDTO{ Success = true, Token = token };
    }

    [HttpPost("admin/login")]
    public async Task<AdminLoginResponseDTO> LoginAdmin([FromBody] AdminLoginRequestDTO model)
    {
       if (model.Login != "Mamoeb3000" || model.Password != "SosyBiby@s0bake11"){
        return new AdminLoginResponseDTO{ Success = false };
       }
       
       var token = _jwtService.GenerateJwtToken(model.Login, "Admin");
       return new AdminLoginResponseDTO{ Success = true, Token = token };
    }

    // Тестовый метод для проверки авторизации admin
    [Authorize(Roles = "Admin")]
    [HttpGet("testAuthAdmin")]
    public string TestAdminLMethod(long id)
    {
       return "OK";
    }

    // Тестовый метод для проверки авторизации private
    [Authorize(Roles = "Private")]
    [HttpGet("testAuthPrivate")]
    public string TestPrivateMethod(long id)
    {
       return "OK";
    }
}