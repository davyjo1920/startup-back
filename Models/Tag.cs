public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } // например, "Тантра", "Body-to-body"
    public ICollection<PrivateTag>? Privates { get; set; }
}
