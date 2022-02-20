using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WorklogService.Models.Handler.Reports;
using WorklogService.Models.Reports;
using WorklogService.Models.Worklogs;
using WorklogService.Services.Workday;
using Xunit;

namespace WorklogServiceTest
{
    public class DailyReportHandlerTest
    {
        private readonly Mock<IWorkdayService> _workdayService;
        private long _createdDateTime = new DateTimeOffset(2022, 1, 12, 13, 30, 0, TimeSpan.FromHours(1)).ToUnixTimeMilliseconds();
        private long _startDateTime = new DateTimeOffset(2022, 1, 12, 10, 30, 0, TimeSpan.FromHours(1)).ToUnixTimeMilliseconds();
        public DailyReportHandlerTest()
        {
            _workdayService = new Mock<IWorkdayService>();
        }

        [Fact]
        public void AddNullWorklogTest()
        {
            //Arrange
            DailyReportHandler dailyReportHandler = new DailyReportHandler(_workdayService.Object);

            //Act / Assert
            Assert.Throws<ArgumentNullException>(() => dailyReportHandler.Add(null));
        }

        [Fact]
        public void ReportCorrectWorklogOnSamedayTest()
        {
            //Arrange
            DateTime date = new DateTime(2022, 1, 12);
            DailyReportHandler dailyReportHandler = new DailyReportHandler(_workdayService.Object);
            Report report = new Report();
            dailyReportHandler.Add(new WorklogModel("user_1", "user_1", _createdDateTime, _startDateTime, 3600));
            Report expectedReport = new Report
            {
                Daily = new List<DailyReport>
                {
                    new DailyReport
                    {
                        Date = date,
                        UserPercents = new List<UserPercent>
                        {
                            new UserPercent
                            {
                                UserId = "user_1",
                                Percent = 100
                            }
                        }
                    }
                }.AsEnumerable()
            };

            //Act
            dailyReportHandler.Report(report);

            //Assert
            Assert.Null(report.Weekly);
            Assert.True(expectedReport.Daily.SequenceEqual(report.Daily));

        }

        [Fact]
        public void ReportCorrectInCorrectWorklogTest()
        {
            //Arrange
            _workdayService.Setup(s => s.GetUserPreviousWorkday(It.IsAny<DateTime>(), It.IsAny<string>()))
                           .Returns(new DateTime(2022, 1, 12));
            DateTime date = new DateTime(2022, 1, 12);
            DailyReportHandler dailyReportHandler = new DailyReportHandler(_workdayService.Object);
            Report report = new Report();
            long incorrectCreatedUnixTime = new DateTimeOffset(2022, 1, 13, 11, 30, 0,TimeSpan.FromHours(1)).ToUnixTimeMilliseconds();
            dailyReportHandler.Add(new WorklogModel("user_1", "user_1", _createdDateTime, _startDateTime, 3600));
            dailyReportHandler.Add(new WorklogModel("user_1", "user_1", incorrectCreatedUnixTime, _startDateTime, 3600));
            Report expectedReport = new Report
            {
                Daily = new List<DailyReport>
                {
                    new DailyReport
                    {
                        Date = date,
                        UserPercents = new List<UserPercent>
                        {
                            new UserPercent
                            {
                                UserId = "user_1",
                                Percent = 50
                            }
                        }
                    }
                }.AsEnumerable()
            };

            //Act
            dailyReportHandler.Report(report);

            //Assert
            Assert.Null(report.Weekly);
            Assert.True(expectedReport.Daily.SequenceEqual(report.Daily));

        }

        [Fact]
        public void ReportTwoCorrectWorklogTest()
        {
            //Arrange
            _workdayService.Setup(s => s.GetUserPreviousWorkday(It.IsAny<DateTime>(), It.IsAny<string>()))
                           .Returns(new DateTime(2022, 1, 12));
            DateTime date = new DateTime(2022, 1, 12);
            DailyReportHandler dailyReportHandler = new DailyReportHandler(_workdayService.Object);
            Report report = new Report();
            long correctCreatedUnixTime = new DateTimeOffset(2022, 1, 13, 9, 30, 0, TimeSpan.FromHours(1)).ToUnixTimeMilliseconds();
            dailyReportHandler.Add(new WorklogModel("user_1", "user_1", _createdDateTime, _startDateTime, 3600));
            dailyReportHandler.Add(new WorklogModel("user_1", "user_1", correctCreatedUnixTime, _startDateTime, 3600));
            Report expectedReport = new Report
            {
                Daily = new List<DailyReport>
                {
                    new DailyReport
                    {
                        Date = date,
                        UserPercents = new List<UserPercent>
                        {
                            new UserPercent
                            {
                                UserId = "user_1",
                                Percent = 100
                            }
                        }
                    }
                }.AsEnumerable()
            };

            //Act
            dailyReportHandler.Report(report);

            //Assert
            Assert.Null(report.Weekly);
            Assert.True(expectedReport.Daily.SequenceEqual(report.Daily));

        }

