using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models;

public class AddressCreateInput
{
    [Display(Name = "Il")]
    public string Province { get; set; }
    [Display(Name = "Ilce")]
    public string District { get; set; }
    [Display(Name = "Cadde")]
    public string Street { get; set; }
    [Display(Name = "Posta Kodu")]
    public string ZipCode { get; set; }
    [Display(Name = "Adres")]
    public string Line { get; set; }
}