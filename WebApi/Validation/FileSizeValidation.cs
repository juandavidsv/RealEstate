using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Validation
{
    public class FileSizeValidation : ValidationAttribute
    {
        private readonly int maxMegaBytes;

        public FileSizeValidation(int megaBytes)
        {
            maxMegaBytes = megaBytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (formFile.Length > maxMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"The file size must not be larger than {maxMegaBytes}mb");
            }

            return ValidationResult.Success;
        }


    }
}
