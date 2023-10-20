using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArztController : ControllerBase
    {

        private readonly ILogger<Arzt> _logger;

        public ArztController(ILogger<Arzt> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetArzt")]
        public Arzt Get()
        {
            return (
                new Arzt
                {
                    name = "Kim",
                    adresse = "AVHjfkj 14"
                }
            );          
        }
    }
}