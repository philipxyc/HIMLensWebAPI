using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    public class GetAllController : Controller
    {
        private HimContext _context;
        public GetAllController(HimContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll([FromHeader(Name = "Ocp-Apim-Subscription-Key")] string key)
        {
            if (key== "b302232340e94255844a15cb58e72fcd")
            {
                return new ObjectResult(Program.ListAll(_context));
            }
            else
            {
                return new NotFoundResult();
            }
        }
    }
}
