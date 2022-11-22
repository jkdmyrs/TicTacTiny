namespace jkdmyrs.TicTacTiny.Infrastructure
{
	public class GameRoomEntity
	{
		public GameRoomEntity() { }

		public GameRoomEntity(string gameCode, string roomId, string? securePassword = null)
		{
			RoomId = roomId ?? throw new ArgumentNullException(nameof(roomId));
            GameCode = gameCode ?? throw new ArgumentNullException(nameof(gameCode));
			if (securePassword is not null && string.IsNullOrWhiteSpace(securePassword))
            {
                throw new ArgumentException("Password cannot be an empty string/whitespace.", nameof(securePassword));
            }
			SecurePassword = securePassword ?? string.Empty;
		}

		public string RoomId { get; set; }
		public string SecurePassword { get; set; }
		public string GameCode { get; set; }
		public bool Secured => SecurePassword != string.Empty;
	}
}

