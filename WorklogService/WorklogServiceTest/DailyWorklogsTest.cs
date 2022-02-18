using System;
using System.Collections.Generic;
using WorklogService.Models.Reports;
using WorklogService.Models.Worklogs;
using Xunit;

namespace WorklogServiceTest
{
    public class DailyWorklogsTest
    {
        [Fact]
        public void DailyWorklogGetReportWithOneCorrectWorklogTest()
        {
            //Arrange
            DateTime worklogDate = new DateTime(2022, 2, 18);
            DailyWorklogs dailyWorklogs = new DailyWorklogs(worklogDate);
            dailyWorklogs.Add((new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 3600), WorklogType.Correct));
            DailyReport dailyReportExpected = new DailyReport
            {
                Date = worklogDate,
                UserPercents = new List<UserPercent>
                {
                    new UserPercent
                    {
                        UserId = "user_1",
                        Percent = 100
                    }
                }
            };
            //Act
            DailyReport report = dailyWorklogs.GetReport();

            //Assert
            Assert.Equal(dailyReportExpected, report);
        }

        [Fact]
        public void DailyWorklogGetReportWithTwoCorrectOneIncorrectWorklogTest()
        {
            //Arrange
            DateTime worklogDate = new DateTime(2022, 2, 18);
            DailyWorklogs dailyWorklogs = new DailyWorklogs(worklogDate);
            dailyWorklogs.Add((new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 3600), WorklogType.Correct));
            dailyWorklogs.Add((new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 7200), WorklogType.Correct));
            dailyWorklogs.Add((new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 1200), WorklogType.Incorrect));
            DailyReport dailyReportExpected = new DailyReport
            {
                Date = worklogDate,
                UserPercents = new List<UserPercent>
                {
                    new UserPercent
                    {
                        UserId = "user_1",
                        Percent = 90
                    }
                }
            };
            //Act
            DailyReport report = dailyWorklogs.GetReport();

            //Assert
            Assert.Equal(dailyReportExpected, report);
        }

        [Fact]
        public void DailyWorklogGetReportWithOneCorrectTwoUsersWorklogTest()
        {
            //Arrange
            DateTime worklogDate = new DateTime(2022, 2, 18);
            DailyWorklogs dailyWorklogs = new DailyWorklogs(worklogDate);
            dailyWorklogs.Add((new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 3600), WorklogType.Correct));
            dailyWorklogs.Add((new WorklogModel("user_2", "user_2", 1645185355, 1645178400, 1800), WorklogType.Correct));
            DailyReport dailyReportExpected = new DailyReport
            {
                Date = worklogDate,
                UserPercents = new List<UserPercent>
                {
                    new UserPercent
                    {
                        UserId = "user_1",
                        Percent = 100
                    },
                    new UserPercent
                    {
                        UserId = "user_2",
                        Percent = 100
                    }
                }
            };
            //Act
            DailyReport report = dailyWorklogs.GetReport();

            //Assert
            Assert.Equal(dailyReportExpected, report);
        }

        [Fact]
        public void DailyWorklogGetReportWithCorrectIncorrectTwoUsersWorklogTest()
        {
            //Arrange
            DateTime worklogDate = new DateTime(2022, 2, 18);
            DailyWorklogs dailyWorklogs = new DailyWorklogs(worklogDate);
            dailyWorklogs.Add((new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 3600), WorklogType.Correct));
            dailyWorklogs.Add((new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 7200), WorklogType.Correct));
            dailyWorklogs.Add((new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 1200), WorklogType.Incorrect));
            dailyWorklogs.Add((new WorklogModel("user_2", "user_2", 1645185355, 1645178400, 1800), WorklogType.Correct));
            dailyWorklogs.Add((new WorklogModel("user_2", "user_2", 1645185355, 1645178400, 1800), WorklogType.Incorrect));
            DailyReport dailyReportExpected = new DailyReport
            {
                Date = worklogDate,
                UserPercents = new List<UserPercent>
                {
                    new UserPercent
                    {
                        UserId = "user_1",
                        Percent = 90
                    },
                    new UserPercent
                    {
                        UserId = "user_2",
                        Percent = 50
                    }
                }
            };
            //Act
            DailyReport report = dailyWorklogs.GetReport();

            //Assert
            Assert.Equal(dailyReportExpected, report);
        }

        [Fact]
        public void AddNullWorklogModelArgumentNullExceptionTest()
        {
            //Arrange
            DateTime worklogDate = new DateTime(2022, 2, 18);
            DailyWorklogs dailyWorklogs = new DailyWorklogs(worklogDate);

            //Act / Assert
            Assert.Throws<ArgumentNullException>(() => dailyWorklogs.Add((null, WorklogType.Correct)));
        }

        [Fact]
        public void NoWorklogInformationGetReportTest()
        {
            //Arrange
            DateTime worklogDate = new DateTime(2022, 2, 18);
            DailyWorklogs dailyWorklogs = new DailyWorklogs(worklogDate);
            DailyReport expectedResult = new DailyReport
            {
                Date = worklogDate,
                UserPercents = new List<UserPercent>()
            };

            //Act
            DailyReport report = dailyWorklogs.GetReport();

            //Assert
            Assert.Equal(expectedResult, report);
        }
    }
}
