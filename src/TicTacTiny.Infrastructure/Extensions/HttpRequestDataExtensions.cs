using System.Net;
using Microsoft.Azure.Functions.Worker.Http;

namespace jkdmyrs.TicTacTiny.Infrastructure.Extensions
{
    public static class HttpRequestDataExtensions
    {
        public static async Task<HttpResponseData> CreateStringResponseAsync(this HttpRequestData requestData, HttpStatusCode statusCode, string response)
        {
            var result = requestData.CreateResponse(statusCode);
            await result.WriteStringAsync(response).ConfigureAwait(false);
            return result;
        }

        public static async Task<HttpResponseData> CreateBadRequestAsync(this HttpRequestData requestData, string message, CancellationToken ct = default)
        {
            var result = requestData.CreateResponse(HttpStatusCode.BadRequest);
            await result.WriteAsJsonAsync(new
            {
                Title = HttpStatusCode.BadRequest.ToString(),
                Status = (int)HttpStatusCode.BadRequest,
                Detail = message
            }, ct).ConfigureAwait(false);
            return result;
        }

        public static string GetRawPassword(this HttpRequestData requestData)
        {
            string securePassword = string.Empty;
            if (requestData.Headers.TryGetValues(InfrastructureConstants.PASSWORD_HEADER, out IEnumerable<string>? values))
            {
                securePassword = values.First();
            }
            return securePassword;
        }
    }
}

