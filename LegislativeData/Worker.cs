using LegislativeData.Domain;
using LegislativeData.Domain.Interfaces;
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

            var fileOutput1 = Path.Combine(PATH_BASE, _configuration.GetValue<string>("FileOutput1"));
            var fileOutput2 = Path.Combine(PATH_BASE, _configuration.GetValue<string>("FileOutput2"));
            
            while (!stoppingToken.IsCancellationRequested)
            {
                if(!VerifyFiles(path_bills, path_lagislators, path_votesResults, path_votes))
                {
                    _logger.LogInformation("One of the data files was not found (bills.csv, legislators.csv, vote_results.csv or votes.csvs), please, put on the right folder");
                    await Task.Delay(5000, stoppingToken);
                }

                if (VerifyFiles(path_bills, path_lagislators, path_votesResults, path_votes))
                {
                    var bills = _csvHelper.ReadBillsCsv(path_bills);
                    var legislators = _csvHelper.ReadLegislatorsCsv(path_lagislators);
                    var votesResults = _csvHelper.ReadVotesResultCsv(path_votesResults);
                    var votes = _csvHelper.ReadVotesCsv(path_votes);

                    var votesPerLegislators = votesResults
                                    .GroupBy(voteResult => voteResult.Legislator_id)
                                    .Select(votesResultPerLegislators =>
                                    {
                                        var votesOpposedPerLegislator = votesResultPerLegislators.Count(numVotes => numVotes.Vote_type == 2);
                                        var votesSupportedPerLegislator = votesResultPerLegislators.Count(numVotes => numVotes.Vote_type == 1);
                                        var legislator = legislators.TryGetValue(votesResultPerLegislators.Key, out var nameLegislator);

                                        return new LegislatorsSupport
                                        {
                                            id = votesResultPerLegislators.Key,
                                            name = legislator ? nameLegislator : "Unknown",
                                            num_opposed_bills = votesOpposedPerLegislator,
                                            num_supported_bills = votesSupportedPerLegislator
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
                                            id = bill.Id,
                                            title = bill.Title,
                                            opposer_count = votesOpposedCount,
                                            suporter_count = votesSupportedCount,
                                            primary_sponsor = legislators.TryGetValue(bill.Sponsor_id, out var legislator) ? legislator : "Unknow"
                                        };
                                    });

                    _csvHelper.WriteResults(votesPerLegislators, fileOutput1);
                    _csvHelper.WriteResults(votesPerBills, fileOutput2);
                    _logger.LogInformation("files successfully generated, check in the root path");
                    _applicationLifetime.StopApplication();
                }

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
            }
        }

        private static bool VerifyFiles(string path, string path2, string path3, string path4)
        {
            return Path.Exists(path) && Path.Exists(path2) && Path.Exists(path3) && Path.Exists(path4);
        }
    }
}
