using CarRentalMVC_Auth.Data;
using CarRentalMVC_Auth.Models.GraphModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalMVC_Auth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Rental_GraphController : ControllerBase
    {
        // GET: api/<UserGraphController>
        [ActionName("Index")]
        public IEnumerable<string> Get()
        {
            List<string> vertices = new List<string>();
            vertices = GraphDbService.GetAll(".hasLabel('Rental')");
            return vertices;
        }

        // GET api/<UserGraphController>/5
        [HttpGet("{id}")]
        [ActionName("Details")]
        public IEnumerable<string> Get(string id)
        {
            List<string> vertices = new List<string>();
            vertices = GraphDbService.GetAll($".hasLabel('Rental').has('id', '{id}')");
            vertices.AddRange(GraphDbService.GetAll($".hasId('{id}').out()"));
            try
            {
                return vertices;
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
        public void Post([FromBody] Rental_Graph value)
        {
            string output;
            output = GraphDbService.Post(value);
        }

        // PUT api/<UserGraphController>/5
        [HttpPut]
        [ActionName("Edit")]
        public void Put([FromBody] Rental_Graph value)
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
            output = GraphDbService.Delete(id, ".hasLabel('Rental')");
        }

    }
}
