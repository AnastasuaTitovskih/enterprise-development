using BikeRental.Application;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.BikeModel;
using BikeRental.Application.Contracts.Rental;
using BikeRental.Application.Contracts.Renter;
using BikeRental.Application.Service;
using BikeRental.Domain;
using BikeRental.Domain.Model;
using BikeRental.Infrastructure.EfCore;
using BikeRental.Infrastructure.EfCore.Repository;
using BikeRental.Infrastructure.Nats;
using BikeRental.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new BikeRentalProfile());
});

builder.AddServiceDefaults();

builder.Services.AddScoped<IRepository<BikeModel, int>, BikeModelRepository>();
builder.Services.AddScoped<IRepository<Bike, int>, BikeRepository>();
builder.Services.AddScoped<IRepository<Rental, int>, RentalRepository>();
builder.Services.AddScoped<IRepository<Renter, int>, RenterRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IApplicationService<BikeModelDto, BikeModelCreateUpdateDto, int>, BikeModelService>();
builder.Services.AddScoped<IApplicationService<BikeDto, BikeCreateUpdateDto, int>, BikeService>();
builder.Services.AddScoped<IApplicationService<RentalDto, RentalCreateUpdateDto, int>, RentalService>();
builder.Services.AddScoped<IApplicationService<RenterDto, RenterCreateUpdateDto, int>, RenterService>();

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("BikeRental"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }

    c.UseInlineDefinitionsForEnums();
});

builder.AddNpgsqlDbContext<BikeRentalDbContext>("DatabaseConnection");

builder.AddNatsClient("nats");
builder.Services.Configure<NatsConsumerSettings>(builder.Configuration.GetSection("NatsConsumer"));
builder.Services.AddHostedService<RentalConsumer>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<BikeRentalDbContext>();

        await context.Database.MigrateAsync();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
