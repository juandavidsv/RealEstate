using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Validation
{
    public class FileTypeValidation : ValidationAttribute
    {
        private readonly string[] typeValid;

        public FileTypeValidation(string[] tiposValidos)
        {
            this.typeValid = tiposValidos;
        }

        public FileTypeValidation(FileType fileType)
        {
            if (fileType == FileType.image)
            {
                typeValid = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
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

            if (!typeValid.Contains(formFile.ContentType))
            {
                return new ValidationResult($"The file type must be one of the following: {string.Join(", ", typeValid)}");
            }

            return ValidationResult.Success;
        }

    }
}
