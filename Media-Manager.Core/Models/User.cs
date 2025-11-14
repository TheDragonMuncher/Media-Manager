using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MediaManager.Core.Models;

public class User : IdentityUser
{
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogIn { get; set; }
    public ICollection<MediaObject>? MediaObjects { get; set; }
}