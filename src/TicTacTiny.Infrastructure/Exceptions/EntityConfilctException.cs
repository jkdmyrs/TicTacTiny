using System;
using jkdmyrs.TicTacTiny.Domain;

namespace jkdmyrs.TicTacTiny.Infrastructure.Exceptions
{
	public class EntityConfilctException : Exception
	{
		public EntityConfilctException()
			: base(CopyTextConstants.ROOM_IN_USE)
		{

		}
	}
}

