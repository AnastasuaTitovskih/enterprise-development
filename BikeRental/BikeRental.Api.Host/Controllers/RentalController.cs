using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Rental;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления записями аренды.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentalController(
    ILogger<RentalController> logger,
    IApplicationService<RentalDto, RentalCreateUpdateDto, int> service)
    : CrudControllerBase<RentalDto, RentalCreateUpdateDto>(logger, service);