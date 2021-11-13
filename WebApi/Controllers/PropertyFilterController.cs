using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
/// <summary>
/// controller property with filters
/// </summary>
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class PropertyFilterController : ControllerBase
    {

        private readonly DB_RealEstateContext _context;

        public PropertyFilterController(DB_RealEstateContext context)
        {
            _context = context;
        }

        /// <summary>
        /// List property with filters
        /// parameter default in int = 0 in string Emptry
        /// </summary>
        /// <param name="propertyDTO">property</param>
        /// <returns>List Property </returns>
        [HttpPost("PropertyFilter")]
        public async Task<ActionResult<List<Property>>> GetPropertyFilter([FromBody] PropertyFilterDTO @propertyDTO)
        {
            List<Property> @property = await _context.Properties.Where(x =>
                           (string.IsNullOrEmpty(@propertyDTO.Address) || x.Address.Contains(@propertyDTO.Address))
                            && (@propertyDTO.IdOwner == 0 || x.IdOwner == @propertyDTO.IdOwner)
                            && (string.IsNullOrEmpty(@propertyDTO.Name) || x.Name.Contains(@propertyDTO.Name))
                            && (@propertyDTO.PriceBeginning == 0 || (x.Price >= @propertyDTO.PriceBeginning))
                            && (@propertyDTO.PriceEnd == 0 || (x.Price <= @propertyDTO.PriceEnd))
                            && (@propertyDTO.YearBeginning == 0 || (x.Year >= @propertyDTO.YearBeginning))
                            && (@propertyDTO.YearEnd == 0 || (x.Year <= @propertyDTO.YearEnd))
                            ).ToListAsync();

            if (@property == null)
            {
                return NotFound();
            }

            return @property;
        }

    }
}
