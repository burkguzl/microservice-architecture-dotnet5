using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Core.Settings.Abstract
{
    public interface IPhonebookDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
