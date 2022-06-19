using CarRentalMVC_Auth.Data;
using CarRentalMVC_Auth.Models.GraphModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalMVC_Auth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Insurance_GraphController : ControllerBase
    {
        // GET: api/<UserGraphController>
        [ActionName("Index")]
        public IEnumerable<string> Get()
        {
            List<string> vertices = new List<string>();
            vertices = GraphDbService.GetAll(".hasLabel('Insurance')");
            return vertices;
        }

        // GET api/<UserGraphController>/5
        [HttpGet("{id}")]
        [ActionName("Details")]
        public string Get(string id)
        {
            List<string> vertices = new List<string>();
            vertices = GraphDbService.GetAll($".hasLabel('Insurance').has('id', '{id}')");
            try
            {
                return vertices[0];
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        // POST api/<UserGraphController>
        [HttpPost]
        [ActionName("Create")]
        public void Post([FromBody] Insurance_Graph value)
        {
            string output;
            output = GraphDbService.Post(value);
        }

        // PUT api/<UserGraphController>/5
        [HttpPut]
        [ActionName("Edit")]
        public void Put([FromBody] Insurance_Graph value)
        {
            string output;
            output = GraphDbService.Put(value);
        }

        // DELETE api/<UserGraphController>/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public void Delete(string id)
        {
            string output = "nothing found";
            output = GraphDbService.Delete(id, ".hasLabel('Insurance')");
        }

    }
}
