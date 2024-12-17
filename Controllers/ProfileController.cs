using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class ProfilesController : ControllerBase
{
    private static List<Profile> Profiles = new List<Profile>
    {
        new Profile { Id = 1, Name = "John Doe", Description = "A software engineer", PhotoUrl = "https://via.placeholder.com/150" },
        new Profile { Id = 2, Name = "Jane Smith", Description = "A graphic designer", PhotoUrl = "https://via.placeholder.com/150" }
    };

    [HttpGet]
    public IActionResult GetProfiles() => Ok(Profiles);

    [HttpGet("{id}")]
    public IActionResult GetProfile(int id)
    {
        var profile = Profiles.Find(p => p.Id == id);
        if (profile == null) return NotFound();
        return Ok(profile);
    }

    [HttpPost]
    public IActionResult CreateProfile(Profile profile)
    {
        profile.Id = Profiles.Count + 1;
        Profiles.Add(profile);
        return CreatedAtAction(nameof(GetProfile), new { id = profile.Id }, profile);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProfile(int id)
    {
        var profile = Profiles.Find(p => p.Id == id);
        if (profile == null) return NotFound();
        Profiles.Remove(profile);
        return NoContent();
    }
}

public class Profile
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
}