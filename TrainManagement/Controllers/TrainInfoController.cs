using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainManagement.Data;
using TrainManagement.Dtos;
using TrainManagement.Helpers;
using TrainManagement.Managers;

namespace TrainManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainInfoController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly ITrainRepo _trainRepo;
    private readonly ICarReception _carReception;
    private readonly IReportFileGenerator _reportFileGenerator;

    public TrainInfoController(ILogger<TrainInfoController> logger, IMapper mapper, ITrainRepo trainRepo, ICarReception carReception, IReportFileGenerator reportFileGenerator)
    {
        _logger = logger;
        _mapper = mapper;
        _trainRepo = trainRepo;
        _carReception = carReception;
        _reportFileGenerator = reportFileGenerator;
    }

    [HttpPost]
    [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue)]
    [Authorize("User")]
    public async Task<ActionResult> AddList([FromForm]IFormFile file)
    {
        if (file.ContentType != "text/xml")
        {
            return BadRequest("Wrong file format");
        }

        await using (var stream = file.OpenReadStream())
        {
            var cars = XMLHelper.ParseStream(stream);
            await _carReception.ReceiveRange(cars);
        }
        
        _logger.LogInformation($"--> Post file {file.FileName}");
        return Ok($"Received file {file.FileName} {file.ContentType} with size in bytes {file.Length}");
    }
    
    [HttpGet]
    [Route("excel")]
    [Authorize("User")]
    public async Task<ActionResult> GetTrainInfoFile([FromQuery]int trainNumber)
    {
        var train = await _trainRepo.GetTrainByNumber(trainNumber);
        if (train == null)
            return NoContent();
        
        _logger.LogInformation("--> Get train with number {arg0} as xlsx", trainNumber);

        var trainReport = _mapper.Map<TrainReport>(train);
        var data = await _reportFileGenerator.GetAsByteArray(trainReport);

        return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"NL_{trainNumber}.xlsx");
    }
    
    [HttpGet]
    [Route("json")]
    [Authorize("User")]
    public async Task<ActionResult<TrainReport>> GetTrainInfo([FromQuery]int trainNumber)
    {
        var train = await _trainRepo.GetTrainByNumber(trainNumber);
        if (train == null)
            return NoContent();
        
        _logger.LogInformation("--> Get train with number {arg0} as json", trainNumber);

        var trainReport = _mapper.Map<TrainReport>(train);
        
        return Ok(trainReport);
    }
}