        [Fact]
        public void ReportCorrectInCorrectNotPreviousDayWorklogTest()
        {
            //Arrange
            _workdayService.Setup(s => s.GetUserPreviousWorkday(It.IsAny<DateTime>(), It.IsAny<string>()))
                           .Returns(new DateTime(2022, 1, 13));
            DateTime date = new DateTime(2022, 1, 12);
            DailyReportHandler dailyReportHandler = new DailyReportHandler(_workdayService.Object);
            Report report = new Report();
            dailyReportHandler.Add(new WorklogModel("user_1", "user_1", _createdDateTime, _startDateTime, 3600));
            dailyReportHandler.Add(new WorklogModel("user_1", "user_1", new DateTimeOffset(2022, 1, 14, 11, 30, 0, TimeSpan.Zero).ToUnixTimeMilliseconds(), _startDateTime, 3600)); //1642069800000
            Report expectedReport = new Report
            {
                Daily = new List<DailyReport>
                {
                    new DailyReport
                    {
                        Date = date,
                        UserPercents = new List<UserPercent>
                        {
                            new UserPercent
                            {
                                UserId = "user_1",
                                Percent = 50
                            }
                        }
                    }
                }.AsEnumerable()
            };

            //Act
            dailyReportHandler.Report(report);

            //Assert
            Assert.Null(report.Weekly);
            Assert.True(expectedReport.Daily.SequenceEqual(report.Daily));

        }

        [Fact]
        public void ReportCorrectInCorrectNotPreviousDayWorklogTwoUserTest()
        {
            //Arrange
            _workdayService.Setup(s => s.GetUserPreviousWorkday(It.IsAny<DateTime>(), It.IsAny<string>()))
                           .Returns(new DateTime(2022, 1, 13));
            DateTime date = new DateTime(2022, 1, 12);
            DailyReportHandler dailyReportHandler = new DailyReportHandler(_workdayService.Object);
            Report report = new Report();
            dailyReportHandler.Add(new WorklogModel("user_1", "user_1", new DateTimeOffset(2022, 1, 12, 11, 30, 0, TimeSpan.Zero).ToUnixTimeMilliseconds(), new DateTimeOffset(2022, 1, 12, 9, 30, 0, TimeSpan.Zero).ToUnixTimeMilliseconds(), 3600));
            dailyReportHandler.Add(new WorklogModel("user_1", "user_1", new DateTimeOffset(2022, 1, 14, 11, 30, 0, TimeSpan.Zero).ToUnixTimeMilliseconds(), _startDateTime, 3600));
            dailyReportHandler.Add(new WorklogModel("user_2", "user_2", new DateTimeOffset(2022, 1, 12, 11, 30, 0, TimeSpan.Zero).ToUnixTimeMilliseconds(), new DateTimeOffset(2022, 1, 12, 10, 30, 0, TimeSpan.Zero).ToUnixTimeMilliseconds(), 3600));
            Report expectedReport = new Report
            {
                Daily = new List<DailyReport>
                {
                    new DailyReport
                    {
                        Date = date,
                        UserPercents = new List<UserPercent>
                        {
                            new UserPercent
                            {
                                UserId = "user_1",
                                Percent = 50
                            },
                            new UserPercent
                            {
                                UserId = "user_2",
                                Percent = 100
                            }
                        }
                    }
                }.AsEnumerable()
            };

            //Act
            dailyReportHandler.Report(report);

            //Assert
            Assert.Null(report.Weekly);
            Assert.True(expectedReport.Daily.SequenceEqual(report.Daily));

        }

        [Fact]
        public void ReportIsNullTest()
        {
            //Arrange
            DailyReportHandler dailyReportHandler = new DailyReportHandler(_workdayService.Object);

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => dailyReportHandler.Report(null));

        }
    }
}
