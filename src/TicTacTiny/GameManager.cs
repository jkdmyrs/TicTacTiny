using jkdmyrs.TicTacTiny.Domain;
using jkdmyrs.TicTacTiny.Infrastructure;
using jkdmyrs.TicTacTiny.Infrastructure.Extensions;

namespace jkdmyrs.TicTacTiny
{
    public class GameManager
    {
        private readonly IStorageClient _client;

        public GameManager(IStorageClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client)); 
        }

        public async Task CreateGameAsnc(string roomId, string rawpass, CancellationToken ct = default)
        {
            byte[]? pass = null;
            if (!string.IsNullOrWhiteSpace(rawpass))
            {
                pass = Security.SecurePassword(rawpass);
            }
            await _client.UpsertGameRoomAsync(DomainConstants.NEW_GAME, roomId, pass, ct).ConfigureAwait(false);
        }

        public async Task<Game> MakeMoveAsync(string roomId, bool player, int position, string rawpass, CancellationToken ct = default)
        {
            var room = await this.GetGameRoomAsync(roomId, rawpass, ct).ConfigureAwait(false);
            var game = room.ToGame().Move(player, position);
            if (game.HasWinner)
            {
                await _client.DeleteGameRoomAsync(roomId, ct).ConfigureAwait(false);
            }
            else
            {
                await _client.UpsertGameRoomAsync(game.ToString(), roomId, room.SecurePassword, ct).ConfigureAwait(false);
            }
            return game;
        }

        private async Task<GameRoomEntity> GetGameRoomAsync(string roomId, string rawpass, CancellationToken ct = default)
        {
            var gameroom = await _client.GetGameRoomAsync(roomId, ct).ConfigureAwait(false);
            if (gameroom.SecurePassword is not null && !Security.VerifyPassword(rawpass, gameroom.SecurePassword))
            {
                throw new Exception("Invalid password.");
            }
            return gameroom;
        }
    }
}
