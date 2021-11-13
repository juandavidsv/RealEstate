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
            byte[] imag = { 0x1, 0x2, 0x3, 0x4, 0x5 };
            var propertyImageDTO = new PropertyImageDTO()
            {
                IdProperty = 1,
                Enabled = true,
                File = imag
            };

            var controller = new PropertyImagesController(contexto);

            var respuesta = await controller.PostPropertyImage(propertyImageDTO);
            var contexto2 = ConstruirContext(nombreBD);

            var exist = contexto2.PropertyImages.Where(x => x.IdPropertyImage == 1);
            Assert.AreEqual(1, exist.Count());
        }
    }
}
