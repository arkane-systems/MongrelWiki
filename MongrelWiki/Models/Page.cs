#region header

// MongrelWiki - Page.cs
// 
// Created by: Alistair J R Young (avatar) at 2021/02/08 8:43 AM.

#endregion

#region using

using System;
using System.ComponentModel.DataAnnotations;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace ArkaneSystems.MongrelWiki.Models
{
    public class Page
    {
        [BsonId]
        [BsonRepresentation (representation: BsonType.ObjectId)]
        public string Id { get; set; }

        [Required (ErrorMessage = "Page slug is required.")]
        public string Slug { get; set; }

        [Required (ErrorMessage = "Page title is required.")]
        public string Title { get; set; }

        [BsonRepresentation (representation: BsonType.Int32)]
        public PageType Type { get; set; }

        [BsonDateTimeOptions (Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
        public DateTime LastChanged { get; set; }
    }
}
