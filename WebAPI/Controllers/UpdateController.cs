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
    public class UpdateController : Controller
    {
        private HimContext _context;
        public UpdateController(HimContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromHeader(Name = "RequestId")] string rId, [FromHeader(Name = "Ocp-Apim-Subscription-Key")] string key, [FromHeader(Name = "Tags")] string tags)
        {
            tags = System.Net.WebUtility.UrlDecode(tags);
            try
            {
                var id = await Program.GetId(key, Request);
                if (id == Guid.Empty)
                    return NotFound();
                var tags_full = await Program.UpdateTags(key, id, _context, tags);
                tags_full.Insert(0, rId);
                foreach (var t in tags_full)
                {
                    t.Replace('+', ' ');
                }
                return new ObjectResult(tags_full);
            }
            catch (InvalidOperationException)
            {
                return new ObjectResult(new List<string> { rId });
            }
            catch (Exception ex)
            {
                return new ObjectResult("Failed to communicate with Cognitive Service :" + ex.Message);
                //return NotFound();
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Del([FromHeader(Name = "RequestId")] string rId, [FromHeader(Name = "Ocp-Apim-Subscription-Key")] string key, [FromHeader(Name = "Tags")] string tags)
        {
            tags = System.Net.WebUtility.UrlDecode(tags);
            try
            {
                var id = await Program.GetId(key, Request);
                if (id == Guid.Empty)
                    return NotFound();
                var tags_full = await Program.DelTags(key, id, _context, tags);
                tags_full.Insert(0, rId);
                foreach (var t in tags_full)
                {
                    t.Replace('+', ' ');
                }
                return new ObjectResult(tags_full);
            }
            catch (InvalidOperationException)
            {
                return new ObjectResult(new List<string> { rId });
            }
            catch (Exception ex)
            {
                return new ObjectResult("Failed to communicate with Cognitive Service :" + ex.Message);
                //return NotFound();
            }
        }
    }
}
