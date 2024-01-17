using CsvHelper;
using LegislativeData.Domain;
using LegislativeData.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislativeData.Application
{
    public class CsvHelperImpl : ICsvHelperImpl
    {
        public IEnumerable<Bills> ReadBillsCsv(string path)
        {
            ReadFile(path, out var csv);

            var records = csv.GetRecords<Bills>().ToHashSet();
            return records;
        }

        public IEnumerable<Legislators> ReadLegislatorsCsv(string path)
        {
            ReadFile(path, out var csv);

            var records = csv.GetRecords<Legislators>().ToList();
            return records;
        }

        public IEnumerable<Votes> ReadVotesCsv(string path)
        {
            ReadFile(path, out var csv);

            var records = csv.GetRecords<Votes>().ToList();
            return records;
        }

        public IEnumerable<VoteResults> ReadVotesResultCsv(string path)
        {
            ReadFile(path, out var csv);

            var records = csv.GetRecords<VoteResults>().ToList();
            return records;
        }

        private static void ReadFile(string path, out CsvReader csv)
        {
            var reader = new StreamReader(path);
            csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        }
    }
}
