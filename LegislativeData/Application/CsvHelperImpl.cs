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
        public IDictionary<int, Bills> ReadBillsCsv(string path)
        {
            ReadFile(path, out var csv);

            var records = csv.GetRecords<Bills>().ToList();

            return records.ToDictionary(bills => bills.Id, bills => bills);
        }

        public IDictionary<int, string> ReadLegislatorsCsv(string path)
        {
            ReadFile(path, out var csv);

            var records = csv.GetRecords<Legislators>().ToList();
                
            return records.ToDictionary(values => values.Id, values => values.Name);
        }

        public IDictionary<int, Votes> ReadVotesCsv(string path)
        {
            ReadFile(path, out var csv);

            var records = csv.GetRecords<Votes>().ToList();
            return records.ToDictionary(votes => votes.Id, votes => votes);
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
