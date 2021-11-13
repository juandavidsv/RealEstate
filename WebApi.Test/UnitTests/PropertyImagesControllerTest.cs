using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.DTOs;

namespace WebApi.Test.UnitTests
{
    [TestClass]
    public class PropertyImagesControllerTest : BaseTests
    {
        [TestMethod]
        public async Task CrearActorConFoto()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            var content = Encoding.UTF8.GetBytes("test image");
            var file = new FormFile(new MemoryStream(content), 0, content.Length, "Data", "imagen.jpg");
            file.Headers = new HeaderDictionary();
            file.ContentType = "image/jpg";

            var property = new PropertyDTO()
            {
                Name = "farm",
                Address = "Av 50",
                Price = 50000,
                CodeInternal = "001",
                Year = 2010,
                IdOwner = 1,
            };


            var controllerPro = new PropertiesController(contexto);
            var ResultPro =controllerPro.PostProperty(property);   

            var propertyImageDTO = new PropertyImageDTO()
            {
                IdProperty = 1,
                Enabled = true,
                File = file
            };

            var controller = new PropertyImagesController(contexto);

            var respuesta = await controller.PostPropertyImage(propertyImageDTO);
            var contexto2 = ConstruirContext(nombreBD);

            var exist = contexto2.PropertyImages.Where(x => x.IdPropertyImage == 1);
            Assert.AreEqual(1, exist.Count());
        }
    }
}
