using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislativeData.Domain
{
    public class BillsResult
    {
        public int id { get; set; }
        public string title { get; set; }
        public int suporter_count { get; set; }
        public int opposer_count { get; set; }
        public string primary_sponsor { get; set; }
    }
}
