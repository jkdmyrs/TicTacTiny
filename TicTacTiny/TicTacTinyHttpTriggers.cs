using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using jkdmyrs.TicTacTiny.Domain;
using jkdmyrs.TicTacTiny.Infrastructure.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace jkdmyrs.TicTacTiny
{
    public class TicTacTiny
    {
        private readonly ILogger _logger;

        public TicTacTiny(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TicTacTiny>();
        }

        [Function(nameof(NewGame))]
        public async Task<HttpResponseData> NewGame([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "game/create")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync(DomainConstants.NEW_GAME).ConfigureAwait(false);
            return response;
        }

        [Function(nameof(Move))]
        public async Task<HttpResponseData> Move(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "game/{gameId}/move/player/{player:int}/position/{position:int}")] HttpRequestData req,
            string gameId,
            int player,
            int position,
            CancellationToken ct = default)
        {
            if (!(player == 1 || player == 0))
            {
                return await req.CreateBadRequestAsync(
                    string.Format(ErrorTextConstants.INVALID_PLAYER_FMT, player),
                    ct
                ).ConfigureAwait(false);
            }
            if (position < 0 || position > 8)
            {
                return await req.CreateBadRequestAsync(
                    string.Format(ErrorTextConstants.INVALID_POSITION_FMT, position),
                    ct
                ).ConfigureAwait(false);
            }
            return await req.CreateStringResponseAsync(
                HttpStatusCode.OK,
                new Board(gameId).Move(player == 1, position).ToString()
            ).ConfigureAwait(false);
        }
    }
}
