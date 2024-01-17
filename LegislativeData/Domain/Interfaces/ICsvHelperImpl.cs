using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislativeData.Domain.Interfaces
{
    public interface ICsvHelperImpl
    {
        public IEnumerable<Bills> ReadBillsCsv(string path);
        public IEnumerable<Legislators> ReadLegislatorsCsv(string path);
        public IEnumerable<VoteResults> ReadVotesResultCsv(string path);
        public IEnumerable<Votes> ReadVotesCsv(string path);
    }
}
