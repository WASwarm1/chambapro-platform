using Chambapro_backend.IAM.Domain.Repositories;
using Chambapro_backend.IAM.Infrastructure.Persistence.EFC.Repositories;
using ChambaPro.Platform.API.Reservation.Application.Internal.CommandServices;
using ChambaPro.Platform.API.Reservation.Application.Internal.QueryServices;
using ChambaPro.Platform.API.Reservation.Domain.Repositories;
using ChambaPro.Platform.API.Reservation.Domain.Services;
using ChambaPro.Platform.API.Reservation.Infrastructure.Persistence.EFC.Repositories;
using ChambaPro.Platform.API.Shared.Domain.Repositories;
using ChambaPro.Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using ChambaPro.Platform.API.Shared.Infrastructure.Mediator.Cortex.Configuration;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Chambapro.Platform.API.User.Domain.Model.Aggregates;
using Chambapro.Platform.API.User.Domain.Repositories;
using Chambapro.Platform.API.User.Infrastructure.Persistence.EF;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Ensure repositories that ask for DbContext (base type) can resolve the registered AppDbContext
builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<AppDbContext>());

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "AWSwarm.ChambaPro.API",
            Version = "v1",
            Description = "AWSwarm ChambaPro Platform API",
            TermsOfService = new Uri("https://awsawm-chambapro.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "AWSawm",
                Email = "contact@awswarm.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//TODO: Per BC config

builder.Services.AddScoped<IReserveCommandService, ReserveCommandService>();
builder.Services.AddScoped<IReserveQueryService, ReserveQueryService>();
builder.Services.AddScoped<IReserveRepository, ReserveRepository>();

builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();



builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: new[] { typeof(Program) }, configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
        //options.AddDefaultBehaviors();
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

//app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();