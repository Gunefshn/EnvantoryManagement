using System.ComponentModel.DataAnnotations;

namespace EnvantoryManagement.Models.Entities;

public class Location
{
    public int Id { get; set; }
    [Required,MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public ICollection<Container> Containers { get; set; } = new List<Container>();
    
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } 
}
