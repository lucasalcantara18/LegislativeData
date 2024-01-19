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
            csv.Dispose();

            return records.ToDictionary(bills => bills.Id, bills => bills);
        }

        public IDictionary<int, string> ReadLegislatorsCsv(string path)
        {
            ReadFile(path, out var csv);

            var records = csv.GetRecords<Legislators>().ToList();
            csv.Dispose();

            return records.ToDictionary(values => values.Id, values => values.Name);
        }

        public IDictionary<int, Votes> ReadVotesCsv(string path)
        {
            ReadFile(path, out var csv);

            var records = csv.GetRecords<Votes>().ToList();
            csv.Dispose();

            return records.ToDictionary(votes => votes.Id, votes => votes);
        }

        public IEnumerable<VoteResults> ReadVotesResultCsv(string path)
        {
            ReadFile(path, out var csv);

            var records = csv.GetRecords<VoteResults>().ToList();
            csv.Dispose();

            return records;
        }

        public bool WriteResults<T>(IEnumerable<T> data, string path)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(data);
                return true;
            }
        }

        private static void ReadFile(string path, out CsvReader csv)
        {
            var reader = new StreamReader(path);
            csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        }
    }
}
