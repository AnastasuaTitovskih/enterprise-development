var builder = DistributedApplication.CreateBuilder(args);

var bikeRentalDb = builder
    .AddPostgres("bike-rental-db")
    .AddDatabase("bike-rental");

builder.AddProject<Projects.BikeRental_Api_Host>("bikerental-api-host")
    .WithReference(bikeRentalDb, "DatabaseConnection")
    .WaitFor(bikeRentalDb);

builder.Build().Run();
