using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Test;

namespace WebApi.Test.UnitTests
{
    [TestClass]
    public class PropertyFilterControllerTest : BaseTests

    {
        [TestMethod]
        public async Task GetAll()
        {
            // Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(Guid.NewGuid().ToString());

            await LoadData(contexto);

            // Prueba
            var controller = new PropertyFilterController(contexto);
            PropertyFilterDTO propertyFilterDTO = new PropertyFilterDTO()
            {
                Name = "",
                Address = "",
                PriceBeginning = 0,
                PriceEnd = 0,
                CodeInternal = "",
                YearEnd = 0,
                YearBeginning = 0,
                IdOwner = 0,
            };
            var respuesta = await controller.GetPropertyFilter(propertyFilterDTO);

            // Verificación
            var generos = respuesta.Value;
            Assert.AreEqual(4, generos.Count);
        }

        [TestMethod]
        public async Task GetFilterName()
        {
            // Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(Guid.NewGuid().ToString());

            await LoadData(contexto);

            // Prueba
            var controller = new PropertyFilterController(contexto);
            PropertyFilterDTO propertyFilterDTO = new PropertyFilterDTO()
            {
                Name = "Home2",
                Address = "",
                PriceBeginning = 0,
                PriceEnd = 0,
                CodeInternal = "",
                YearEnd = 0,
                YearBeginning = 0,
                IdOwner = 0,
            };
            var respuesta = await controller.GetPropertyFilter(propertyFilterDTO);

            // Verificación
            var generos = respuesta.Value;
            Assert.AreEqual(1, generos.Count);
        }
        [TestMethod]
        public async Task GetFilterPrice()
        {
            // Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(Guid.NewGuid().ToString());

            await LoadData(contexto);

            // Prueba
            var controller = new PropertyFilterController(contexto);
            PropertyFilterDTO propertyFilterDTO = new PropertyFilterDTO()
            {
                Name = "",
                Address = "",
                PriceBeginning = 10000,
                PriceEnd = 30000,
                CodeInternal = "",
                YearEnd = 0,
                YearBeginning = 0,
                IdOwner = 0,
            };
            var respuesta = await controller.GetPropertyFilter(propertyFilterDTO);

            // Verificación
            var generos = respuesta.Value;
            Assert.AreEqual(2, generos.Count);
        }
        private static async Task LoadData(Data.DB_RealEstateContext contexto)
        {
            contexto.Properties.Add(new Property()
            {
                Name = "Home",
                Address = "Av 50",
                Price = 10000,
                CodeInternal = "001",
                Year = 2010,
                IdOwner = 1,
            });
            contexto.Properties.Add(new Property()
            {
                Name = "Home2",
                Address = "Av 51",
                Price = 30000,
                CodeInternal = "002",
                Year = 2000,
                IdOwner = 2,
            });
            contexto.Properties.Add(new Property()
            {
                Name = "edifice",
                Address = "Av 50",
                Price = 100000,
                CodeInternal = "001",
                Year = 2010,
                IdOwner = 1,
            });
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
        }

       
    }
}
