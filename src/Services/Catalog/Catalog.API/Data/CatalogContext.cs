using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration )
        {
            // Step no 1: Get the connection string 
            var client = new MongoClient(configuration.GetValue<string>("DbSettings:ConnString"));
            // Step no 2: Get the database Name 
            var database = client.GetDatabase(configuration.GetValue<string>("DbSettings:DbName"));
            // Stepno 3: Get the collection Name Products
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DbSettings:CollectionName"));
            // Step no 4:  Seeding the data 
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
