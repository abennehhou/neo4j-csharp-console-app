namespace Neo4jConsoleApp.Models
{
    public class Actor
    {
        public string birthday { get; set; }
        public string birthplace { get; set; }
        public string name { get; set; }
        public string lastModified { get; set; }
        public string id { get; set; }
        public string biography { get; set; }
        public string version { get; set; }
        public string profileImageUrl { get; set; }

        public override string ToString()
        {
            return $"#{id}, name {name}, version {version}.";
        }
    }
}
