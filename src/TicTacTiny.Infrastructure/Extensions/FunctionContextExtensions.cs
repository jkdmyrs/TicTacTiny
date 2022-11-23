using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace jkdmyrs.TicTacTiny.Infrastructure.Extensions
{
	public static class FunctionContextExtensions
	{
		public static void SetInvocationResult(this FunctionContext context, HttpResponseData httpResponse)
        {
			context.GetInvocationResult().Value = httpResponse;
        }
	}
}

