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
/// Property Images Controller
/// </summary>
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class PropertyImagesController : ControllerBase
    {
        private readonly DB_RealEstateContext _context;

        public PropertyImagesController(DB_RealEstateContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Get Property Images
        /// </summary>
        /// <returns></returns>
        // GET: api/PropertyImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyImage>>> GetPropertyImages()
        {
            return await _context.PropertyImages.ToListAsync();
        }

        /// <summary>
        /// Get Property Image
        /// </summary>
        /// <param name="idPropertyImage">idPropertyImage </param>
        /// <returns></returns>
        // GET: api/PropertyImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyImage>> GetPropertyImage(int idPropertyImage)
        {
            var propertyImage = await _context.PropertyImages.Where(e => e.IdPropertyImage == idPropertyImage).FirstOrDefaultAsync();

            if (propertyImage == null)
            {
                return NotFound();
            }

            return propertyImage;
        }

        /// <summary>
        /// Add Image from property
        /// </summary>
        /// <param name="propertyImage"></param>
        /// <returns>GetPropertyImage</returns>
        // POST: api/PropertyImages

        [HttpPost]
        public async Task<ActionResult<PropertyImage>> PostPropertyImage(PropertyImageDTO propertyImageDTO)
        {

            PropertyImage propertyImage = new PropertyImage()
            {
                IdProperty = propertyImageDTO.IdProperty,
                File =  propertyImageDTO.File,
                Enabled =  propertyImageDTO.Enabled,
            };
            _context.PropertyImages.Add(propertyImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPropertyImage", new { id = propertyImage.IdPropertyImage }, propertyImage);
        }

    }
}
