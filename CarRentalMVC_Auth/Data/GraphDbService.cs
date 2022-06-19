using CarRentalMVC_Auth.Models.GraphModel;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;

namespace CarRentalMVC_Auth.Data
{
    public static class GraphDbService
    {
        private static string hostname = "shark0151-g.gremlin.cosmosdb.azure.com";
        private static int port = 443;
        private static string authKey = "YRqJ9fT9gyhWTX256RkpDYQirWliLMPnrBpqBBs2iZL60IjfhSbOLG9W1xYm1L2J3y3FPEV9mJon46lfUbvRcQ==";
        private static string database = "CarRentalGraphDB";
        private static string collection = "Rentals";
        public static GremlinServer gremlinServer;

        static GraphDbService()
        {
            gremlinServer = new GremlinServer(hostname, port, enableSsl: true,
                username: "/dbs/" + database + "/colls/" + collection,
                password: authKey);
        }

        private static Dictionary<string, string> gremlinQueriesEx = new Dictionary<string, string>
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

        public static List<string> GetAll(string filter)
        {
            List<string> vertices = new List<string>();
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                KeyValuePair<string, string> query = new KeyValuePair<string, string>("1", "g.V()"+filter);
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

        #region Post

        public static string Post(User_Graph value)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(),
                       new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                string x =
                    $"g.addV('{value.label}').property('name', '{value.name}').property('birthdate', '{value.birthdate}').property('license_exp', '{value.license_exp}').property('license_id', '{value.license_id}').property('id_number', '{value.id_number}').property('pk', '1')";
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

            return output;
        }

        public static string Post(Location_Graph value)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(),
                       new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                string x =
                    $"g.addV('{value.label}').property('name', '{value.name}').property('address', '{value.address}').property('zipcode', '{value.zipcode}').property('pk', '1')";
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

            return output;
        }

        public static string Post(Insurance_Graph value)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(),
                       new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                string x =
                    $"g.addV('{value.label}').property('type', '{value.type}').property('rate', '{value.rate}').property('description', '{value.description}').property('pk', '1')";
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

            return output;
        }

        public static string Post(Rental_Graph value)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(),
                       new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                value.id = Guid.NewGuid().ToString();
                Dictionary<string, string> gremlinQueries = new Dictionary<string, string>
                {
                    { "AddRental",      $"g.addV('{value.label}').property('id', '{value.id}').property('start_time', '{value.start_time}').property('end_time', '{value.end_time}').property('active', {value.active.ToString().ToLower()}).property('pk', '1')" },
                    { "AddUser",        $"g.V('{value.userID}').addE('Rents').to(g.V('{value.id}'))" },
                    { "AddVehicle",        $"g.V('{value.id}').addE('Uses').to(g.V('{value.vehicleID}'))" },
                    { "AddPickLocation",        $"g.V('{value.id}').addE('PickAt').to(g.V('{value.pick_locationID}'))" },
                    { "AddDropLocation",        $"g.V('{value.id}').addE('DropAt').to(g.V('{value.drop_locationID}'))" }
                };
                
                foreach (var query in gremlinQueries)
                {

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

            return output;
        }

        public static string Post(Vehicle_Graph value)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(),
                       new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                string x =
                    $"g.addV('{value.label}').property('name', '{value.name}').property('description', '{value.description}').property('pk', '1')";
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

            return output;
        }

        #endregion

        #region Put

        public static string Put(User_Graph value)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                string x =
                    $"g.V('{value.id}').hasLabel('{value.label}').property('name', '{value.name}').property('birthdate', '{value.birthdate}').property('license_exp', '{value.license_exp}').property('license_id', '{value.license_id}').property('id_number', '{value.id_number}')";

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

            return output;
        }

        public static string Put(Location_Graph value)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                string x =
                    $"g.V('{value.id}').hasLabel('{value.label}').property('name', '{value.name}').property('address', '{value.address}').property('zipcode', '{value.zipcode}')";

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

            return output;
        }

        public static string Put(Insurance_Graph value)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                string x =
                    $"g.V('{value.id}').hasLabel('{value.label}').property('type', '{value.type}').property('rate', '{value.rate}').property('description', '{value.description}')";

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

            return output;
        }

        public static string Put(Rental_Graph value)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                Dictionary<string, string> gremlinQueries = new Dictionary<string, string>
                {
                    { "ChangeRental",      $"g.V('{value.id}').hasLabel('{value.label}').property('start_time', '{value.start_time}').property('end_time', '{value.end_time}').property('active', {value.active.ToString().ToLower()})" },
                    { "DropEdge1",        $"g.V('{value.userID}').outE('Rents').where(inV().has('id', '{value.id}')).drop()" },
                    { "DropEdge2",        $"g.V('{value.id}').outE('Uses').drop()" },
                    { "DropEdge3",        $"g.V('{value.id}').outE('PickAt').drop()" },
                    { "DropEdge4",        $"g.V('{value.id}').outE('DropAt').drop()" },
                    { "AddUser",        $"g.V('{value.userID}').addE('Rents').to(g.V('{value.id}'))" },
                    { "AddVehicle",        $"g.V('{value.id}').addE('Uses').to(g.V('{value.vehicleID}'))" },
                    { "AddPickLocation",        $"g.V('{value.id}').addE('PickAt').to(g.V('{value.pick_locationID}'))" },
                    { "AddDropLocation",        $"g.V('{value.id}').addE('DropAt').to(g.V('{value.drop_locationID}'))" }
                };
                foreach (var query in gremlinQueries)
                {
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

            return output;
        }

        public static string Put(Vehicle_Graph value)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                string x =
                    $"g.V('{value.id}').hasLabel('{value.label}').property('name', '{value.name}').property('description', '{value.description}')";

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

            return output;
        }

        #endregion

        public static string Delete(string id, string filter)
        {
            string output = "nothing found";
            using (var gremlinClient = new GremlinClient(GraphDbService.gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                KeyValuePair<string, string> query = new KeyValuePair<string, string>("1", $"g.V('{id}'){filter}.drop()"); //has label check?
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

        #region Helper

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
