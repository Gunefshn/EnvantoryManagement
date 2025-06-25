namespace EnvantoryManagement.Models.DTOs.Tag;

public class TagUpdateDto
{
    public string Name { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}