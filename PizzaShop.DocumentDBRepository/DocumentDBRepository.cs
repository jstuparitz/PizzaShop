using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using PizzaShop.DomainModel.Shared;

namespace PizzaShop.DocumentDBRepository
{
    public class DocumentDbRepository<TAggregate> : IRepository<TAggregate> where TAggregate : IAggregate
    {
        private static DocumentClient client;
        private static readonly string databaseId = ConfigurationManager.AppSettings["DatabaseId"];
        private static readonly string uri = ConfigurationManager.AppSettings["DocumentDBUri"];
        private static readonly string key = ConfigurationManager.AppSettings["DocumentDBKey"];

        public string CollectionId { get; set; }

        public async Task<bool> Insert(TAggregate entity)
        {
            using (client = new DocumentClient(new Uri(uri), key))
            {
                var link = await GetCollection();
                var document = await client.CreateDocumentAsync(link.SelfLink, entity);
                if (document.StatusCode == HttpStatusCode.OK)
                    return true;
                return false;
            }
        }

        public async Task<bool> Insert(IEnumerable<TAggregate> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(TAggregate entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(TAggregate entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<TAggregate>> SearchFor(Expression<Func<TAggregate, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<TAggregate>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<TAggregate> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        private async Task<DocumentCollection> GetCollection()
        {
            //Get, or Create, the Database
            var database = await GetOrCreateDatabaseAsync(databaseId);

            //Get, or Create, the Document Collection
            return await GetOrCreateCollectionAsync(database.SelfLink, CollectionId);
        }

        /// <summary>
        /// Get a DocumentCollection by id, or create a new one if one with the id provided doesn't exist.
        /// </summary>
        /// <param name="dbLink">The Database SelfLink property where this DocumentCollection exists / will be created</param>
        /// <param name="id">The id of the DocumentCollection to search for, or create.</param>
        /// <returns>The matched, or created, DocumentCollection object</returns>
        private static async Task<DocumentCollection> GetOrCreateCollectionAsync(string dbLink, string id)
        {
            DocumentCollection collection = client.CreateDocumentCollectionQuery(dbLink).Where(c => c.Id == id).ToArray().FirstOrDefault();
            if (collection == null)
            {
                collection = await client.CreateDocumentCollectionAsync(dbLink, new DocumentCollection { Id = id });
            }

            return collection;
        }

        /// <summary>
        /// Get a Database by id, or create a new one if one with the id provided doesn't exist.
        /// </summary>
        /// <param name="id">The id of the Database to search for, or create.</param>
        /// <returns>The matched, or created, Database object</returns>
        private static async Task<Database> GetOrCreateDatabaseAsync(string id)
        {
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == id).ToArray().FirstOrDefault();
            if (database == null)
            {
                database = await client.CreateDatabaseAsync(new Database { Id = id });
            }

            return database;
        }
    }
}