﻿using jkdmyrs.TicTacTiny.Domain;

namespace jkdmyrs.TicTacTiny.Infrastructure.Extensions
{
	public static class GameRoomEntityExtensions
	{
		public static Game ToGame(this GameRoomEntity entity)
		{
			return Game.FromCode(entity.GameCode);
		}
	}
}

