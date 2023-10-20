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
        public IEnumerable<Arzt> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Arzt
            {
                name = "Kim",
                adresse ="AVHjfkj 14"
            })
            .ToArray();
        }
    }
}