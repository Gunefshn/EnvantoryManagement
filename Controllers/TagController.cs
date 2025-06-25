using EnvantoryManagement.Data;
using EnvantoryManagement.Models.DTOs;
using EnvantoryManagement.Models.DTOs.Tag;
using EnvantoryManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnvantoryManagement.Controllers;
[ApiController]
[Route("/Tag")]

public class TagController(AppDbContext context): ControllerBase
{
   [HttpPost]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public IActionResult Create(TagCreateDto dto)
   {
      var tag = new Tag
      {
         Name = dto.Name,
         Created = DateTime.Now
      };
      context.ItemTags.Add(tag);
      context.SaveChanges();

      // var result = new TagDto()
      // {
      //    Id = tag.Id,
      //    Name = tag.Name
      // };
      return Ok(tag.Id);
   }
   
   [HttpGet("by-tag/{tagName}")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   public IActionResult GetItemByTag(string tagName)
   {
      var items = context.Items
         .Include(i => i.Tags)
         .Where(i => i.Tags.Any(t => t.Name.ToLower() == tagName.ToLower()))
         .ToList();

      if (!items.Any())
      {
         return NotFound();
      }
      var result = items.Select(item => new ItemListDto
      {
         Name = item.Name,
         
      }).ToArray();
      return Ok(result);
   }
   
}