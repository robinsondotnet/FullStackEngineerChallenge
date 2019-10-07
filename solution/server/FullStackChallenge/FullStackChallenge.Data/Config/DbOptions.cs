namespace FullStackChallenge.Data.Config
{
    public class DbOptions
    {
        public string Driver { get; set; }

        public DbOptionsConfig Config { get; set; }

        public class DbOptionsConfig
        {
            public DbOptionsConfigItem Neo4j { get; set; }
            public DbOptionsConfigItem Sqlite { get; set; }
        }

        public class DbOptionsConfigItem
        {
            public string Endpoint { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
