using System.Net;

namespace RealEstate_Dapper_UI.Services
{
    /// <summary>
    /// When the API cannot be reached (not running, wrong port, certificate or network error),
    /// HttpClient throws and the whole page fails. This handler turns those transport errors into a
    /// 503 response with an empty body, so the existing "if (!IsSuccessStatusCode)" fallbacks in the
    /// controllers and ViewComponents render their empty states instead. Successful calls are untouched.
    /// </summary>
    public class ApiUnavailableHandler : DelegatingHandler
    {
        private readonly ILogger<ApiUnavailableHandler> _logger;

        public ApiUnavailableHandler(ILogger<ApiUnavailableHandler> logger) => _logger = logger;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                return await base.SendAsync(request, cancellationToken);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "API request failed: {Method} {Url}", request.Method, request.RequestUri);
                return Unavailable(request);
            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogError(ex, "API request timed out: {Method} {Url}", request.Method, request.RequestUri);
                return Unavailable(request);
            }
        }

        private static HttpResponseMessage Unavailable(HttpRequestMessage request) =>
            new(HttpStatusCode.ServiceUnavailable) { RequestMessage = request, Content = new StringContent("") };
    }
}
