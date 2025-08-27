using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentPostController : ControllerBase
    {
 
        // POST api/<ApartmentPostController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
 
    }
}
