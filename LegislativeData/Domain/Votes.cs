using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislativeData.Domain
{
    public class Votes
    {
        [Name("id")]
        public int Id { get; set; }
        [Name("bill_id")]
        public int Bill_id { get; set; }
    }
}
