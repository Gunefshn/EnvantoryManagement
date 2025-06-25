using System.ComponentModel.DataAnnotations;

namespace EnvantoryManagement.Models.Entities;

public class Tag
{
    public int Id { get; set; }
    [Required,MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Item> ItemTags { get; set; } 
    
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
}