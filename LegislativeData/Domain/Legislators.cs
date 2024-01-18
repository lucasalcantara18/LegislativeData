using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislativeData.Domain
{
    public class Legislators
    {
        [Name("id")]
        public int Id { get; set; }
        [Name("name")]
        public string Name { get; set; }
    }
}
