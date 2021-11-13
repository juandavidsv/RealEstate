using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Test.UnitTests
{
    [TestClass]
    public class AccountControllerTest : BaseTests
    {

        [TestMethod]
        public async Task UserNoLogon()
        {
            var nombreBD = Guid.NewGuid().ToString();
          
            var controller = ConstruirCuentasController(nombreBD);
            var userInfo = new UserInfo() { Email = "ejemplo@hotmail.com", Password = "malPassword" };
            var respuesta = await controller.Login(userInfo);

            Assert.IsNull(respuesta.Value);
            var resultado = respuesta.Result as BadRequestObjectResult;
            Assert.IsNotNull(resultado);
        }


        [TestMethod]
        public async Task UserLogon()
        {
            var nombreBD = Guid.NewGuid().ToString();

            var controller = ConstruirCuentasController(nombreBD);
            var userInfo = new UserInfo() { Email = "juandavidsv@gmail.com", Password = "bueno" };
            var respuesta = await controller.Login(userInfo);

            Assert.IsNotNull(respuesta.Value);
          
            Assert.IsNotNull(respuesta.Value.Token);
        }

        private AccountController ConstruirCuentasController(string nombreBD)
        {
          
            var miConfiguracion = new Dictionary<string, string>
            {
                {"JWT:key", "ASDJKNKJN4KJ5N4KJSNDKAJSNDAKSJDNK4J5N43KJ53N4KSNDJASKLDNAS"},
                 {"User:Email", "juandavidsv@gmail.com"},
                 {"User:Pass", "bueno"}
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(miConfiguracion)
                .Build();

            return new AccountController( configuration);
        }

    }
}
