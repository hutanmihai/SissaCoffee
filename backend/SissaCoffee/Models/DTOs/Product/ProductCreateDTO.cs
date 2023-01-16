namespace SissaCoffee.Models.DTOs.Product
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public List<Guid> Ingredients { get; set; }
        public Guid ProductVariant { get; set; }
    }
}