using MongoDB.Driver;
using Report.Core.Data;
using Report.Core.Entities;
using Report.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Report.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly IPhonebookDbContext _context;

        public ReportRepository(IPhonebookDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ReportEntity report)
        {
            await _context.Reports.InsertOneAsync(report);
        }

        public async Task<ReportEntity> GetAsync(string id)
        {
            return await _context.Reports.Find(Builders<ReportEntity>.Filter.Eq(i => i.Id, id)).FirstOrDefaultAsync();

        }

        public async Task<List<ReportEntity>> GetAllAsync()
        {
            return await _context.Reports.Find(i => true).ToListAsync();

        }
    }
}
