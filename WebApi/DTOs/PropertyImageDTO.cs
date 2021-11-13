using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Validation;

#nullable disable

namespace WebApi.DTOs
{
    public partial class PropertyImageDTO
    {

        [Required]
        public int IdProperty { get; set; }
        [Required]
        [FileSizeValidation(4)]
        [FileTypeValidation(FileType.image)]
        public IFormFile File { get; set; }
       
        public bool Enabled { get; set; }

    }
}
