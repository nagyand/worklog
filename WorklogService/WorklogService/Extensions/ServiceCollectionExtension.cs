using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorklogService.DataAccess;
using WorklogService.DataAccess.Holiday;
using WorklogService.DataAccess.Reports;
using WorklogService.DataAccess.WorkdayException;
using WorklogService.Handlers.Reports;
using WorklogService.Models.Configuration;
using WorklogService.Services;
using WorklogService.Services.Workday;

namespace WorklogService.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<HolidayConfiguration>(holiday => holiday.FilePath = config.GetSection("holiday:filePath").Value)
                    .Configure<ReportConfiguration>(report => report.ReportPath = config.GetSection("report:filePath").Value)
                    .Configure<WorklogConfiguration>(worklog => worklog.FilePath = config.GetSection("worklog:filePath").Value)
                    .Configure<WorkdayExceptionConfiguration>(workday => workday.FilePath = config.GetSection("holiday:filePath").Value);
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IHolidayData, HolidayData>()
                    .AddSingleton<IWorkdayExceptionData, WorkdayExceptionData>()
                    .AddSingleton<IWorklogData, WorklogData>()
                    .AddSingleton<IHolidayService, HolidayService>()
                    .AddSingleton<IWorkdayService, WorkdayService>()
                    .AddSingleton<IReportHandler, DailyReportHandler>()
                    .AddSingleton<IReportHandler, WeeklyReportHandler>()
                    .AddSingleton<IReportData, ReportData>();
            return services;
        }
    }
}
