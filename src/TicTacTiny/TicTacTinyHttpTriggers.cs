using System.Net;
using jkdmyrs.TicTacTiny.Domain;
using jkdmyrs.TicTacTiny.Infrastructure;
using jkdmyrs.TicTacTiny.Infrastructure.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace jkdmyrs.TicTacTiny
{
    public class TicTacTiny
    {
        private readonly ILogger _logger;
        private readonly IStorageClient _storage;

        public TicTacTiny(ILoggerFactory loggerFactory, IStorageClient storage)
        {
            _logger = loggerFactory.CreateLogger<TicTacTiny>();
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        [Function(nameof(NewGame))]
        public async Task<HttpResponseData> NewGame([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "room/create")] HttpRequestData req,
            CancellationToken ct)
        {
            var request = await req.ReadFromJsonAsync<CreateGameRequest>(ct).ConfigureAwait(false);
            if (request is null)
            {
                return await req.CreateBadRequestAsync(
                    $"Invalide {nameof(CreateGameRequest)} request.",
                    ct
                ).ConfigureAwait(false);
            }
            await _storage.UpsertGameRoomAsync(DomainConstants.NEW_GAME, request.RoomId, request.Password).ConfigureAwait(false);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync(DomainConstants.NEW_GAME).ConfigureAwait(false);
            return response;
        }

        [Function(nameof(Move))]
        public async Task<HttpResponseData> Move(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "room/{roomId}/player/{player:int}/position/{position:int}")] HttpRequestData req,
            string roomId,
            int player,
            int position,
            CancellationToken ct = default)
        {
            if (!(player == 1 || player == 0))
            {
                string error = string.Format(ErrorTextConstants.INVALID_PLAYER_FMT, player);
                return await req.CreateBadRequestAsync(error, ct).ConfigureAwait(false);
            }
            if (position < 0 || position > 8)
            {
                string error = string.Format(ErrorTextConstants.INVALID_POSITION_FMT, position);
                return await req.CreateBadRequestAsync(error, ct).ConfigureAwait(false);
            }

            var password = req.GetSecurePassword();

            var gameroom = await _storage.GetGameRoomAsync(roomId, password).ConfigureAwait(false);
            var newCode = gameroom
                .ToGame()
                .Move(player == 1, position)
                .ToString();

            await _storage.UpsertGameRoomAsync(newCode, roomId, password).ConfigureAwait(false);

            return await req.CreateStringResponseAsync(HttpStatusCode.OK, newCode).ConfigureAwait(false);
        }
    }
}
