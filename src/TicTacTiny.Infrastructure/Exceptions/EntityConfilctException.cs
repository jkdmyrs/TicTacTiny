using System;
namespace jkdmyrs.TicTacTiny.Infrastructure.Exceptions
{
	public class EntityConfilctException : Exception
	{
		public EntityConfilctException()
			: base("Room name is use. Pick a different name.")
		{

		}
	}
}

