namespace EnvantoryManagement.Models.DTOs.Tag;

public class TagCreateDto
{
    public string Name { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}