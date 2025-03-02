using ManagementSystem.DAL.SqlServer;
using ManagementSystem.Application;
using ManagementSystemProject.Middlewares;
using ManagementSystemProject.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("MyConn");

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSqlServerServices(connectionString!);
builder.Services.AddApplicationServices();
builder.Services.AddAuthenticationDependency(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseMiddleware<RateLimitMiddleware>(2, TimeSpan.FromMinutes(1));
app.UseMiddleware<ExceptionHandlerMiddleware>();
//Custom middlewares

app.MapControllers();

app.Run();
