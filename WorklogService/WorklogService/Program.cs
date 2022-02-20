using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WorklogService.DataAccess;
using WorklogService.DataAccess.Reports;
using WorklogService.Extensions;
using WorklogService.Handlers.Reports;
using WorklogService.Models.Reports;
using WorklogService.Reporter;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                              .Build();

var logger = new LoggerConfiguration()
                  .MinimumLevel.Debug()
                  .WriteTo.File("worklogservice.log")
                  .WriteTo.Console()
                  .CreateLogger();

var serviceProvider = new ServiceCollection()
                        .AddConfiguration(configuration)
                        .AddServices()
                        .BuildServiceProvider();

WorklogReport reporter = new WorklogReport(serviceProvider.GetRequiredService<IWorklogData>(), serviceProvider.GetServices<IReportHandler>(), logger);
try
{
    logger.Information("Create report");
    Report report = reporter.CreateReport();
    IReportData reportData = serviceProvider.GetRequiredService<IReportData>();
    reportData.Write(report);
}
catch (Exception e)
{
    logger.Error(e.Message);
}
finally
{
    logger.Information("End service");
}