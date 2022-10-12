namespace noswebapp_api.Models;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Challenge { get; set; }

    [Required]
    public long TimeStamp { get; set; }
}