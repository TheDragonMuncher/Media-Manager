using System;
using System.ComponentModel.DataAnnotations;

namespace MediaManager.MVC.Models;

public class RegisterViewModel
{ 
    [Required]
    public string UserName { get; set; } = string.Empty;
   
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;
}
