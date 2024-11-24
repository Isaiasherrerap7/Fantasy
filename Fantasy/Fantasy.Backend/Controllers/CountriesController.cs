using Fantasy.Backend.Data;
using Fantasy.Shared.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly DataContext _context;

    public CountriesController(DataContext context)
    {
        _context = context;
    }

    // Tomar lista de los paises
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _context.Countries.ToListAsync());
    }

    // Tomar uno de los paises
    // Nota existen tres formas de pasarles parametros a una api(1.ruta.2.body.3.query string)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        return Ok(country);
    }

    // Crear un pais
    [HttpPost]
    public async Task<IActionResult> PostAsync(Country country)
    {
        _context.Add(country);
        await _context.SaveChangesAsync();
        return Ok(country);
    }

    //  Actualziar un pais actual
    [HttpPut]
    public async Task<IActionResult> PutAsync(Country country)
    {
        var currentCountry = await _context.Countries.FindAsync(country.Id);
        if (currentCountry == null)
        {
            return NotFound();
        }

        currentCountry.Name = country.Name;
        _context.Update(currentCountry);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    //  Borrar un pais actual
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletAsync(int id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        _context.Remove(country);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}