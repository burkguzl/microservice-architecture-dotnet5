using Report.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Report.Core.Repositories
{
    public interface IReportRepository
    {
        Task CreateAsync(ReportEntity report);
        Task<List<ReportEntity>> GetAllAsync();
        Task<ReportEntity> GetAsync(string id);
    }
}
