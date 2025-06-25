using EnvantoryManagement.Data;
using EnvantoryManagement.Models.DTOs.Container;
using EnvantoryManagement.Models.DTOs.Location;
using EnvantoryManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnvantoryManagement.Controllers;

[ApiController]
[Route("/location")]
public class LocationController(AppDbContext context): ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Create(LocationCreateDto dto)
    {
        var location = new Location
        {
            Name = dto.Name,
            Created = dto.Created
        };

        context.Locations.Add(location);
        context.SaveChanges();

        return CreatedAtAction(nameof(GetContainers), new { id = location.Id }, null);
    }

    [HttpGet("{id}/containers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetContainers(int id)
    {
        var location = context.Locations
            .Include(l => l.Containers)
            .FirstOrDefault(l => l.Id == id);

        if (location == null)
            return NotFound();

        var result = location.Containers.Select(c => new ContainerDto
        {
            Id = c.Id,
            Name = c.Name,
            LocationId = c.LocationId,
            LocationName = location.Name
        }).ToList();

        return Ok(result);
    }
}