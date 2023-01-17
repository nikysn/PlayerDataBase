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
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;

    [HttpGet]
    [ProducesResponseType(typeof(GetPlayerResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        var players = PlayersRepository.GetPlayers();
        var response = new GetPlayerResponse
        {
            Players = players.Select(x => new PlayerDto
            {
                Id = x.Id,
                Name = x.Name,
                Level = x.Level,
                BanStatus = (PlayerDto.BanStatuses)x.BanStatus
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
    public async Task<IActionResult> Delete(int number)
    {
        PlayersRepository.DeletePlayer(number);
        return Ok();
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateBanStatus([FromBody][Required] UpdateBanStatusRequest  request)
    {
        PlayersRepository.ChangeBanStatus(request.number, request.action);
        return Ok();
    }
}

