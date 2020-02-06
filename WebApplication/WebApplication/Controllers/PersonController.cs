using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("person")]
    public class PersonController : ControllerBase
    {

        [HttpGet]
        public void DataTwo()
        {

        }

        [HttpPost]
        public string Data()
        {
            return "tt";
        }

    }
}
