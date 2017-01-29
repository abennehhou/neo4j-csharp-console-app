using System.Collections.Generic;

namespace Neo4jConsoleApp.Models
{
    public class Record<T> where T : class
    {
        public IList<T> row { get; set; }
    }
}
