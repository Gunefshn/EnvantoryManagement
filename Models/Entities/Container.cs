using System.ComponentModel.DataAnnotations;

namespace EnvantoryManagement.Models.Entities;

public class Container
{
    public int Id { get; set; }
    [Required,MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public ICollection<Item> Items { get; set; } =new List<Item>();
    
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } 
}