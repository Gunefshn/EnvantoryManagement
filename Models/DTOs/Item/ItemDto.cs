namespace EnvantoryManagement.Models.DTOs;

public class ItemDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string ContainerName { get; set; }
    public string[] Tags { get; set; }
    public DateTime? ExpiryDate {get; set;} //SKT

    
}