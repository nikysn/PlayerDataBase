using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using PlayerDataBase.API.Contracts;
using PlayerDataBase.DataAccess;

namespace PlayerDataBase.API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class PlayersController : ControllerBase
{
    private readonly ILogger<PlayersController> _logger;

    public PlayersController(ILogger<PlayersController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetPlayerResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        Player[] players = PlayersRepository.GetPlayers();
        var response = new GetPlayerResponse
        {
            Players = players.Select(x => new PlayerDto
            {
                Id = x.Id,
                Name = x.Name,
                Level = x.Level,
                BanStatus = x.BanStatus.ToString()
            }).ToArray()
        };
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create([FromBody] CreatePlayerRequest request)
    {
        var playerId = PlayersRepository.AddPlayer(request.Name,request.Level);
        return Ok(playerId);
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(Guid playerId)
    {
        PlayersRepository.DeletePlayer(playerId);
        return Ok();
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateBanStatus([FromBody][Required] UpdateBanStatusRequest  request)
    {
        PlayersRepository.ChangeBanStatus(request.PlayerId, request.Action);
        return Ok();
    }
}

