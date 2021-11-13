using System;
using System.Collections.Generic;

#nullable disable
/// <summary>
/// Dto property filters
/// </summary>
namespace WebApi.Models
{
    public partial class PropertyFilterDTO
    {
        public PropertyFilterDTO()
        {
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public decimal PriceBeginning { get; set; }
        public decimal PriceEnd { get; set; }
        public string CodeInternal { get; set; }
        public short YearBeginning { get; set; }
        public short YearEnd { get; set; }
        public int IdOwner { get; set; }
    }
}
