using System.Net;
using jkdmyrs.TicTacTiny.Domain;
using jkdmyrs.TicTacTiny.Infrastructure;
using jkdmyrs.TicTacTiny.Infrastructure.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace jkdmyrs.TicTacTiny.API
{
    public class TicTacTiny
    {
        private readonly ILogger _logger;
        private readonly GameManager _manager;

        public TicTacTiny(ILoggerFactory loggerFactory, GameManager gameManager)
        {
            _logger = loggerFactory.CreateLogger<TicTacTiny>();
            _manager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }

        [Function(nameof(NewGame))]
        public async Task<HttpResponseData> NewGame([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "room/create")] HttpRequestData req,
            CancellationToken ct)
        {
            var request = await req.ReadFromJsonAsync<CreateGameRequest>(ct).ConfigureAwait(false);
            if (request is null || string.IsNullOrWhiteSpace(request.RoomId))
            {
                return await req.CreateStatusCodeResultAsync(
                    HttpStatusCode.BadRequest,
                    $"Invalid {nameof(CreateGameRequest)} request.",
                    ct
                ).ConfigureAwait(false);
            }
            await _manager.CreateGameAsnc(request.RoomId, request.Password, ct).ConfigureAwait(false);
            return await req.CreateStringResultAsync(DomainConstants.NEW_GAME, ct).ConfigureAwait(false);
        }

        [Function(nameof(Move))]
        public async Task<HttpResponseData> Move(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "room/{roomId}/player/{player:int}/position/{position:int}")] HttpRequestData req,
            string roomId,
            int player,
            int position,
            CancellationToken ct = default)
        {
            var game = await _manager.MakeMoveAsync(roomId, player, position, req.GetRawPassword(), ct).ConfigureAwait(false);
            return await req.CreateStringResultAsync(game.ToString()).ConfigureAwait(false);
        }
    }
}
