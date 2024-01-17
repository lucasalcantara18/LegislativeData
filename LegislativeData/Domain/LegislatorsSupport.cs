using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislativeData.Domain
{
    public class LegislatorsSupport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Num_supported_bills { get; set; }
        public int Num_opposed_bills { get; set; }
    }
}
