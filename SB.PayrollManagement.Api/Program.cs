using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SB.PayrollManagement.Api.Extentions;
using SB.PayrollManagement.Application.Interfaces.Repositories;
using SB.PayrollManagement.Application.Interfaces.Services;
using SB.PayrollManagement.Application.Services;
using SB.PayrollManagement.Persistence.Context;
using SB.PayrollManagement.Persistence.Repositories;
using System.Text;
using System.Text.Json;

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

    const string FrontendCorsPolicy = "FrontendCorsPolicy";
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(FrontendCorsPolicy, policy =>
        {
            policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

    builder.Services.AddDbContext<PayrollManagementContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbTask")));

    builder.Services.AddScoped<IGovermentRepository, GovernmentRepository>();
    builder.Services.AddScoped<IGovernmentService, GovernmentService>();

    builder.Services.AddScoped<IUsersRepository, UsersRepository>();
    builder.Services.AddScoped<IRolesRepository, RolesRepository>();
    builder.Services.AddScoped<IUsersService, UsersService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IRoleService, RoleService>();

    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();

    builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
    builder.Services.AddScoped<IDepartmentService, DepartmentService>();

    builder.Services.AddScoped<ISalariedEmployeeRepository, SalariedEmployeeRepository>();
    builder.Services.AddScoped<ISalariedEmployeeService, SalariedEmployeeService>();

    builder.Services.AddScoped<IHourlyEmployeeRepository, HourlyEmployeeRepository>();
    builder.Services.AddScoped<IHourlyEmployeeService, HourlyEmployeeService>();

    builder.Services.AddScoped<ICommissionEmployeeRepository, CommissionEmployeeRepository>();
    builder.Services.AddScoped<ICommissionEmployeeService, CommissionEmployeeService>();

    builder.Services.AddScoped<ISalariedCommissionEmployeeRepository, SalariedCommissionEmployeeRepository>();
    builder.Services.AddScoped<ISalariedCommissionEmployeeService, SalariedCommissionEmployeeService>();

    builder.Services.AddScoped<IEmployeeTypeRepository, EmployeeTypeRepository>();
    builder.Services.AddScoped<IEmployeeTypeService, EmployeeTypeService>();
    builder.Services.AddScoped<IPayrollRecordRepository, PayrollRecordRepository>();
    builder.Services.AddScoped<IPayrollService, PayrollService>();

    var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is not configured");
    var jwtIssuer = builder.Configuration["Jwt:Issuer"];
    var jwtAudience = builder.Configuration["Jwt:Audience"];

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (context.Request.Cookies.TryGetValue("access_token", out var token))
                    {
                        context.Token = token;
                    }
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    context.HttpContext.Items["AuthError"] = context.Exception is SecurityTokenExpiredException
                        ? "The token has expired"
                        : "Invalid token";
                    return Task.CompletedTask;
                },
                OnChallenge = async context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var message = context.HttpContext.Items["AuthError"] as string ?? "No valid authentication token was provided";
                    var payload = JsonSerializer.Serialize(new { success = false, message });
                    await context.Response.WriteAsync(payload);
                }
            };
        });

    builder.Services.AddAuthorization();

    var app = builder.Build();

    app.UseGlobalExceptionHandler();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors(FrontendCorsPolicy);

    app.UseAuthentication();
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
