namespace noswebapp_api.RequestEntities;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Challenge { get; set; }

}