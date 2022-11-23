using Azure;
using Azure.Data.Tables;

namespace jkdmyrs.TicTacTiny.Infrastructure
{
	public class GameRoomEntity : ITableEntity
	{
		public GameRoomEntity() { }

		public GameRoomEntity(string gameCode, string roomId, byte[]? securepass = null)
		{
			RoomId = roomId ?? throw new ArgumentNullException(nameof(roomId));
			PartitionKey = roomId;
			RowKey = roomId;
            GameCode = gameCode ?? throw new ArgumentNullException(nameof(gameCode));
			if (securepass is null)
            {
				SecurePassword = null;
            }
			else
            {
				SecurePassword = securepass;
            }
		}

		public string RoomId { get; init; }
		public byte[]? SecurePassword { get; init; }
		public string GameCode { get; init; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}

