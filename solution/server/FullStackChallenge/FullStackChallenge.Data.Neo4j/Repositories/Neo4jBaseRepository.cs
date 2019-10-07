using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FullStackChallenge.Data.Config;
using FullStackChallenge.Data.Models;
using FullStackChallenge.Data.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Neo4j.Driver.V1;
using Newtonsoft.Json;

namespace FullStackChallenge.Data.Neo4j
{
    public class Neo4jBaseRepository<TModel> : IBaseRepository<TModel> where TModel: IModel
    {
        private readonly IDriver _driver;

        public Neo4jBaseRepository(IOptions<DbOptions> options)
        {
            var neo4jConfig = options.Value.Config.Neo4j;
            _driver = GraphDatabase.Driver(neo4jConfig.Endpoint, AuthTokens.Basic(neo4jConfig.UserName,
                                                                                           neo4jConfig.Password));
        }

        Task<List<TModel>> IRepository<TModel>.GetAsync()
        {
            using (var session = _driver.Session())
            {
                return session.WriteTransactionAsync(async tx =>
                {
                    var cypherText = $"MATCH (model:{typeof(TModel).Name}) RETURN id(model), model";
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

        Task<bool> IRepository<TModel>.UpdateAsync(TModel model)
        {
            throw new System.NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            _driver?.Dispose();
        }
    }
}
