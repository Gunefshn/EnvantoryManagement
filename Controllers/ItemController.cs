using EnvantoryManagement.Data;
using EnvantoryManagement.Models.DTOs;
using EnvantoryManagement.Models.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnvantoryManagement.Controllers;

[ApiController]
[Route("/item")]

public class ItemController(AppDbContext context): ControllerBase
{
    [HttpGet]
    [Route("{id:int:min(1)}")]
    [ProducesResponseType<ItemDto[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
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
}