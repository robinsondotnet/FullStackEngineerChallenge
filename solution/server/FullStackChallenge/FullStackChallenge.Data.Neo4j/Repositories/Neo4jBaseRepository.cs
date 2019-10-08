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
        Task<List<TModel>> GetByRelationshipAsync(SimpleNodeRelationship relationship, string orderBy, string direction = "asc");
        Task<TModel> InsertWithRelationshipAsync(TModel model, SimpleNodeRelationship relationship);
        Task<bool> SetRelationshipAsync(int modelId, SimpleNodeRelationship relationship);
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

        async Task<List<TModel>> INeo4jBaseRepository<TModel>.GetByRelationshipAsync(SimpleNodeRelationship relationship, string orderBy, string direction = "asc")
        {
            using (var session = _driver.Session())
            {
                return await session.WriteTransactionAsync(async tx =>
                {
                        var cypherText = $@"MATCH (model:{typeof(TModel).Name})
                                        -[:{relationship.RelationshipName}]->
                                            (targetNode:{relationship.TargetNodeName})
                                        WHERE id(targetNode) = {relationship.TargetNodeId}
                                        RETURN id(model), model";
                    
                        cypherText += string.IsNullOrEmpty(orderBy)
                        ? ""
                        : $" ORDER BY model.{orderBy} {direction}";
                    
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

        public async Task<TModel> InsertWithRelationshipAsync(TModel model, SimpleNodeRelationship relationship)
        {
            using (var session = _driver.Session())
            {
                return await session.WriteTransactionAsync(async tx =>
                {
                    var queryString = _queryBuilder.CreateInsertQueryStringFromObjectProps(model);
                    Console.WriteLine(queryString);
                    
                    // TODO: Handle creationTime with other approach (DataAnotation, etc) instead of  hardcoding
                    var cypherText = $@"MATCH(refNode:{relationship.TargetNodeName})
                                        WHERE id(refNode) = {relationship.TargetNodeId}
                                        CREATE (refNode)
                                        <-[:{relationship.RelationshipName}]-
                                        (model:{typeof(TModel).Name} {{{queryString} , createdAt: TIMESTAMP()}}) 
                                        RETURN id(model), model";
                    
                    Console.WriteLine(cypherText);
                    
                    var txResult = await tx.RunAsync(cypherText, model);

                    var rawResult = await txResult.SingleAsync();
                    Console.WriteLine(rawResult);
                    
                    var nodeId = rawResult[0].As<int>();
                    var nodeProps = JsonConvert.SerializeObject(rawResult[1].As<INode>().Properties);
                    var result = JsonConvert.DeserializeObject<TModel>(nodeProps);
                    result.Id = nodeId;
                    return result;
                });
            }
        }

        public async Task<bool> SetRelationshipAsync(int modelId, SimpleNodeRelationship relationship)
        {
            using (var session = _driver.Session())
            {
                return await session.WriteTransactionAsync(async tx =>
                {
                    var cypherText = $@"MATCH (model:{typeof(TModel).Name}), (nodeRef: {relationship.TargetNodeName})
                                        WHERE id(model)={modelId} AND id(nodeRef)={relationship.TargetNodeId}
                                        CREATE (model)<-[:{relationship.RelationshipName}]-(nodeRef)
                                        RETURN id(model), model";
                    
                    Console.WriteLine(cypherText);
                    
                    var txResult = await tx.RunAsync(cypherText);

                    var rawResult = await txResult.SingleAsync();
                    Console.WriteLine(rawResult);
                    
                    var nodeId = rawResult[0].As<int>();
                    var nodeProps = JsonConvert.SerializeObject(rawResult[1].As<INode>().Properties);
                    var result = JsonConvert.DeserializeObject<TModel>(nodeProps);
                    result.Id = nodeId;
                    return true;
                });
            }
        }

        async Task<bool> IRepository<TModel>.InsertAsync(TModel model)
        {
            using (var session = _driver.Session())
            {
                return await session.WriteTransactionAsync(async tx =>
                {
                    var queryString = _queryBuilder.CreateInsertQueryStringFromObjectProps(model);
                    Console.WriteLine(queryString);
                    
                    // TODO: Handle creationTime with other approach (DataAnotation, etc) instead of  hardcoding
                    var cypherText = $@"CREATE (model:{typeof(TModel).Name} {{{queryString} , createdAt: TIMESTAMP()}}) 
                                        RETURN id(model), model";
                    
                    Console.WriteLine(cypherText);
                    
                    var _ = await tx.RunAsync(cypherText, model);
                    await tx.CommitAsync();
                    return true;
                });
            }
        }

        async Task<TModel> IRepository<TModel>.UpdateAsync(TModel model)
        {
            using (var session = _driver.Session())
            {
                return await session.WriteTransactionAsync(async tx =>
                {
                    var queryString = _queryBuilder.CreateUpdateQueryStringFromObjectProps("model", model);
                    Console.WriteLine(queryString);
                    
                    var cypherText = $@"MATCH (model:{typeof(TModel).Name}) WHERE ID(model) = {model.Id} 
                                        SET {queryString} RETURN id(model), model";
                    
                    Console.WriteLine(cypherText);
                    
                    var txResult = await tx.RunAsync(cypherText, model);
                    
                    var rawResult = await txResult.SingleAsync();
                    Console.WriteLine(rawResult);
                    
                    var nodeId = rawResult[0].As<int>();
                    var nodeProps = JsonConvert.SerializeObject(rawResult[1].As<INode>().Properties);
                    var result = JsonConvert.DeserializeObject<TModel>(nodeProps);
                    result.Id = nodeId;
                    return result;
                });
            }
        }

        void IDisposable.Dispose()
        {
            _driver?.Dispose();
        }
    }
}
