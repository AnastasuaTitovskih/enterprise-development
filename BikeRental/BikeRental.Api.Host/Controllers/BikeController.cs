using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Bike;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления велосипедами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikeController(
    ILogger<BikeController> logger,
    IApplicationService<BikeDto, BikeCreateUpdateDto, int> service)
    : CrudControllerBase<BikeDto, BikeCreateUpdateDto>(logger, service);