using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;

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
    }
}
