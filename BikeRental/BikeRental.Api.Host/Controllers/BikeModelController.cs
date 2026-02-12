using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.BikeModel;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления моделями велосипедов.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikeModelController(
    ILogger<BikeModelController> logger,
    IApplicationService<BikeModelDto, BikeModelCreateUpdateDto, int> service)
    : CrudControllerBase<BikeModelDto, BikeModelCreateUpdateDto>(logger, service);