using LegislativeData;
using LegislativeData.Application;
using LegislativeData.Domain.Interfaces;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<ICsvHelperImpl, CsvHelperImpl>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
