using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models;

public class CourseCreateInput
{
    [Display(Name = "Kurs Ismi")]
    public string Name { get; set; }
    [Display(Name = "Kurs Aciklama")]
    public string Description { get; set; }
    [Display(Name = "Kurs Fiyati")]
    public decimal Price { get; set; }
    public string? UserId { get; set; }
    public string? Picture { get; set; }
    public FeatureViewModel Feature { get; set; }
    [Display(Name = "Kurs Kategorisi")]
    public string CategoryId { get; set; }
    [Display(Name = "Resim")]
    public IFormFile? PhotoFormFile { get; set; }
}