using MongoDB.Driver;
using Report.Core.Data;
using Report.Core.Entities;
using Report.Core.Enums;
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

        public async Task UpdateAsync(string reportId, int totalPerson, int totalPhoneNumber)
        {

            var report = await _context.Reports.Find(i => i.Id == reportId).FirstOrDefaultAsync();

            await _context.Reports.UpdateOneAsync(Builders<ReportEntity>.Filter.Eq(i => i.Id, reportId),
                Builders<ReportEntity>.Update.Set(i => i.PersonCount, totalPerson)
                                             .Set(i => i.PhoneCount, totalPhoneNumber)
                                             .Set(i => i.Status, ReportStatusEnum.Prepared.ToString()));

        }
    }
}
