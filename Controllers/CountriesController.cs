using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_reservation_api.Core.DTOs.Country;
using hotel_reservation_api.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hotel_reservation_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController(ICountryService countryService) : ControllerBase
{


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = await countryService.GetAllAsync();
        return Ok(countries);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var country = await countryService.GetByIdAsync(id);
        if (country == null) return NotFound($"Country with Id {id} not found.");

        return Ok(country);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCountryDto dto)
    {
        var createdCountry = await countryService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdCountry.Id }, createdCountry);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCountryDto dto)
    {
        var isUpdated = await countryService.UpdateAsync(id, dto);
        if (!isUpdated) return NotFound($"Country with Id {id} not found.");

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var isDeleted = await countryService.DeleteAsync(id);
        if (!isDeleted) return NotFound($"Country with Id {id} not found.");

        return NoContent();
    }
}