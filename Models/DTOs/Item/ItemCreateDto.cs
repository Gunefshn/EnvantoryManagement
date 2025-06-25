namespace EnvantoryManagement.Models.DTOs;

public class ItemCreateDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int LocationId { get; set; }
    public List<string> Tags { get; set; }
}