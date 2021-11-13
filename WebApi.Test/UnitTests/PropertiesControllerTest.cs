using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.DTOs;
using WebApi.Models;

namespace WebApi.Test.UnitTests
{
    [TestClass]
    public class PropertiesControllerTest : BaseTests
    {
        [TestMethod]
        public async Task GetPropertyTest()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.Properties.Add(new Property()
            {
                Name = "farm",
                Address = "Av 50",
                Price = 50000,
                CodeInternal = "001",
                Year = 2010,
                IdOwner = 1,
            });

            await contexto.SaveChangesAsync();

            var controller = new PropertiesController(contexto);
            var respuesta = await controller.GetProperty(1);

            var resultado = respuesta.Value;
            Assert.AreEqual(1, resultado.IdProperty);
        }

        [TestMethod]
        public async Task GetNoExit()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            var controller = new PropertiesController(contexto);
            var result = await controller.GetProperty(1);

            var resultstatus = result.Result as StatusCodeResult;
            Assert.AreEqual(404, resultstatus.StatusCode);
        }



        [TestMethod]
        public async Task CreateProperty()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            var property = new PropertyDTO()
            {
                Name = "farm",
                Address = "Av 50",
                Price = 50000,
                CodeInternal = "001",
                Year = 2010,
                IdOwner = 1,
            };


            var controller = new PropertiesController(contexto);

            var respuesta = await controller.PostProperty(property);


            var contexto2 = ConstruirContext(nombreBD);
            Assert.AreEqual(1, contexto2.Properties.Count());
        }

        [TestMethod]
        public async Task UpdatePrice()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            var property = new PropertyDTO()
            {
                Name = "farm",
                Address = "Av 50",
                Price = 50000,
                CodeInternal = "001",
                Year = 2010,
                IdOwner = 1,
            };


            var controller = new PropertiesController(contexto);

            await controller.PostProperty(property);

            await controller.PutPropertyPrice(1, 1000);

            var contexto2 = ConstruirContext(nombreBD);

            var exist = contexto2.Properties.Where(x => x.Price == 1000);
            Assert.AreEqual(1, exist.Count());
        }
    }
}
