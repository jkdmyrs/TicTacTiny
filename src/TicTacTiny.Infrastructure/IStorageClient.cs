namespace jkdmyrs.TicTacTiny.Infrastructure
{
	public interface IStorageClient
    {
        Task UpsertGameRoomAsync(string gameCode, string roomId, string? securePassword = null);
        Task<GameRoomEntity> GetGameRoomAsync(string roomId, string? securePassword = null);
	}
}

