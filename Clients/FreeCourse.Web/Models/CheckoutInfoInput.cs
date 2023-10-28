using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models;

public class CheckoutInfoInput
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
    [Display(Name = "Kart Isim Soyisim")]
    public string CardName { get; set; }
    [Display(Name = "Kart Numarasi")]
    public string CardNumber { get; set; }
    [Display(Name = "Son Kullanma Tarihi")]
    public string Expiration { get; set; }
    [Display(Name = "CVV/CVC2")]
    public string CVV { get; set; }
}