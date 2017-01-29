using System.Collections.Generic;

namespace Neo4jConsoleApp.Models
{
    public class Result<T> where T : class
    {
        public IList<string> columns { get; set; }
        public IList<Record<T>> data { get; set; }
    }
}
