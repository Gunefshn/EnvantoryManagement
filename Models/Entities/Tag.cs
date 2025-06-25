using System.ComponentModel.DataAnnotations;

namespace EnvantoryManagement.Models.Entities;

public class ItemTag
{
    public int Id { get; set; }
    [Required,MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public ICollection<ItemTag>ItemTags { get; set; } = new List<Item>();
    
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
}