using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace InvoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {

        // GET api/invoice
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok();
        }

        // POST api/invoice
        [HttpPost]
        public IActionResult Post([FromBody] Models.Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Models.ServiceProvider provider;
            //Store your companies data on a JSON file located in App_Data
            try
            {
                var JSON = System.IO.File.ReadAllText("App_Data/serviceProvider.json");
                provider = JsonConvert.DeserializeObject<Models.ServiceProvider>(JSON);
            }
            catch (Exception)
            {
                return StatusCode(500,"Problem reading service provider data file");  
            }

            
            return CreatedAtAction("Get", Models.Invoice.generateInvoice(client, provider));
        }

    }
}
