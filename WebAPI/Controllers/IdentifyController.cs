using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.IO;
using Microsoft.AspNetCore.WebUtilities;
using WebAPI.Data;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    public class IdentifyController : Controller
    {
        private readonly HimContext _context;
        public IdentifyController(HimContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Get([FromHeader(Name = "RequestId")] string rId, [FromHeader(Name = "Ocp-Apim-Subscription-Key")] string key)
        {
            try
            {
                var id = await Program.GetId(key, Request);
                if (id == Guid.Empty)
                    return NotFound();
                var tags = await Program.GetTags(key, id, _context);
                tags.Insert(0, rId);
                foreach (var t in tags)
                {
                    t.Replace('+', ' ');
                }
                return new ObjectResult(tags);
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

        static async void MakeRequest(HttpRequest _req)
        {
            var client = new HttpClient();
            var queryDictionary = new System.Collections.Generic.Dictionary<string, string>();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{subscription key}");

            // Request parameters
            queryDictionary["returnFaceId"] = "true";
            queryDictionary["returnFaceLandmarks"] = "false";
            queryDictionary["returnFaceAttributes"] = "{string}";

            var uri = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString("https://westus.api.cognitive.microsoft.com/face/v1.0/detect", queryDictionary);

            HttpResponseMessage response;

            // Request body
            var _body = _req.Body;
            byte[] byteData = Encoding.UTF8.GetBytes("{body}");
            var _content = new StreamContent(_body);
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("< your content type, i.e. application/json >");
                response = await client.PostAsync(uri, _content);
            }

        }
    }

}