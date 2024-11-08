using Gelf.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Troas.Customer.Application.DbServices;
using Troas.Customer.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

// Configure Application Insights telemetry collection.
builder.Services.AddApplicationInsightsTelemetry();

// Configure GELF for Graylog
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddGelf(options =>
{
    options.AdditionalFields = new Dictionary<string, object>
    {
        {"facility", builder.Configuration.GetSection("Logging")["GELF:Facility"]},
        {"Environment", builder.Configuration.GetSection("Logging")["GELF:Environment"]},
        {"machine_name", Environment.MachineName}
    };
    options.Host = builder.Configuration.GetSection("Logging")["GELF:Host"];
    options.LogSource = builder.Configuration.GetSection("Logging")["GELF:LogSource"];
    options.Port = int.Parse(builder.Configuration.GetSection("Logging")["GELF:Port"]);
}));

//Configure OpenTelemetry Tracing using Jaeger
builder.Services.AddOpenTelemetry()
    .WithTracing(b => b.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName))
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(opts => opts.Endpoint = new Uri("http://localhost:4317")));

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgresConnection"),
        b => b.MigrationsAssembly("Troas.Customer.Infrastructure")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
        
// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI();
        
app.UseHttpsRedirection();
app.UseAuthorization();
        
app.MapControllers();

app.Run();

public partial class Program { } // Make Program class partial for testing purposes
