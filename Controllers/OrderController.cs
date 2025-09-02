

using Microsoft.AspNetCore.Mvc;

namespace TokenExpires.Controllers
{
    public class OrderController : ControllerBase
    {
        private static string _accessToken;
        private static DateTime _expiry;
        private readonly HttpClient _httpClient;

        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult GetOrders()
        {

            if (string.IsNullOrEmpty(_accessToken) || DateTime.UtcNow >= _expiry)
            {
                var TokenResponse = GetAccessToken();
                _accessToken = TokenResponse.AccessToken;
                _expiry = TokenResponse.ExpiresIn;
            }

            var orders = GetOrders(_accessToken);

            return Ok(orders);

        }

        public TokenResponse GetAccessToken()
        {

            return new TokenResponse
            {
                TokenType = "Bearer",
                ExpiresIn = DateTime.UtcNow.AddMinutes(15),
                AccessToken = "sample_access_token"
            };
        }
        
        public object GetOrders(string accessToken)
        {
         
            return new { OrderId = 1, ProductName = "Sample Product" };
        }
    }
}