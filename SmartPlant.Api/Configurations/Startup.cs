using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;

namespace SmartPlant.Api.Configurations
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            // Configura la sección de opciones de la aplicación para que use la configuración de DatabaseSettings
            //services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = int.MaxValue; // Opcional: establece el límite de tamaño del cuerpo multipart
            });

        }

    }
}
