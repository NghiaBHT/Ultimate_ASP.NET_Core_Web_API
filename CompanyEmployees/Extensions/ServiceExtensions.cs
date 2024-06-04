using Contracts;
using LoggerService;
using Repository;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        /************************************/
        //CORS (Cross-Origin Resource Sharing) is a mechanism
        //to give or restrict access rights to applications from different domains.
        /************************************/
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                /*Basic CORS policy settings*/
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin() //method which allows requests from any source
                    .AllowAnyMethod()//allows all HTTP methods
                    .AllowAnyHeader());//method by using

                /*
                 options.AddPolicy("CorsPolicy", builder =>
                    builder.WithOrigins("https://example.com") //-> which will allow requests only from that concrete source
                    .WithMethods("POST", "GET")//-> allow only specific HTTP methods.
                    .WithHeaders("accept", "contenttype"));// -> allow only specific headers.
                */
            });

        /************************************/
        //ASP.NET Core applications are by default self-hosted, and if we want to 
        //host our application on IIS, we need to configure an IIS integration which
        //will eventually help us with the deployment to IIS.
        /************************************/
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(option =>
            {

            });

        public static void ConfigureLoggerService(this IServiceCollection services) => 
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}
