using System;
using System.ComponentModel.DataAnnotations;
using MediaManager.Core.Models;

namespace Media_Manager.Core.Models;

public class Book
{
    public int id { get; set; }
    public string OLID { get; set; } = string.Empty; // open library id ** aka author_key in open library api **
    [Required]
    public string ISBN { get; set; } = string.Empty;
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string AuthorName { get; set; } = string.Empty;
    [Required]
    public int PublicationYear { get; set; }

    //Relations
    public int MediaObjectId { get; set; }
    public MediaObject MediaObject { get; set; }

}
