using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislativeData.Domain
{
    public class BillsResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Suporter_count { get; set; }
        public int Opposer_count { get; set; }
        public string Primary_sponsor { get; set; }
    }
}
