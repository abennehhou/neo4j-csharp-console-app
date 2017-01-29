using Neo4jClient;
using Neo4jConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jConsoleApp
{
    class Program
    {
        /// <summary>
        /// Request to get the actors who acted in a movie directed by Stanley Kubrick.
        /// </summary>
        private const string matchText = "(n:Actor)-[:ACTS_IN]-> (:Movie) <-[:DIRECTED]- (:Director{name:'Stanley Kubrick'})";

        static void Main(string[] args)
        {
            Console.WriteLine("****** Test with HttpClient ******");
            var httpRows = CallWithHttpClientAsync().Result;
            foreach (var row in httpRows)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine("****** Test with Neo4jClient ******");
            var neo4jRows = CallWithNeo4jClient();
            foreach (var row in neo4jRows)
            {
                Console.WriteLine(row);
            }

            Console.ReadLine();
        }

        static async Task<IList<Actor>> CallWithHttpClientAsync()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpBody = "{\"statements\":[{\"statement\":\"MATCH " + matchText + " RETURN n LIMIT 3\"}]}";
            var httpResponse = await httpClient
                .PostAsync(new Uri("http://localhost:7474/db/data/transaction/commit"),
                    new StringContent(httpBody, Encoding.UTF8, "application/json"));

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Neo4jHttpResponse<Actor>>(stringResponse);

            return response.results
                .SelectMany(result => result.data)
                .SelectMany(record => record.row)
                .ToList();
        }

        static IList<Actor> CallWithNeo4jClient()
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();

            var query = client
                .Cypher
                .Match(matchText)
                .Return(n => n.As<Actor>())
                .Limit(3);

            return query.Results.ToList();
        }
    }
}
