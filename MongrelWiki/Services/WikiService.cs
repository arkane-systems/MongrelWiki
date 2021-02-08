#region header

// MongrelWiki - WikiService.cs
// 
// Created by: Alistair J R Young (avatar) at 2021/02/08 8:57 AM.

#endregion

#region using

using System.Collections.Generic;
using System.Threading.Tasks;

using ArkaneSystems.MongrelWiki.Configurations;
using ArkaneSystems.MongrelWiki.Models;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

#endregion

namespace ArkaneSystems.MongrelWiki.Services
{
    public class WikiService
    {
        private readonly DatabaseConfiguration settings;

        public WikiService (IOptions<DatabaseConfiguration> settings)
        {
            this.settings = settings.Value;

            var client = new MongoClient (connectionString: this.settings.ConnectionString);
            this.Database = client.GetDatabase (name: this.settings.DatabaseName);
            this.Pages    = this.Database.GetCollection<Page> (name: "pages");
        }

        internal IMongoDatabase Database { get; }

        #region Page collection

        internal IMongoCollection<Page> Pages { get; }

        public async Task<bool> CheckPageExists (string slug)
            => await this.Pages.CountDocumentsAsync (filter: p => p.Slug == slug) > 0;

        public async Task<List<Page>> GetAllPagesAsync () => await this.Pages.Find (filter: p => true).ToListAsync ();

        public async Task<Page> GetPageByIdAsync (string id)
            => await this.Pages.Find (filter: p => p.Id == id).FirstOrDefaultAsync ();

        public async Task<Page> CreatePageAsync (Page page)
        {
            await this.Pages.InsertOneAsync (document: page);

            return page;
        }

        public async Task UpdatePageAsync (string id, Page page)
            => await this.Pages.ReplaceOneAsync (filter: p => p.Id == id, replacement: page);

        public async Task DeletePageAsync (string id) => await this.Pages.DeleteOneAsync (filter: p => p.Id == id);

        #endregion Page collection
    }
}
