namespace TodoApi.Models;

public class AdminLoginResponseDTO
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public string? Message { get; set; }
}