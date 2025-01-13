namespace TodoApi.Models;

public class PrivateRegisterRequestDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime BirthDate { get; set; }
}