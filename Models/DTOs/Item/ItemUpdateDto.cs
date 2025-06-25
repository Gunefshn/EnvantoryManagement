namespace EnvantoryManagement.Models.DTOs;

public class ItemUpdateDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int ContainerId { get; set; }
    public List<string> Tags { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}