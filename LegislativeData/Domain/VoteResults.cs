using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislativeData.Domain
{
    public class VoteResults
    {
        public int Id { get; set; }
        public int Lagislator_id { get; set; }
        public int Vote_id { get; set; }
        public int Vote_type {  get; set; }
    }
}
