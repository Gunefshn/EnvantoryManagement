namespace EnvantoryManagement.Models.Entities;

public class Envantory
{
    public int Id { get; set; }                    
    public string Name { get; set; }             
    public int Quantity { get; set; }             
    public DateTime CreatedAt { get; set; }= DateTime.Now;  
    public DateTime UpdatedAt { get; set; }=DateTime.Now;
}