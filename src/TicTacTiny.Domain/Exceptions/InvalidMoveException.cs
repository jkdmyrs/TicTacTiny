using System;
namespace jkdmyrs.TicTacTiny.Domain.Exceptions
{
	public class InvalidMoveException : Exception
	{
		public InvalidMoveException(string message) : base(message) { }
	}
}

