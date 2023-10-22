using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models;

public class CourseUpdateInput
{
    public string Id { get; set; }
    
    [Display(Name = "Kurs Ismi")]
    [Required]
    public string Name { get; set; }
    [Display(Name = "Kurs Aciklama")]
    [Required]
    public string Description { get; set; }
    [Display(Name = "Kurs Fiyati")]
    [Required]
    public decimal Price { get; set; }
    public string? UserId { get; set; }
    public string? Picture { get; set; }
    public FeatureViewModel Feature { get; set; }
    [Display(Name = "Kurs Kategorisi")]
    [Required]
    public string CategoryId { get; set; }
    [Display(Name = "Resim")]
    public IFormFile? PhotoFormFile { get; set; }
}