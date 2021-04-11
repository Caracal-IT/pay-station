using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Caracal.PayStation.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase {
        static HttpClient client = new HttpClient();
        
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request) {
            var reason = "Login Failed";
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:5001/users/Login", request);
          //  response.EnsureSuccessStatusCode();
          if (response.IsSuccessStatusCode) {
              var ctx = await response.Content.ReadFromJsonAsync<UserContext>();
              reason = ctx?.UserId.ToString();
          }

          return Ok(new LoginResponse($"Welcome <em style='color:hotpink'>{reason}</em>"));
        }
    }

    public record LoginRequest(string Username, string Password, long TenantId = 207);

    public record LoginResponse(string WelcomeMessage);

    public record UserContext(int UserId, string Token);
}