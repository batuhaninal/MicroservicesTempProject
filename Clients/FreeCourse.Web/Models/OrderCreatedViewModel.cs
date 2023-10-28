namespace FreeCourse.Web.Models;

public class OrderCreatedViewModel
{
    public int OrderId { get; set; }
    public string Error { get; set; }
    public bool IsSuccessfull { get; set; }
}