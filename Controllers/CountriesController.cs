using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_reservation_api.Core.DTOs.Country;
using hotel_reservation_api.Core.Interfaces;
using Microsoft.AspNetCore.Authorization; // নতুন নামস্পেস যুক্ত করা হয়েছে
using Microsoft.AspNetCore.Mvc;

namespace hotel_reservation_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _countryService.GetAllAsync();
        return Ok(countries);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var country = await _countryService.GetByIdAsync(id);
        if (country == null) return NotFound($"Country with Id {id} not found.");

        return Ok(country);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")] // শুধুমাত্র Admin ক্রিয়েট করতে পারবে
    public async Task<IActionResult> Create([FromBody] CreateCountryDto dto)
    {
        var createdCountry = await _countryService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdCountry.Id }, createdCountry);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")] // শুধুমাত্র Admin আপডেট করতে পারবে
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCountryDto dto)
    {
        var isUpdated = await _countryService.UpdateAsync(id, dto);
        if (!isUpdated) return NotFound($"Country with Id {id} not found.");

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")] // শুধুমাত্র Admin ডিলিট করতে পারবে
    public async Task<IActionResult> Delete(int id)
    {
        var isDeleted = await _countryService.DeleteAsync(id);
        if (!isDeleted) return NotFound($"Country with Id {id} not found.");

        return NoContent();
    }
}