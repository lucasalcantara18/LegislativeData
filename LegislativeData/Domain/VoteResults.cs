using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislativeData.Domain
{
    public class VoteResults
    {
        [Name("id")]
        public int Id { get; set; }
        [Name("legislator_id")]
        public int Legislator_id { get; set; }
        [Name("vote_id")]
        public int Vote_id { get; set; }
        [Name("vote_type")]
        public int Vote_type {  get; set; }
    }
}
