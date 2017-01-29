using System.Collections.Generic;

namespace Neo4jConsoleApp.Models
{
    public class Neo4jHttpResponse<T> where T : class
    {
        public IList<Result<T>> results { get; set; }
        public IList<object> errors { get; set; }
    }
}
