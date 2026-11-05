using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_reservation_api.Core.DTOs.Hotel;
using hotel_reservation_api.Core.Interfaces;
using Microsoft.AspNetCore.Authorization; // অথোরাইজেশন ব্যবহারের জন্য নামস্পেস যুক্ত করা হয়েছে
using Microsoft.AspNetCore.Mvc;

namespace hotel_reservation_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var hotels = await _hotelService.GetAllAsync();
        return Ok(hotels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var hotel = await _hotelService.GetByIdAsync(id);
        if (hotel == null) return NotFound($"Hotel with Id {id} not found.");

        return Ok(hotel);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")] // শুধুমাত্র Admin নতুন হোটেল তৈরি করতে পারবে
    public async Task<IActionResult> Create([FromBody] CreateHotelDto dto)
    {
        var createdHotel = await _hotelService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdHotel.Id }, createdHotel);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")] // শুধুমাত্র Admin হোটেল আপডেট করতে পারবে
    public async Task<IActionResult> Update(int id, [FromBody] UpdateHotelDto dto)
    {
        var isUpdated = await _hotelService.UpdateAsync(id, dto);
        if (!isUpdated) return NotFound($"Hotel with Id {id} not found.");

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")] // শুধুমাত্র Admin হোটেল ডিলিট করতে পারবে
    public async Task<IActionResult> Delete(int id)
    {
        var isDeleted = await _hotelService.DeleteAsync(id);
        if (!isDeleted) return NotFound($"Hotel with Id {id} not found.");

        return NoContent();
    }
}