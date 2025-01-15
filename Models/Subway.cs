public class Subway
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public int CityId { get; set; }
    public ICollection<PrivateSubway>? Privates { get; set; }
}
