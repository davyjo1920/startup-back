public class PrivateResponseDTO
{
     public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public DateTime BirthDate { get; set; }

    public string? Telegram { get; set; }
    public string? WhatsApp { get; set; }

   public string Login { get; set; }
   public string PasswordHash { get; set; }

    public int? CityId { get; set; }
    public City? City { get; set; }

    // Координаты конкретного адреса (необязательно)
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public ICollection<Subway>? Subways { get; set; }
    public string? District { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public PrivateStatus? Status { get; set; }

    // Связь с тегами (многие-ко-многим)
    public ICollection<PrivateTag>? Tags { get; set; }
    public ICollection<Photo>? Photos { get; set; }
}
