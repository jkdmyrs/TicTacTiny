using System;
using jkdmyrs.TicTacTiny.Domain;

namespace jkdmyrs.TicTacTiny.Infrastructure.Exceptions
{
	public class EntityNotFoundException : Exception
	{
        internal EntityNotFoundException()
			:base(CopyTextConstants.ROOM_DOES_NOT_EXIST)
		{

		}
	}
}

