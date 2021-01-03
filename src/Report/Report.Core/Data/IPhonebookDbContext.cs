using MongoDB.Driver;
using Report.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Core.Data
{
    public interface IPhonebookDbContext
    {
        IMongoCollection<ReportEntity> Reports { get; }
    }
}
