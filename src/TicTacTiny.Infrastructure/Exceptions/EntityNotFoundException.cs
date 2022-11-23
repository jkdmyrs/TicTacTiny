using System;
namespace jkdmyrs.TicTacTiny.Infrastructure.Exceptions
{
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException()
			:base("Room does not exist.")
		{

		}
	}
}

