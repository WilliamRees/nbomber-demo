using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NBobmerDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {        
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<object>> Get()
        {
            var rnd = new Random();

            var sleep = rnd.Next(0, 1000);

            await Task.Run(async () =>
            {
                Thread.Sleep(sleep);
            });            

            return new List<object>
            {
                new {
                    FirstName = "Bob",
                    LastName = "Smith"
                },
                new {
                    FirstName = "Alice",
                    LastName = "Smith"
                }
            };
        }
    }
}
