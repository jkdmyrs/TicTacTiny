namespace jkdmyrs.TicTacTiny.Infrastructure
{
	public interface IStorageClient
    {
        Task UpsertGameRoomAsync(string gameCode, string roomId, byte[]? securepass = null, CancellationToken ct = default);
        Task<GameRoomEntity> GetGameRoomAsync(string roomId, CancellationToken ct = default);
        Task DeleteGameRoomAsync(string roomId, CancellationToken ct = default);
	}
}

