using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MediaManager.Core.Models;

public class Role : IdentityRole
{
    [Required]
    public string? Description { get; set; }
}