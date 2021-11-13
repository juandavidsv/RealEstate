using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApi.DTOs
{
    public partial class PropertyImageDTO
    {

        [Required]
        public int IdProperty { get; set; }
        [Required]
        public byte[] File { get; set; }
       
        public bool Enabled { get; set; }

    }
}
