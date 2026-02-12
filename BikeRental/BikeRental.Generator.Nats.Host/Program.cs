using BikeRental.Generator.Nats.Host.Configuration;
using BikeRental.Generator.Nats.Host.Generator;
using BikeRental.Generator.Nats.Host.Producer;
using BikeRental.Generator.Nats.Host.Worker;
using BikeRental.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.Configure<GeneratorSettings>(
    builder.Configuration.GetSection("Generator"));

builder.Services.Configure<NatsProducerSettings>(
    builder.Configuration.GetSection("NatsProducer"));

builder.AddNatsClient("nats");

builder.Services.AddSingleton<RentalGenerator>();

builder.Services.AddSingleton<RentalProducer>();

builder.Services.AddHostedService<RentalGeneratorWorker>();

var app = builder.Build();

app.Run();