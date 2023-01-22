using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;
namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly ClientPolicy _clientPolicy;
        private readonly IHttpClientFactory _clientFactory;
        public RequestController(ClientPolicy clientPolicy, IHttpClientFactory clientFactory)
        {
            _clientPolicy = clientPolicy;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            var client = _clientFactory.CreateClient();
            var response = await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(() => client.GetAsync("https://localhost:7060/api/v1/response/10"));

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Success");
                return Ok();
            }
            else{
                Console.WriteLine("Failure");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}