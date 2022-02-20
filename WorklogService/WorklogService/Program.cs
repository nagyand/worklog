// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using WorklogService.DataAccess;
using WorklogService.DataAccess.Holiday;
using WorklogService.DataAccess.Reports;
using WorklogService.DataAccess.WorkdayException;
using WorklogService.Models.Configuration;
using WorklogService.Models.Handler.Reports;
using WorklogService.Models.Reports;
using WorklogService.Reporter;
using WorklogService.Services;
using WorklogService.Services.Workday;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                              .Build();

var logger = new LoggerConfiguration()
                  .MinimumLevel.Debug()
                  .WriteTo.File("workerservice.log")
                  .WriteTo.Console()
                  .CreateLogger();

var serviceProvider = new ServiceCollection()
    .Configure<HolidayConfiguration>(holiday => holiday.FilePath = configuration.GetSection("holiday:filePath").Value)
    .Configure<ReportConfiguration>(report => report.ReportPath = configuration.GetSection("report:filePath").Value)
    .Configure<WorklogConfiguration>(worklog => worklog.FilePath = configuration.GetSection("worklog:filePath").Value)
    .Configure<WorkdayExceptionConfiguration>(workday => workday.FilePath = configuration.GetSection("holiday:filePath").Value)
    .AddSingleton<IHolidayData, HolidayData>()
    .AddSingleton<IWorkdayExceptionData, WorkdayExceptionData>()
    .AddSingleton<IWorklogData, WorklogData>()
    .AddSingleton<IHolidayService, HolidayService>()
    .AddSingleton<IWorkdayService, WorkdayService>()
    .AddSingleton<IReportHandler, DailyReportHandler>()
    .AddSingleton<IReportHandler, WeeklyReportHandler>()
    .AddSingleton<IReportData, ReportData>()
    .BuildServiceProvider();
var reporter = new WorklogReport(serviceProvider.GetRequiredService<IWorklogData>(), serviceProvider.GetServices<IReportHandler>(), logger);
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