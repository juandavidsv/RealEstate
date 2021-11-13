using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Test
{
    public class BaseTests
    {

        protected DB_RealEstateContext ConstruirContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<DB_RealEstateContext>()
                .UseInMemoryDatabase(nombreDB).Options;

            var dbContext = new DB_RealEstateContext(opciones);
            return dbContext;
        }

    }
}
