using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using WorklogService.DataAccess;
using WorklogService.Handlers.Reports;
using WorklogService.Models.Reports;
using WorklogService.Models.Worklogs;
using WorklogService.Reporter;
using Xunit;

namespace WorklogServiceTest
{
    public class WorklogReportTest
    {
        private readonly Mock<ILogger> _loggerMock;
        private readonly Mock<IWorklogData> _worklogDataMock;
        public WorklogReportTest()
        {
            _loggerMock = new Mock<ILogger>();
            _worklogDataMock = new Mock<IWorklogData>();
        }

        [Fact]
        public void CreateReportNoWorklogTest()
        {
            //Arrange
            _worklogDataMock.Setup(s => s.GetWorklogs()).Returns(new List<WorklogModel> ());
            WorklogReport report = new WorklogReport(_worklogDataMock.Object, new List<IReportHandler>(), _loggerMock.Object);

            //Act
            Report generatedReport = report.CreateReport();

            //Assert
            Assert.Null(generatedReport.Daily);
            Assert.Null(generatedReport.Weekly);
        }

        [Fact]
        public void CreateReportNoHandlerTest()
        {
            //Arrange
            _worklogDataMock.Setup(s => s.GetWorklogs()).Returns(new List<WorklogModel> { new WorklogModel("user-1","user-2",3,3,3600) });
            WorklogReport report = new WorklogReport(_worklogDataMock.Object, new List<IReportHandler>(), _loggerMock.Object);

            //Act
            Report generatedReport = report.CreateReport();

            //Assert
            Assert.Null(generatedReport.Daily);
            Assert.Null(generatedReport.Weekly);
        }

        [Fact]
        public void CreateReportOneHandlerTest()
        {
            //Arrange
            _worklogDataMock.Setup(s => s.GetWorklogs()).Returns(new List<WorklogModel> { new WorklogModel("user-1", "user-2", 3, 3, 3600) });
            Mock<IReportHandler> reportHandlerMock = new Mock<IReportHandler>();
            reportHandlerMock.Setup(s => s.Report(It.IsAny<Report>())).Callback((Report report) => report.Daily = new List<DailyReport>());
            WorklogReport report = new WorklogReport(_worklogDataMock.Object, new List<IReportHandler> { reportHandlerMock.Object}, _loggerMock.Object);

            //Act
            Report generatedReport = report.CreateReport();

            //Assert
            Assert.NotNull(generatedReport.Daily);
            Assert.Null(generatedReport.Weekly);
        }

        [Fact]
        public void CreateReportTwoHandlerTest()
        {
            //Arrange
            _worklogDataMock.Setup(s => s.GetWorklogs()).Returns(new List<WorklogModel> { new WorklogModel("user-1", "user-2", 3, 3, 3600) });
            Mock<IReportHandler> dailyReportHandlerMock = new Mock<IReportHandler>();
            dailyReportHandlerMock.Setup(s => s.Report(It.IsAny<Report>())).Callback((Report report) => report.Daily = new List<DailyReport>());
            Mock<IReportHandler> weeklyReportHandlerMock = new Mock<IReportHandler>();
            weeklyReportHandlerMock.Setup(s => s.Report(It.IsAny<Report>())).Callback((Report report) => report.Weekly = new List<WeeklyReport>());
            WorklogReport report = new WorklogReport(_worklogDataMock.Object, new List<IReportHandler> {weeklyReportHandlerMock.Object,dailyReportHandlerMock.Object }, _loggerMock.Object);

            //Act
            Report generatedReport = report.CreateReport();

            //Assert
            Assert.NotNull(generatedReport.Daily);
            Assert.NotNull(generatedReport.Weekly);
        }

        [Fact]
        public void CreateWorklogReportNullWorklogData()
        {
            //Arrange / Act / Assert
            ArgumentException exception =  Assert.Throws<ArgumentNullException>(() => new WorklogReport(null,new List<IReportHandler>(), _loggerMock.Object));
            Assert.Equal("Value cannot be null. (Parameter 'worklogData')", exception.Message);
        }

        [Fact]
        public void CreateWorklogReportNullLogger()
        {
            //Arrange / Act / Assert
            ArgumentException exception = Assert.Throws<ArgumentNullException>(() => new WorklogReport(_worklogDataMock.Object, new List<IReportHandler>(), null));
            Assert.Equal("Value cannot be null. (Parameter 'logger')", exception.Message);
        }
    }
}
