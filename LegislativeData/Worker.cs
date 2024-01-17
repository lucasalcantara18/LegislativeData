using Application.Services;
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

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IHostApplicationLifetime applicationLifetime, ICsvHelperImpl csvHelper)
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


            var result = _csvHelper.ReadBillsCsv(path_bills);

            while (!stoppingToken.IsCancellationRequested)
            {
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
