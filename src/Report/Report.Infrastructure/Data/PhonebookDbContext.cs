using MongoDB.Driver;
using Report.Core.Data;
using Report.Core.Entities;
using Report.Core.Settings.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Infrastructure.Data
{
    public class PhonebookDbContext:IPhonebookDbContext
    {
        public PhonebookDbContext(IPhonebookDatabaseSettings phonebookDatabaseSettings)
        {
            var client = new MongoClient(phonebookDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(phonebookDatabaseSettings.DatabaseName);
            Reports = database.GetCollection<ReportEntity>(phonebookDatabaseSettings.CollectionName);

            //seed data
            PhonebookDbSeedData.SeedData(Reports);

        }
        public IMongoCollection<ReportEntity> Reports { get; set; }
    }
}
