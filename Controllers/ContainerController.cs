using EnvantoryManagement.Data;
using EnvantoryManagement.Models.DTOs;
using EnvantoryManagement.Models.DTOs.Container;
using EnvantoryManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnvantoryManagement.Controllers;

[ApiController]
[Route("/container")]
public class ContainerController(AppDbContext context): ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(ContainerCreateDto dto)
    {
        var container = new Container
        {
            Name = dto.Name,
            LocationId = dto.LocationId,
            Created = dto.Created
        };

        context.Containers.Add(container);
        context.SaveChanges();

        return CreatedAtAction(nameof(GetItems), new { id = container.Id }, null);
    }

    [HttpGet("{id}/items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetItems(int id)
    {
        var container = context.Containers
            .Include(c => c.Items)
            .ThenInclude(i => i.Tags)
            .FirstOrDefault(c => c.Id == id);

        if (container == null)
            return NotFound();

        var result = container.Items.Select(item => new ItemDto
        {
            Name = item.Name,
            Quantity = item.Quantity,
            ContainerName = container.Name,
            Tags = item.Tags.Select(t => t.Name).ToArray()
        }).ToList();

        return Ok(result);
    }

    [HttpPut("{id}/location/{locationId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateLocation(int id, int locationId)
    {
        var container = context.Containers.Find(id);
        var location = context.Locations.Find(locationId);

        if (container == null || location == null)
            return NotFound();

        container.LocationId = locationId;
        container.Updated = DateTime.Now;

        context.SaveChanges();
        return NoContent();
    }
}

