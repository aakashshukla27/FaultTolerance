using Microsoft.AspNetCore.Mvc;

namespace ResponseService.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        //Get Request /api/response/number
        [Route("{id:int}")]
        [HttpGet]
        public ActionResult GetResponse(int id)
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 101);
            if (id >= number){
                Console.WriteLine(" --> Failure - Generate a HTTP 500");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Console.WriteLine("--> Success - Generate a Http 200");
            return Ok();
        }
    }
}