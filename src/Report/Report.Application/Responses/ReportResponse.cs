using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Application.Responses
{
    public class ReportResponse
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
        public string Status { get; set; }
    }
}
