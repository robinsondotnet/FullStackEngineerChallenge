using System.Collections.Generic;
using FullStackChallenge.Infra.Utils;

namespace FullStackChallenge.Data.Neo4j.Repositories
{
    public interface IQueryBuilder
    {
        string CreateQueryStringFromObjectProps<T>(string nodeAlias, T obj) where T : class;
    }
    
    public class QueryBuilder : IQueryBuilder
    {
        public string CreateQueryStringFromObjectProps<T>(string nodeAlias, T obj) where T : class
        {
            var props = obj.GetType().GetProperties();

            var values = new List<string>();

            foreach (var prop in props)
            {
                values.Add($"{nodeAlias}.{prop.Name.ToCamelCase()} = ${prop.Name}");
            }

            return string.Join(",", values);
        }
    }
}