using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Renter;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления арендаторами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RenterController(
    ILogger<RenterController> logger,
    IApplicationService<RenterDto, RenterCreateUpdateDto, int> service)
    : CrudControllerBase<RenterDto, RenterCreateUpdateDto>(logger, service);