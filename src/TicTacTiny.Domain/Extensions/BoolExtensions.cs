using System;
namespace jkdmyrs.TicTacTiny.Domain.Extensions
{
	public static class BoolExtensions
	{
		public static string ToPlayerCopyText(this bool player)
		{
			return player == true ? "X" : "O";
		}
	}
}

