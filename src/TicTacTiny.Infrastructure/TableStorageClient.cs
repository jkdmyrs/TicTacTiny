using Azure.Data.Tables;

namespace jkdmyrs.TicTacTiny.Infrastructure
{
	public class TableStorageClient : IStorageClient
    {
        private readonly TableServiceClient _client;
        private readonly Lazy<TableClient> _gameRoomTable;

        public TableStorageClient(TableServiceClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));

            _gameRoomTable = new Lazy<TableClient>(() =>
            {
                var table = _client.GetTableClient(TableStorageConstants.TABLE_NAME_GAME_ROOM);
                table.CreateIfNotExists();
                return table;
            });
        }

        public async Task<GameRoomEntity> GetGameRoomAsync(string roomId, CancellationToken ct = default)
        {
            return await _gameRoomTable.Value.GetEntityAsync<GameRoomEntity>(roomId, roomId, cancellationToken: ct).ConfigureAwait(false); 
        }

        private async Task UpsertGameRoomAsync(GameRoomEntity entity, CancellationToken ct = default)
        {
            await _gameRoomTable.Value.UpsertEntityAsync(entity, cancellationToken: ct).ConfigureAwait(false);
        }

        public async Task UpsertGameRoomAsync(string gameCode, string roomId, byte[]? securepass = null, CancellationToken ct = default)
        {
            await this.UpsertGameRoomAsync(new GameRoomEntity(gameCode, roomId, securepass), ct).ConfigureAwait(false);
        }

        public async Task DeleteGameRoomAsync(string roomId, CancellationToken ct = default)
        {
            await _gameRoomTable.Value.DeleteEntityAsync(roomId, roomId, cancellationToken: ct).ConfigureAwait(false);
        }
    }
}

