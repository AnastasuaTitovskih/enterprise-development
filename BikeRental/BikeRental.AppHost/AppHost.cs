var builder = DistributedApplication.CreateBuilder(args);

var bikeRentalDb = builder
    .AddPostgres("bike-rental-db")
    .AddDatabase("bike-rental");

var natsUserName = builder.AddParameter("NatsLogin");
var natsPassword = builder.AddParameter("NatsPassword");

var nats = builder.AddNats("nats", userName: natsUserName, password: natsPassword, port: 4222)
    .WithJetStream()
    .WithArgs("-m", "8222")
    .WithHttpEndpoint(port: 8222, targetPort: 8222);

builder.AddContainer("nats-ui", "ghcr.io/nats-nui/nui")
    .WithReference(nats)
    .WaitFor(nats)
    .WithHttpEndpoint(port: 31311, targetPort: 31311);

builder.AddProject<Projects.BikeRental_Api_Host>("bikerental-api-host")
    .WithReference(bikeRentalDb, "DatabaseConnection")
    .WithReference(nats)
    .WaitFor(bikeRentalDb)
    .WaitFor(nats);

builder.Build().Run();
