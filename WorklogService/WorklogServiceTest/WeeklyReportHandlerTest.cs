using System;
using System.Collections.Generic;
using System.Linq;
using WorklogService.Handlers.Reports;
using WorklogService.Models.Reports;
using WorklogService.Models.Worklogs;
using Xunit;

namespace WorklogServiceTest
{
    public class WeeklyReportHandlerTest
    {
        private readonly WeeklyReportHandler _handler;
        public WeeklyReportHandlerTest()
        {
            _handler = new WeeklyReportHandler();
        }
        [Fact]
        public void AddNullWorklogToWeeklyHandlerTest()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => _handler.Add(null));
        }

        [Fact]
        public void AddOneWorklogToTheHandlerGetReportTest()
        {
            //Arrange
            Report report = new Report();
            Report expectedReport = new Report
            {
                Weekly = new List<WeeklyReport>
                {
                    new WeeklyReport
                    {
                        Start = new DateTime(2022,2,14),
                        UserPercents = new List<UserPercent>
                        {
                            new UserPercent
                            {
                                UserId = "user_1",
                                Percent = 10
                            }
                        }
                    }
                }.AsEnumerable()
            };

            //Act
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 11520));
            _handler.Report(report);

            //Assert
            Assert.Null(report.Daily);
            Assert.True(expectedReport.Weekly.SequenceEqual(report.Weekly));


        }

        [Fact]
        public void AddMoreWorklogToTheHandlerGetReportTest()
        {
            //Arrange
            Report report = new Report();
            Report expectedReport = new Report
            {
                Weekly = new List<WeeklyReport>
                {
                    new WeeklyReport
                    {
                        Start = new DateTime(2022,2,14),
                        UserPercents = new List<UserPercent>
                        {
                            new UserPercent
                            {
                                UserId = "user_1",
                                Percent = 33.12
                            }
                        }
                    }
                }.AsEnumerable()
            };

            //Act
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 11520));
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 23040));
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 3600));
            _handler.Report(report);

            //Assert
            Assert.Null(report.Daily);
            Assert.True(expectedReport.Weekly.SequenceEqual(report.Weekly));


        }

        [Fact]
        public void AddMoreWorklogMoreUserToTheHandlerGetReportTest()
        {
            //Arrange
            Report report = new Report();
            Report expectedReport = new Report
            {
                Weekly = new List<WeeklyReport>
                {
                    new WeeklyReport
                    {
                        Start = new DateTime(2022,2,14),
                        UserPercents = new List<UserPercent>
                        {
                            new UserPercent
                            {
                                UserId = "user_1",
                                Percent = 33.12
                            },
                            new UserPercent
                            {
                                UserId = "user_2",
                                Percent = 10
                            }
                        }
                    }
                }.AsEnumerable()
            };

            //Act
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 11520));
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 23040));
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 3600));
            _handler.Add(new WorklogModel("user_2", "user_2", 1644919200000, 1644919200000, 11520));
            _handler.Report(report);

            //Assert
            Assert.Null(report.Daily);
            Assert.True(expectedReport.Weekly.SequenceEqual(report.Weekly));


        }

        [Fact]
        public void AddMoreWorklogMoreWeeksToTheHandlerGetReportTest()
        {
            //Arrange
            Report report = new Report();
            Report expectedReport = new Report
            {
                Weekly = new List<WeeklyReport>
                {
                    new WeeklyReport
                    {
                        Start = new DateTime(2022,1,10),
                        UserPercents = new List<UserPercent>
                        {
                            new UserPercent
                            {
                                UserId = "user_1",
                                Percent = 10
                            }
                        }
                    },
                    new WeeklyReport
                    {
                        Start = new DateTime(2022,2,14),
                        UserPercents = new List<UserPercent>
                        {
                            new UserPercent
                            {
                                UserId = "user_1",
                                Percent = 33.12
                            }
                        }
                    }
                }.AsEnumerable()
            };

            //Act
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 11520));
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 23040));
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 3600));
            _handler.Add(new WorklogModel("user_1", "user_1", 1641979800000, 1641979800000, 11520));
            _handler.Report(report);

            //Assert
            Assert.Null(report.Daily);
            Assert.True(expectedReport.Weekly.SequenceEqual(report.Weekly));
        }

        [Fact]
        public void AddMoreWorklogMoreWeeksMoreUsersToTheHandlerGetReportTest()
        {
            //Arrange
            Report report = new Report();
            Report expectedReport = new Report
            {
                Weekly = new List<WeeklyReport>
                {
                    new WeeklyReport
                    {
                        Start = new DateTime(2022,1,10),
                        UserPercents = new List<UserPercent>
                        {
                            new UserPercent
                            {
                                UserId = "user_1",
                                Percent = 10
                            },
                            new UserPercent
                            {
                                UserId = "user_2",
                                Percent = 10
                            }
                        }
                    },
                    new WeeklyReport
                    {
                        Start = new DateTime(2022,2,14),
                        UserPercents = new List<UserPercent>
                        {
                            new UserPercent
                            {
                                UserId = "user_1",
                                Percent = 33.12
                            },
                            new UserPercent
                            {
                                UserId = "user_2",
                                Percent = 10
                            }
                        }
                    }
                }.AsEnumerable()
            };

            //Act
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 11520));
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 23040));
            _handler.Add(new WorklogModel("user_1", "user_1", 1644919200000, 1644919200000, 3600));
            _handler.Add(new WorklogModel("user_2", "user_2", 1644919200000, 1644919200000, 11520));
            _handler.Add(new WorklogModel("user_1", "user_1", 1641979800000, 1641979800000, 11520));
            _handler.Add(new WorklogModel("user_2", "user_2", 1641979800000, 1641979800000, 11520));
            _handler.Report(report);

            //Assert
            Assert.Null(report.Daily);
            Assert.True(expectedReport.Weekly.SequenceEqual(report.Weekly));
        }
    }
}
