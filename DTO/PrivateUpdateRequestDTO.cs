public class PrivateUpdateRequestDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public DateTime BirthDate { get; set; }

    public string? Telegram { get; set; }
    public string? WhatsApp { get; set; }

    public int? CityId { get; set; }
    public City? City { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public string? District { get; set; }

    public List<int>? TagIds { get; set; }
    public List<int>? SubwayIds { get; set; }
}
