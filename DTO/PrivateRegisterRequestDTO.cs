namespace TodoApi.Models;

public class PrivateRegisterRequestDTO
{
    public string Login { get; set; }
    public string Password { get; set; }
    public DateTime BirthDate { get; set; }
}