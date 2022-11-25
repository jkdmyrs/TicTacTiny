using Azure;
using Azure.Data.Tables;

namespace jkdmyrs.TicTacTiny.Infrastructure
{
    public class GameRoomEntity : ITableEntity
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public GameRoomEntity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public GameRoomEntity(string gameCode, string roomId, byte[]? securepass = null)
		{
			RoomId = roomId;
			PartitionKey = roomId;
			RowKey = roomId;
            GameCode = gameCode;
			SecurePassword = securepass;
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

