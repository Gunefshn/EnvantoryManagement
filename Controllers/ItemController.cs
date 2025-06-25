using EnvantoryManagement.Data;
using EnvantoryManagement.Models.DTOs;
using EnvantoryManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnvantoryManagement.Controllers;

[ApiController]
[Route("/item")]
public class ItemController(AppDbContext context): ControllerBase
{
    [HttpGet]
    [ProducesResponseType<ItemDto[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get()
    {
        var items = context.Items
            .Include(i => i.Tags)
            .Include(i => i.Container)
            .ToList();
        var tags = items.Select(x => x.Tags).ToArray();

        var result = items.Select(item => new ItemDto
        {
            ContainerName = item.Container.Name,
            Name = item.Name,
            Quantity = item.Quantity,
            Tags = item.Tags.Select(t => t.Name).ToArray()
        }).ToArray();

        return Ok(result); 
    }
    
    [HttpGet("search")]
    [ProducesResponseType(typeof(ItemDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Search([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest("Arama metni boş olamaz.");

        var items = context.Items
            .Include(i => i.Tags)
            .Include(i => i.Container)
            .Where(i => i.Name.ToLower().Contains(query.ToLower()))
            .ToList();

        if (!items.Any())
            return NotFound("Aranan ifadeye uygun ürün bulunamadı.");

        var result = items.Select(item => new ItemDto
        {
            Name = item.Name,
            Quantity = item.Quantity,
            ContainerName = item.Container?.Name,
            Tags = item.Tags.Select(t => t.Name).ToArray()
        }).ToList();

        return Ok(result);
    }
    [HttpGet("expired")]
    public IActionResult GetExpiredItems()
    {
        var today = DateTime.Today;

        var items = context.Items
            .Include(i => i.Tags)
            .Include(i => i.Container)
            .Where(i => i.ExpiryDate.HasValue && i.ExpiryDate < today)
            .ToList();

        var result = items.Select(item => new ItemDto
        {
            Name = item.Name,
            Quantity = item.Quantity,
            ContainerName = item.Container?.Name,
            Tags = item.Tags.Select(t => t.Name).ToArray(),
            ExpiryDate = item.ExpiryDate
        }).ToList();

        return Ok(result);
    }
    [HttpGet("expiring-soon")]
    public IActionResult GetExpiringSoonItems()
    {
        var today = DateTime.Today;
        var threshold = today.AddDays(7);

        var items = context.Items
            .Include(i => i.Tags)
            .Include(i => i.Container)
            .Where(i => i.ExpiryDate.HasValue && i.ExpiryDate >= today && i.ExpiryDate <= threshold)
            .ToList();

        var result = items.Select(item => new ItemDto
        {
            Name = item.Name,
            Quantity = item.Quantity,
            ContainerName = item.Container?.Name,
            Tags = item.Tags.Select(t => t.Name).ToArray(),
            ExpiryDate = item.ExpiryDate
        }).ToList();

        return Ok(result);
    }
    [HttpGet("with-expiry")]
    public IActionResult GetItemsWithExpiry()
    {
        var items = context.Items
            .Include(i => i.Tags)
            .Include(i => i.Container)
            .Where(i => i.ExpiryDate.HasValue)
            .ToList();

        var result = items.Select(item => new ItemDto
        {
            Name = item.Name,
            Quantity = item.Quantity,
            ContainerName = item.Container?.Name,
            Tags = item.Tags.Select(t => t.Name).ToArray(),
            ExpiryDate = item.ExpiryDate
        }).ToList();

        return Ok(result);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(ItemCreateDto dto)
    {
        var container = context.Containers.Find(dto.ContainerId);
        if (container == null)
            return BadRequest("ContainerId geçersiz.");
    
        var tags = context.ItemTags.Where(t => dto.Tags.Contains(t.Name)).ToList();
    
        var item = new Item
        {
            Name = dto.Name,
            Quantity = dto.Quantity,
            ContainerId = dto.ContainerId,
            Tags = tags,
            Created = dto.Created
        };
    
        context.Items.Add(item);
        context.SaveChanges();
    
        return CreatedAtAction(nameof(Get), null);
    }
    
    [HttpPut("{id}/tags")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AssignTags(int id, ItemAssignTagsDto dto)
    {
        var item = context.Items.Include(i => i.Tags).FirstOrDefault(i => i.Id == id);
        if (item == null)
            return NotFound();
    
        var tags = context.ItemTags.Where(t => dto.TagIds.Contains(t.Id)).ToList();
    
        foreach (var tag in tags)
        {
            if (!item.Tags.Any(t => t.Id == tag.Id))
                item.Tags.Add(tag);
        }
    
        context.SaveChanges();
        return NoContent();
    }
    
    [HttpPut("{id}/container")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AssignContainer(int id, ItemAssignContainerDto dto)
    {
        var item = context.Items.Find(id);
        if (item == null)
            return NotFound();
    
        var container = context.Containers.Find(dto.ContainerId);
        if (container == null)
            return BadRequest("Container bulunamadı.");
    
        item.ContainerId = dto.ContainerId;
        item.Updated = DateTime.Now;
    
        context.SaveChanges();
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var item = context.Items.Find(id);
        if (item == null)
            return NotFound();
    
        context.Items.Remove(item);
        context.SaveChanges();
    
        return NoContent();
    }
}