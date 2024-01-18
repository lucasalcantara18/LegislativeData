using LegislativeData.Domain;
using LegislativeData.Domain.Enum;
using LegislativeData.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LegislativeData
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ICsvHelperImpl _csvHelper;

        public Worker(ILogger<Worker> logger, 
                      IConfiguration configuration, 
                      IHostApplicationLifetime applicationLifetime, 
                      ICsvHelperImpl csvHelper)
        {
            _logger = logger;
            _configuration = configuration;
            _applicationLifetime = applicationLifetime;
            _csvHelper = csvHelper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var PATH_BASE = Path.Combine(Directory.GetCurrentDirectory());

            var path_bills = Path.Combine(PATH_BASE, _configuration.GetValue<string>("BillsFile"));
            var path_lagislators = Path.Combine(PATH_BASE, _configuration.GetValue<string>("LegislatorsFile"));
            var path_votesResults = Path.Combine(PATH_BASE, _configuration.GetValue<string>("VoteResultsFile"));
            var path_votes = Path.Combine(PATH_BASE, _configuration.GetValue<string>("VotesFile"));


            var bills = _csvHelper.ReadBillsCsv(path_bills);
            var legislators = _csvHelper.ReadLegislatorsCsv(path_lagislators);
            var votesResults = _csvHelper.ReadVotesResultCsv(path_votesResults);
            var votes = _csvHelper.ReadVotesCsv(path_votes);

            while (!stoppingToken.IsCancellationRequested)
            {

                var votesPerLegislators = votesResults
                                          .GroupBy(voteResult => voteResult.Legislator_id)
                                          .Select(votesResultPerLegislators =>
                                          {
                                              var votesOpposedPerLegislator = votesResultPerLegislators.Count(numVotes => numVotes.Vote_type == 2);
                                              var votesSupportedPerLegislator = votesResultPerLegislators.Count(numVotes => numVotes.Vote_type == 1);
                                              var legislator = legislators.TryGetValue(votesResultPerLegislators.Key, out var nameLegislator);

                                              return new LegislatorsSupport
                                              {
                                                  Id = votesResultPerLegislators.Key,
                                                  Name = legislator ? nameLegislator : "Unknown",
                                                  Num_opposed_bills = votesOpposedPerLegislator,
                                                  Num_supported_bills = votesSupportedPerLegislator
                                              };
                                          });

                var votesPerBills = votesResults
                                          .GroupBy(voteResult => voteResult.Vote_id)
                                          .Select(votesResultPerBills =>
                                          {
                                              var existVote = votes.TryGetValue(votesResultPerBills.Key, out var vote);
                                              var existBill = bills.TryGetValue(vote.Bill_id, out var bill);
                                              var votesOpposedCount = votesResultPerBills.Count(numVotes => numVotes.Vote_type == 2);
                                              var votesSupportedCount = votesResultPerBills.Count(numVotes => numVotes.Vote_type == 1);

                                              return new BillsResult
                                              {
                                                  Id = bill.Id,
                                                  Title = bill.Title,
                                                  Opposer_count = votesOpposedCount,
                                                  Suporter_count = votesSupportedCount,
                                                  Primary_sponsor = legislators.TryGetValue(bill.Sponsor_id, out var legislator) ? legislator : "Unknow"
                                              };
                                          });

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }

            _applicationLifetime.StopApplication();
        }
    }
}
