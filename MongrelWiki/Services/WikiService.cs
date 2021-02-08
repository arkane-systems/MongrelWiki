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
        private readonly IMongoDatabase database;

        private readonly DatabaseConfiguration settings;

        public WikiService (IOptions<DatabaseConfiguration> settings)
        {
            this.settings = settings.Value;

            var client = new MongoClient (connectionString: this.settings.ConnectionString);
            this.database = client.GetDatabase (name: this.settings.DatabaseName);
            this.pages    = this.database.GetCollection<Page> (name: "pages");
        }

        #region Page collection

        private readonly IMongoCollection<Page> pages;

        public async Task<List<Page>> GetAllPagesAsync () => await this.pages.Find (filter: p => true).ToListAsync ();

        public async Task<Page> GetPageByIdAsync (string id)
            => await this.pages.Find (filter: p => p.Id == id).FirstOrDefaultAsync ();

        public async Task<Page> CreatePageAsync (Page page)
        {
            await this.pages.InsertOneAsync (document: page);

            return page;
        }

        public async Task UpdatePageAsync (string id, Page page)
            => await this.pages.ReplaceOneAsync (filter: p => p.Id == id, replacement: page);

        public async Task DeletePageAsync (string id) => await this.pages.DeleteOneAsync (filter: p => p.Id == id);

        #endregion Page collection
    }
}
