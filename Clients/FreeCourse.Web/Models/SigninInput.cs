using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models;

public class SigninInput
{
    [Display(Name = "Email Adresiniz")]
    public string Email { get; set; }
    [Display(Name = "Sifreniz")]
    public string Password { get; set; }
    [Display(Name = "Beni Hatirla")]
    public bool IsRemember { get; set; }
}