using MongoDB.Bson;
using MongoDB.Driver;
using Report.Core.Entities;
using Report.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Infrastructure.Data
{
    public class PhonebookDbSeedData
    {
        public static void SeedData(IMongoCollection<ReportEntity> reportCollection)
        {
            bool IsPersonExist = reportCollection.Find(i => true).Any();
            if (!IsPersonExist)
            {
                reportCollection.InsertManyAsync(new List<ReportEntity>
                {
                    new ReportEntity
                    {
                         Id = ObjectId.GenerateNewId().ToString(),
                          Date = DateTime.Now,
                           Location = "Istanbul",
                            PersonCount = 3,
                             PhoneCount = 4,
                              Status = ReportStatusEnum.Prepared.ToString()
                    },
                    new ReportEntity
                    {
                         Id = ObjectId.GenerateNewId().ToString(),
                          Date = DateTime.Now,
                            Status = ReportStatusEnum.Preparing.ToString()
                    },
                    new ReportEntity
                    {
                         Id = ObjectId.GenerateNewId().ToString(),
                          Date = DateTime.Now,
                           Location = "Izmir",
                            PersonCount = 10,
                             PhoneCount = 20,
                              Status = ReportStatusEnum.Prepared.ToString()
                    }

                });
            }
        }
    }
}
