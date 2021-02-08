#region header

// MongrelWiki - PageInitHelpers.cs
// 
// Created by: Alistair J R Young (avatar) at 2021/02/08 2:29 PM.

#endregion

#region using

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ArkaneSystems.MongrelWiki.Models;
using ArkaneSystems.MongrelWiki.Services;

using MongoDB.Bson;
using MongoDB.Driver;

#endregion

namespace ArkaneSystems.MongrelWiki.Helpers.Initialization
{
    public static class PageInitHelpers
    {
        public static async Task<bool> PerformDatabaseInitSteps (this WikiService wiki)
        {
            var actionNeeded = false;

            actionNeeded |= await wiki.EnsureIndexPageExists ();
            actionNeeded |= await wiki.EnsurePageIndicesExist ();

            return actionNeeded;
        }

        private static async Task<bool> EnsureIndexPageExists (this WikiService wiki)
        {
            if (!await wiki.CheckPageExists (slug: "index"))
            {
                var indexPage = new Page (slug: "index", title: "Welcome!");

                await wiki.CreatePageAsync (page: indexPage);

                return true;
            }

            return false;
        }

        private static async Task<bool> EnsurePageIndicesExist (this WikiService wiki)
        {
            var wikiCollation = new BsonDocument
                                {
                                    [name: "locale"]          = new BsonString (value: "en"),
                                    [name: "strength"]        = new BsonInt32 (value: 2),
                                    [name: "caseLevel"]       = new BsonBoolean (value: false),
                                    [name: "numericOrdering"] = new BsonBoolean (value: true),
                                };

            var indices = new List<CreateIndexModel<Page>>
                          {
                              new (
                                   keys: new JsonIndexKeysDefinition<Page> (json: "{ slug: 1 }"),
                                   options: new CreateIndexOptions
                                            {
                                                Unique = true,
                                                Collation =
                                                    Collation.FromBsonDocument (document: wikiCollation),
                                                Background = true,
                                            }
                                  ),
                              new (
                                   keys: new JsonIndexKeysDefinition<Page> (json: "{ title: 1 }"),
                                   options: new CreateIndexOptions
                                            {
                                                Unique = true,
                                                Collation =
                                                    Collation.FromBsonDocument (document: wikiCollation),
                                                Background = true,
                                            }
                                  ),
                          };

            IEnumerable<string>? created = await wiki.Pages.Indexes.CreateManyAsync (models: indices);

            if (created?.Count () > 0)
                return true;

            return false;
        }
    }
}
