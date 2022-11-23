using System.Net;
using System.Net.Mail;
using Microsoft.Azure.Functions.Worker.Http;

namespace jkdmyrs.TicTacTiny.Infrastructure.Extensions
{
    public static class HttpRequestDataExtensions
    {
        public static async Task<HttpResponseData> CreateStringResultAsync(this HttpRequestData requestData, string content, CancellationToken ct = default)
        {
            var result = requestData.CreateResponse();
            await result.WriteStringAsync(content).ConfigureAwait(false);
            return result;
        }

        public static async Task<HttpResponseData> CreateStatusCodeResultAsync(this HttpRequestData requestData, HttpStatusCode httpStatusCode, CancellationToken ct = default)
        {
            var result = requestData.CreateResponse();
            await result.WriteAsJsonAsync(new
            {
                Title = httpStatusCode.ToString(),
                Status = (int)httpStatusCode,
                Detail = httpStatusCode.ToString()
            }, ct).ConfigureAwait(false);
            result.StatusCode = httpStatusCode;
            return result;
        }

        public static async Task<HttpResponseData> CreateStatusCodeResultAsync(this HttpRequestData requestData, HttpStatusCode httpStatusCode, string message, CancellationToken ct = default)
        {
            var result = requestData.CreateResponse();
            await result.WriteAsJsonAsync(new
            {
                Title = httpStatusCode.ToString(),
                Status = (int)httpStatusCode,
                Detail = message
            }, ct).ConfigureAwait(false);
            result.StatusCode = httpStatusCode;
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

