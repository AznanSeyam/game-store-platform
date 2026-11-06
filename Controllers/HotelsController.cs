using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_reservation_api.Core.DTOs.Hotel;
using hotel_reservation_api.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hotel_reservation_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController(IHotelService hotelService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var hotels = await hotelService.GetAllAsync();
        return Ok(hotels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var hotel = await hotelService.GetByIdAsync(id);
        if (hotel == null) return NotFound($"Hotel with Id {id} not found.");

        return Ok(hotel);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateHotelDto dto)
    {
        var createdHotel = await hotelService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdHotel.Id }, createdHotel);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateHotelDto dto)
    {
        var isUpdated = await hotelService.UpdateAsync(id, dto);
        if (!isUpdated) return NotFound($"Hotel with Id {id} not found.");

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var isDeleted = await hotelService.DeleteAsync(id);
        if (!isDeleted) return NotFound($"Hotel with Id {id} not found.");

        return NoContent();
    }
}