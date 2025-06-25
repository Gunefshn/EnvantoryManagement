namespace EnvantoryManagement.Models.DTOs;

public class ItemUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int LocationId { get; set; }
    public List<string> Tags { get; set; }
}