namespace Entities.Dtos
{
    public class CategoryPostCountDto
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public int PostCount { get; set; }
    }
}