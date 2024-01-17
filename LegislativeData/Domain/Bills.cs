using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislativeData.Domain
{
    public class Bills
    {
        [Name("id")]
        public int Id { get; set; }
        [Name("title")]
        public string Title { get; set; }
        [Name("sponsor_id")]
        public int Sponsor_id { get; set; }
    }
}
