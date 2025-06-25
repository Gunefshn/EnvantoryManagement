namespace EnvantoryManagement.Models.DTOs;

public class ItemCreateDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int ContainerId { get; set; }
    public List<string> Tags { get; set; }
    public DateTime Created { get; set; }=DateTime.Now;
}