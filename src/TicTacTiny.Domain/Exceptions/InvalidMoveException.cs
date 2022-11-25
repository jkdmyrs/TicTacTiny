using System;
namespace jkdmyrs.TicTacTiny.Domain.Exceptions
{
	public class InvalidMoveException : Exception
	{
        internal InvalidMoveException(string message) : base(message) { }
	}
}

