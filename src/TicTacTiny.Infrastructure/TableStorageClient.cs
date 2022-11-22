namespace jkdmyrs.TicTacTiny.Infrastructure
{
	public class TableStorageClient : IStorageClient
    {
        public Task<GameRoomEntity> GetGameRoomAsync(string roomId, string? securePassword = null)
        {
            throw new NotImplementedException();
        }

        public Task UpsertGameRoomAsync(string gameCode, string roomId, string? securePassword = null)
        {
            throw new NotImplementedException();
        }
    }
}

