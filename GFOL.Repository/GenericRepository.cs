﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GFOL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        //Task<Document> CreateAsync(T item);
        //Task DeleteAsync(string id);
        //Task<T> GetByIdAsync(string id);
        //Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        //Task<Document> UpdateAsync(string id, T item);
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //private readonly string _databaseId;
        //private readonly string _collectionId;
        //private readonly IDocumentClient _client;
        //private readonly IAppConfiguration _appConfig;

        //public GenericRepository(IAppConfiguration appConfig, IDocumentClient client)
        //{
        //    _appConfig = appConfig;

        //    _databaseId = _appConfig.DatabaseId;
        //    _collectionId = _appConfig.CollectionId;

        //    _client = client;
        //}

        //public async Task<T> GetByIdAsync(string id)
        //{
        //    try
        //    {
        //        var param = UriFactory.CreateDocumentUri(_databaseId, _collectionId, id);
        //        Document document = await _client.ReadDocumentAsync(param);
        //        return (T)(dynamic)document;
        //    }
        //    catch (DocumentClientException e)
        //    {
        //        if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}

        //public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        //{
        //    var query = _client.CreateDocumentQuery<T>(
        //        UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId),
        //        new FeedOptions { MaxItemCount = -1 })
        //        .Where(predicate)
        //        .AsDocumentQuery();

        //    var results = new List<T>();
        //    while (query.HasMoreResults)
        //    {
        //        results.AddRange(await query.ExecuteNextAsync<T>());
        //    }

        //    return results;
        //}

        //public async Task<Document> CreateAsync(T item)
        //{
        //    return await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId), item);
        //}

        //public async Task<Document> UpdateAsync(string id, T item)
        //{
        //    return await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id), item);
        //}

        //public async Task DeleteAsync(string id)
        //{
        //    await _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_databaseId, _collectionId, id));
        //}
    }
}
