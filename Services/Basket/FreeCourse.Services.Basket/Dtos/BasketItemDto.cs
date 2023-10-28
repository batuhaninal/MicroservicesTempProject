namespace FreeCourse.Services.Basket.Dtos
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }

        public void Update(string courseId, string courseName, decimal price)
        {
            CourseId = courseId;
            CourseName = courseName;
            Price = price;
        }
    }
}
