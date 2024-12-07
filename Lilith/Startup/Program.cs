using Stella.Core.ErrorHandling;
using Stella.Core.DocumentStore;
using Microsoft.Extensions.Options;
using EnPassant.Lilith.Services;


var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

    services.AddSingleton<MongoDbContext>();
    services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
    services.AddSingleton(typeof(IDocumentStore<>), typeof(DocumentStore<>));
    services.AddSingleton<IUserService, UserService>();
}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    // app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}

