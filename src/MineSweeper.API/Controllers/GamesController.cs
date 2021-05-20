﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MineSweeper.Application.Interfaces;
using MineSweeper.Application.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MineSweeper.Services.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        private readonly IGameAppService _service;

        public GamesController(ILogger<GamesController> logger,
                               IGameAppService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Create a game customizing number of rows, cols and mines
        /// </summary>
        /// <param name="game"></param>
        /// <returns>Created game id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateGame([FromBody]GameViewModel game)
        {
            try
            {
                Guid _gameId = await _service.CreateGame(game);

                return Ok(_gameId);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                    return BadRequest();

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Endpoint to visit a cell informating game id, number of row and col
        /// </summary>
        /// <param name="id"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>The result of the action to visit a cell</returns>
        [HttpPut("{id}/visit-cell/rows/{row}/cols/{col}")]
        [ProducesResponseType(typeof(VisitCellResultViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> VisitCell(Guid id, int row, int col)
        {
            try
            {
                VisitCellResultViewModel _result = await _service.VisitCell(id, row, col);

                return Ok(_result);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                    return BadRequest();

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
