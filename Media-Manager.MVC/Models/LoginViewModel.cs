using System;
using System.ComponentModel.DataAnnotations;

namespace MediaManager.MVC.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
