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
    public class CreateController : Controller
    {
        private HimContext _context;
        public CreateController(HimContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Creat([FromHeader(Name = "RequestId")] string rId, [FromHeader(Name = "Ocp-Apim-Subscription-Key")] string key, [FromHeader(Name = "Tags")] string tags)
        {
            tags = System.Net.WebUtility.UrlDecode(tags);
            try
            {
                Program.CreatPerson(key, Request, _context, tags).Wait();
            }
            catch (Exception ex)
            {
                return new ObjectResult("Failed to add person: " + ex.Message);
            }
            return new ObjectResult(new List<string> { rId, "Success" });
        }
        [HttpDelete]
        public async Task<IActionResult> Del([FromHeader(Name = "RequestId")] string rId, [FromHeader(Name = "Ocp-Apim-Subscription-Key")] string key)
        {
            try
            {
                var dId = await Program.GetId(key, Request);
                Program.DelPerson(key, dId, _context).Wait();
            }
            catch (Exception ex)
            {
                return new ObjectResult("Failed to del person: " + ex.Message);
            }
            return new ObjectResult(new List<string> { rId, "Success" });
        }
    }
}
