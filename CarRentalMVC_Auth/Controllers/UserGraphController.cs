using CarRentalMVC_Auth.Data;
using CarRentalMVC_Auth.Models.GraphModel;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRentalMVC_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGraphController : ControllerBase
    {
        private static Dictionary<string, string> gremlinQueries = new Dictionary<string, string>
        {
            { "Cleanup",        "g.V().drop()" },
            { "AddVertex 1",    "g.addV('person').property('id', 'thomas').property('firstName', 'Thomas').property('age', 44).property('pk', 1)" },
            { "AddVertex 2",    "g.addV('person').property('id', 'mary').property('firstName', 'Mary').property('lastName', 'Andersen').property('age', 39).property('pk', 1)" },
            { "AddVertex 3",    "g.addV('person').property('id', 'ben').property('firstName', 'Ben').property('lastName', 'Miller').property('pk', 1)" },
            { "AddVertex 4",    "g.addV('person').property('id', 'robin').property('firstName', 'Robin').property('lastName', 'Wakefield').property('pk', 1)" },
            { "AddEdge 1",      "g.V('thomas').addE('knows').to(g.V('mary'))" },
            { "AddEdge 2",      "g.V('thomas').addE('knows').to(g.V('ben'))" },
            { "AddEdge 3",      "g.V('ben').addE('knows').to(g.V('robin'))" },
            { "UpdateVertex",   "g.V('thomas').property('age', 44)" },
            { "CountVertices",  "g.V().count()" },
            { "Filter Range",   "g.V().hasLabel('person').has('age', gt(40))" },
            { "Project",        "g.V().hasLabel('person').values('firstName')" },
            { "Sort",           "g.V().hasLabel('person').order().by('firstName', decr)" },
            { "Traverse",       "g.V('thomas').out('knows').hasLabel('person')" },
            { "Traverse 2x",    "g.V('thomas').out('knows').hasLabel('person').out('knows').hasLabel('person')" },
            { "Loop",           "g.V('thomas').repeat(out()).until(has('id', 'robin')).path()" },
            { "DropEdge",       "g.V('thomas').outE('knows').where(inV().has('id', 'mary')).drop()" },
            { "CountEdges",     "g.E().count()" },
            { "DropVertex",     "g.V('thomas').drop()" },
        };

        // GET: api/<UserGraphController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> vertices = new List<string>();
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                KeyValuePair<string, string> query = new KeyValuePair<string, string>("1", "g.V()");
                // Create async task to execute the Gremlin query.
                    var resultSet = SubmitRequest(gremlinClient, query).Result;
                    if (resultSet.Count > 0)
                    {
                        Console.WriteLine("\tResult:");
                        foreach (var result in resultSet)
                        {
                            // The vertex results are formed as Dictionaries with a nested dictionary for their properties
                            string output = JsonConvert.SerializeObject(result);
                            vertices.Add(output);
                            Console.WriteLine($"\t{output}");
                        }
                        Console.WriteLine();
                    }

                    // Print the status attributes for the result set.
                    // This includes the following:
                    //  x-ms-status-code            : This is the sub-status code which is specific to Cosmos DB.
                    //  x-ms-total-request-charge   : The total request units charged for processing a request.
                    PrintStatusAttributes(resultSet.StatusAttributes);
                    Console.WriteLine();
                
            }
            return vertices;
        }

        // GET api/<UserGraphController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                KeyValuePair<string, string> query = new KeyValuePair<string, string>("1", $"g.V().has('id', '{id}')");
                // Create async task to execute the Gremlin query.
                var resultSet = SubmitRequest(gremlinClient, query).Result;
                if (resultSet.Count > 0)
                {
                    Console.WriteLine("\tResult:");
                    foreach (var result in resultSet)
                    {
                        // The vertex results are formed as Dictionaries with a nested dictionary for their properties
                        output = JsonConvert.SerializeObject(result);
                        Console.WriteLine($"\t{output}");
                    }
                    Console.WriteLine();
                }

                // Print the status attributes for the result set.
                // This includes the following:
                //  x-ms-status-code            : This is the sub-status code which is specific to Cosmos DB.
                //  x-ms-total-request-charge   : The total request units charged for processing a request.
                PrintStatusAttributes(resultSet.StatusAttributes);
                Console.WriteLine();

            }
            return output;
        }

        // POST api/<UserGraphController>
        [HttpPost]
        public void Post([FromBody] GraphUser value)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(),
                       new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                string x =
                    $"g.addV('{value.label}').property('id', '{value.id}').property('name', '{value.name}').property('username', '{value.username}').property('password', '{value.password}').property('email', '{value.email}').property('birthdate', '{value.birthdate}').property('license_exp', '{value.license_exp}').property('license_id', '{value.license_id}').property('id_number', '{value.id_number}').property('pk', '1')";
                KeyValuePair<string, string> query = new KeyValuePair<string, string>("1", x);
                // Create async task to execute the Gremlin query.
                var resultSet = SubmitRequest(gremlinClient, query).Result;
                if (resultSet.Count > 0)
                {
                    Console.WriteLine("\tResult:");
                    foreach (var result in resultSet)
                    {
                        // The vertex results are formed as Dictionaries with a nested dictionary for their properties
                        output = JsonConvert.SerializeObject(result);
                        Console.WriteLine($"\t{output}");
                    }

                    Console.WriteLine();
                }

                // Print the status attributes for the result set.
                // This includes the following:
                //  x-ms-status-code            : This is the sub-status code which is specific to Cosmos DB.
                //  x-ms-total-request-charge   : The total request units charged for processing a request.
                PrintStatusAttributes(resultSet.StatusAttributes);
                Console.WriteLine();

            }
        }

        private static Dictionary<string, string> updateQueries = new Dictionary<string, string>
        {
            { "label",   "g.V('{id}').property('id', {value.id})" },
        };

        // PUT api/<UserGraphController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] GraphUser value)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                string x =
                    $"g.V('{id}').property('name', '{value.name}').property('username', '{value.username}').property('password', '{value.password}').property('email', '{value.email}').property('birthdate', '{value.birthdate}').property('license_exp', '{value.license_exp}').property('license_id', '{value.license_id}').property('id_number', '{value.id_number}')";

                KeyValuePair<string, string> query = new KeyValuePair<string, string>("1", x);
                // Create async task to execute the Gremlin query.
                var resultSet = SubmitRequest(gremlinClient, query).Result;
                if (resultSet.Count > 0)
                {
                    Console.WriteLine("\tResult:");
                    foreach (var result in resultSet)
                    {
                        // The vertex results are formed as Dictionaries with a nested dictionary for their properties
                        output = JsonConvert.SerializeObject(result);
                        Console.WriteLine($"\t{output}");
                    }
                    Console.WriteLine();
                }

                // Print the status attributes for the result set.
                // This includes the following:
                //  x-ms-status-code            : This is the sub-status code which is specific to Cosmos DB.
                //  x-ms-total-request-charge   : The total request units charged for processing a request.
                PrintStatusAttributes(resultSet.StatusAttributes);
                Console.WriteLine();

            }
        }

        // DELETE api/<UserGraphController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                KeyValuePair<string, string> query = new KeyValuePair<string, string>("1", $"g.V('{id}').drop()");
                // Create async task to execute the Gremlin query.
                var resultSet = SubmitRequest(gremlinClient, query).Result;
                if (resultSet.Count > 0)
                {
                    Console.WriteLine("\tResult:");
                    foreach (var result in resultSet)
                    {
                        // The vertex results are formed as Dictionaries with a nested dictionary for their properties
                        output = JsonConvert.SerializeObject(result);
                        Console.WriteLine($"\t{output}");
                    }
                    Console.WriteLine();
                }

                // Print the status attributes for the result set.
                // This includes the following:
                //  x-ms-status-code            : This is the sub-status code which is specific to Cosmos DB.
                //  x-ms-total-request-charge   : The total request units charged for processing a request.
                PrintStatusAttributes(resultSet.StatusAttributes);
                Console.WriteLine();

            }
            
        }


        #region Crap
        private static Task<ResultSet<dynamic>> SubmitRequest(GremlinClient gremlinClient, KeyValuePair<string, string> query)
        {
            try
            {
                return gremlinClient.SubmitAsync<dynamic>(query.Value);
            }
            catch (ResponseException e)
            {
                Console.WriteLine("\tRequest Error!");

                // Print the Gremlin status code.
                Console.WriteLine($"\tStatusCode: {e.StatusCode}");

                // On error, ResponseException.StatusAttributes will include the common StatusAttributes for successful requests, as well as
                // additional attributes for retry handling and diagnostics.
                // These include:
                //  x-ms-retry-after-ms         : The number of milliseconds to wait to retry the operation after an initial operation was throttled. This will be populated when
                //                              : attribute 'x-ms-status-code' returns 429.
                //  x-ms-activity-id            : Represents a unique identifier for the operation. Commonly used for troubleshooting purposes.
                PrintStatusAttributes(e.StatusAttributes);
                Console.WriteLine($"\t[\"x-ms-retry-after-ms\"] : {GetValueAsString(e.StatusAttributes, "x-ms-retry-after-ms")}");
                Console.WriteLine($"\t[\"x-ms-activity-id\"] : {GetValueAsString(e.StatusAttributes, "x-ms-activity-id")}");

                throw;
            }
        }

        private static void PrintStatusAttributes(IReadOnlyDictionary<string, object> attributes)
        {
            Console.WriteLine($"\tStatusAttributes:");
            Console.WriteLine($"\t[\"x-ms-status-code\"] : {GetValueAsString(attributes, "x-ms-status-code")}");
            Console.WriteLine($"\t[\"x-ms-total-request-charge\"] : {GetValueAsString(attributes, "x-ms-total-request-charge")}");
        }

        public static string GetValueAsString(IReadOnlyDictionary<string, object> dictionary, string key)
        {
            return JsonConvert.SerializeObject(GetValueOrDefault(dictionary, key));
        }

        public static object GetValueOrDefault(IReadOnlyDictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return null;
        }
        #endregion
    }
}
