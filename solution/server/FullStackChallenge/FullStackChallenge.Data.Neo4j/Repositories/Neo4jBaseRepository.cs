using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullStackChallenge.Data.Config;
using FullStackChallenge.Data.Models;
using FullStackChallenge.Data.Repositories.Core;
using Microsoft.Extensions.Options;
using Neo4j.Driver.V1;
using Newtonsoft.Json;

namespace FullStackChallenge.Data.Neo4j.Repositories
{
    public interface INeo4jBaseRepository<TModel> : IBaseRepository<TModel> where TModel : class, IModel, new ()
    {
        Task<List<TModel>> GetAsync(Tuple<string, string, int> relationships);
    }
    
    public class Neo4jBaseRepository<TModel> : INeo4jBaseRepository<TModel> where TModel: class, IModel, new ()
    {
        private readonly IDriver _driver;

        private readonly IQueryBuilder _queryBuilder;

        public Neo4jBaseRepository(IOptions<DbOptions> options, IQueryBuilder queryBuilder)
        {
            var neo4JConfig = options.Value.Config.Neo4j;
            _driver = GraphDatabase.Driver(neo4JConfig.Endpoint, AuthTokens.Basic(neo4JConfig.UserName,
                                                                                           neo4JConfig.Password));
            _queryBuilder = queryBuilder;
        }

        async Task<List<TModel>> IRepository<TModel>.GetAsync()
        {
            using (var session = _driver.Session())
            {
                return await session.WriteTransactionAsync(async tx =>
                {
                    var cypherText = $@"MATCH (model:{typeof(TModel).Name}) 
                                        RETURN id(model), model";
                    
                    var txResult = await tx.RunAsync(cypherText);

                    // TODO: Use a proper custom serializer 
                    return await txResult.ToListAsync(record => {
                        var nodeId = record[0].As<int>();
                        var nodeProps = JsonConvert.SerializeObject(record[1].As<INode>().Properties);
                        var model = JsonConvert.DeserializeObject<TModel>(nodeProps);
                        model.Id = nodeId;
                        return model;
                    });
                });
            }
        }

        async Task<List<TModel>> INeo4jBaseRepository<TModel>.GetAsync(Tuple<string, string, int> relationships)
        {
            using (var session = _driver.Session())
            {
                return await session.WriteTransactionAsync(async tx =>
                {
                        var cypherText = $@"MATCH (model:{typeof(TModel).Name})
                                        -[:{relationships.Item2}]->
                                            (targetNode:{relationships.Item1})
                                        WHERE id(targetNode) = {relationships.Item3}
                                        RETURN id(model), model";
                        Console.WriteLine(cypherText);
                    
                        var txResult = await tx.RunAsync(cypherText);

                        return await txResult.ToListAsync(record => {
                            var nodeId = record[0].As<int>();
                            var nodeProps = JsonConvert.SerializeObject(record[1].As<INode>().Properties);
                            var model = JsonConvert.DeserializeObject<TModel>(nodeProps);
                            model.Id = nodeId;
                            return model;
                        });
                });
            }
        }

        Task<bool> IRepository<TModel>.InsertAsync(TModel model)
        {
            throw new NotImplementedException();
            //using (var session = _driver.Session())
            //{
            //    var greeting = session.WriteTransaction(tx =>
            //    {
            //        var result = tx.Run("CREATE (:Greeting) " +
            //                            "SET a.message = $message " +
            //                            "RETURN a.message + ', from node ' + id(a)");
            //        return result.Single()[0].As();
            //    });
            //    Console.WriteLine(greeting);
            //}
        }

        async Task<bool> IRepository<TModel>.UpdateAsync(TModel model)
        {
            using (var session = _driver.Session())
            {
                return await session.WriteTransactionAsync(async tx =>
                {
                    var queryString = _queryBuilder.CreateQueryStringFromObjectProps("model", model);
                    Console.WriteLine(queryString);
                    
                    var cypherText = $@"MATCH (model:{typeof(TModel).Name}) WHERE ID(model) = {model.Id} SET {queryString} RETURN id(model), model";
                    
                    Console.WriteLine(cypherText);
                    
                    var _ = await tx.RunAsync(cypherText, model);
                    await tx.CommitAsync();
                    return true;
                 
                });
            }
        }

        void IDisposable.Dispose()
        {
            _driver?.Dispose();
        }
    }
}
