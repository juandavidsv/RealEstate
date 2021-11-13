using System;
using System.Collections.Generic;

#nullable disable
/// <summary>
/// Dto property filters
/// </summary>
namespace WebApi.DTOs
{
    public partial class PropertyDTO
    {
        public PropertyDTO()
        {
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public short Year { get; set; }
        public int IdOwner { get; set; }
    }
}
