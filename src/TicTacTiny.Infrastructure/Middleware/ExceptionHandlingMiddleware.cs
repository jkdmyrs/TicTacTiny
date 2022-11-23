using System;
using System.Net;
using jkdmyrs.TicTacTiny.Domain.Exceptions;
using jkdmyrs.TicTacTiny.Infrastructure.Exceptions;
using jkdmyrs.TicTacTiny.Infrastructure.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace jkdmyrs.TicTacTiny.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                async Task<HttpResponseData> GetResponseAsync(HttpRequestData req, Exception e)
                {
                    switch (e)
                    {
                        // 400
                        case InvalidBoardException:
                        case InvalidMoveException:
                            return await req.CreateStatusCodeResultAsync(HttpStatusCode.BadRequest, e.Message);
                        // 401
                        case InvalidPasswordException:
                            return await req.CreateStatusCodeResultAsync(HttpStatusCode.Unauthorized);
                        // 404
                        case EntityNotFoundException:
                            return await req.CreateStatusCodeResultAsync(HttpStatusCode.NotFound, e.Message);
                        // 409
                        case EntityConfilctException:
                            return await req.CreateStatusCodeResultAsync(HttpStatusCode.Conflict, e.Message);
                        // 500
                        default:
                            return await req.CreateStatusCodeResultAsync(HttpStatusCode.InternalServerError);
                    }
                }
                HttpRequestData? req = await context.GetHttpRequestDataAsync();
                if (req is null)
                {
                    throw;
                }
                else
                {
                    var response = await GetResponseAsync(req, e?.InnerException ?? new Exception());
                    context.SetInvocationResult(response);
                }
            }
        }
    }
}

