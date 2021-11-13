using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Models;
/// <summary>
/// Controller Property
/// </summary>
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class PropertiesController : ControllerBase
    {
        private readonly DB_RealEstateContext _context;

        public PropertiesController(DB_RealEstateContext context)
        {
            _context = context;
        }

        /// <summary>
        /// fetch the elements of the properties entity 
        /// </summary>
        /// <param name="idProperty"> idProperty</param>
        /// <returns> ActionResult Property </returns>
        // GET: api/Properties/5
        [HttpGet("{idProperty}")]
        public async Task<ActionResult<Property>> GetProperty(int idProperty)
        {
            var @property = await _context.Properties.FindAsync(idProperty);

            if (@property == null)
            {
                return NotFound();
            }

            return @property;
        }

        /// <summary>
        /// Allows updating Change Price of  properties
        /// </summary>
        /// <param name="idProperty"> idProperty</param>
        /// <param name="property">entity properties</param>
        /// <returns></returns>
        // PUT: api/Properties/5/2,2
        [HttpPut("{idProperty},{Price}")]
        public async Task<IActionResult> PutPropertyPrice(int idProperty, decimal Price)
        {

            var @property = await _context.Properties.FindAsync(idProperty);

            if (@property == null)
            {
                return NotFound();
            }
            @property.Price = Price;
            _context.Entry(@property).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(idProperty))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Create Property 
        /// </summary>
        /// <param name="property">Entity Property </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Property>> PostProperty(PropertyDTO @property)
        {
            Property ObjDTO = new Property()
            {
                Name = @property.Name,
                Address = @property.Address,
                CodeInternal = @property.CodeInternal,
                Price = @property.Price,
                IdOwner = @property.IdOwner,
                Year = @property.Year,
            };

            _context.Properties.Add(ObjDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProperty", new { id = ObjDTO.IdProperty }, @property);
        }

        /// <summary>
        /// query Property Exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool PropertyExists(int id)
        {
            return _context.Properties.Any(e => e.IdProperty == id);
        }
    }
}
