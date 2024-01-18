using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislativeData.Domain.Interfaces
{
    public interface ICsvHelperImpl
    {
        public IDictionary<int, Bills> ReadBillsCsv(string path);
        public IDictionary<int, string> ReadLegislatorsCsv(string path);
        public IEnumerable<VoteResults> ReadVotesResultCsv(string path);
        public IDictionary<int, Votes> ReadVotesCsv(string path);
    }
}
