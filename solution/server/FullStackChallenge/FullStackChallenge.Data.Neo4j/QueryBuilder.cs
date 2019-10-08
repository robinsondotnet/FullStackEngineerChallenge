using System.Collections.Generic;
using FullStackChallenge.Infra.Utils;

namespace FullStackChallenge.Data.Neo4j
{
    public interface IQueryBuilder
    {
        string CreateUpdateQueryStringFromObjectProps<T>(string nodeAlias, T obj) where T : class;
        string CreateInsertQueryStringFromObjectProps<T>(T obj) where T : class;
    }
    
    public class QueryBuilder : IQueryBuilder
    {
        public string CreateUpdateQueryStringFromObjectProps<T>(string nodeAlias, T obj) where T : class 
            => CreateQueryStringFromObjectProps(nodeAlias, "=", obj);

        public string CreateInsertQueryStringFromObjectProps<T>(T obj) where T : class 
            => CreateQueryStringFromObjectProps(string.Empty, ":", obj);

        private static string CreateQueryStringFromObjectProps<T>(string nodeAlias, string delimiter, T obj) where T : class
        {
            var props = obj.GetType().GetProperties();

            var values = new List<string>();

            foreach (var prop in props)
            {
                var parameterName = string.IsNullOrEmpty(nodeAlias)
                    ? prop.Name.ToCamelCase()
                    : $"{nodeAlias}.{prop.Name.ToCamelCase()}";
                
                values.Add($"{parameterName} {delimiter} ${prop.Name}");
            }

            return string.Join(",", values);
        }
    }
}