namespace SissaCoffee.Models.DTOs;

public class ProductDTO
{
    public string Name { get; set; } = String.Empty;

    public int Size { get; set; } = 0;
    public string Unit { get; set; } = String.Empty;
    public string? TagName { get; set; } = String.Empty;
    public IList<string> Ingredients { get; set; } = new List<string>();
}