using EETMovie.Core.CreateMetadata;
using EETMovie.Core.GetMetadata;
using EETMovie.Core.GetStats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EETMovie.API.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMediator _mediator;

    public MovieController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("/metadata")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateMetadata(CreateMetadataRequest createMetadataRequest)
    {
        CreateMetadataResponse response = await _mediator.Send(createMetadataRequest);
        return StatusCode((int) response.HttpStatusCode, response.SuccessMessage);
    }

    [HttpGet("/movies/stats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStatisticsForAllMovies()
    {
        GetStatsResponse response = await _mediator.Send(new GetStatsRequest());
        return StatusCode((int) response.HttpStatusCode, response.MovieStatisticsList);
    }
    
    [HttpGet("/metadata/{movieId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMetadata(int movieId)
    {
        GetMetadataResponse response = await _mediator.Send(new GetMetadataRequest(movieId));
        return StatusCode((int) response.HttpStatusCode, response);
    }
}