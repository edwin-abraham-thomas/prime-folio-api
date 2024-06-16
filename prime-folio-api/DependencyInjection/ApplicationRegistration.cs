using DataAccess.Repositories;
using Interfaces.Repositories;
using Interfaces.Services;
using Services;

namespace DependencyInjection;

public static class ApplicationRegistration
{
    public static void RegisterServices(this IServiceCollection services, IHostEnvironment env, IConfiguration configuration)
    {
        services.Configure<MongoConfig>(config =>
        {
            config.MongoConnectionString = configuration.GetValue<string>("MongoConnectionString");
            config.MongoDatabaseName = configuration.GetValue<string>("MongoDatabaseName");
        });

        //Automapper
        services.AddAutoMapper(typeof(Program));

        //Services
        services.AddTransient<IUserService, UserService>();

        //Repositories
        services.AddTransient<IUserRepository, UserRepository>();
    }
}
