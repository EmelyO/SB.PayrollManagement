using Microsoft.EntityFrameworkCore;
using Serilog;
using SB.PayrollManagement.Api.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Application.Services;
using SB.PayrollManagement.Persistence.Context;
using SB.PayrollManagement.Persistence.Repositories;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<PayrollManagementContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbTask")));

    builder.Services.AddScoped<IGovermentRepository, GovernmentRepository>();
    builder.Services.AddScoped<IGovernmentService, GovernmentService>();

    var app = builder.Build();

    app.UseGlobalExceptionHandler();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "La aplicación terminó inesperadamente");
}
finally
{
    Log.CloseAndFlush();
}
