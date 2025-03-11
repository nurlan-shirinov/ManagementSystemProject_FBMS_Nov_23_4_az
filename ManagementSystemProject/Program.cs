using ManagementSystem.Application;
using ManagementSystem.Application.Security;
using ManagementSystem.DAL.SqlServer;
using ManagementSystemProject.Infrastructure;
using ManagementSystemProject.Middlewares;
using ManagementSystemProject.Security;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);



    //builder.Logging.ClearProviders();
    //builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    //builder.Host.UseNLog();



    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerService();

    var connectionString = builder.Configuration.GetConnectionString("MyConn");

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddSqlServerServices(connectionString!);
    builder.Services.AddApplicationServices();
    builder.Services.AddAuthenticationDependency(builder.Configuration);
    builder.Services.AddScoped<IUserContext, HttpUserContext>();
    builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();


    //app.UseMiddleware<RateLimitMiddleware>(2, TimeSpan.FromMinutes(1));
    app.UseMiddleware<ExceptionHandlerMiddleware>();
    //Custom middlewares

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex, "Error occured while program started");
	throw;
}
finally
{
    LogManager.Shutdown();
}
