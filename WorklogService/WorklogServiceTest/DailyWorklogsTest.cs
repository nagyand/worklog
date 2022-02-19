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
            WorklogModel worklog = new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 3600);
            DailyWorklogs dailyWorklogs = new DailyWorklogs(worklogDate, worklog, true);
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
            WorklogModel worklog = new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 3600);
            DailyWorklogs dailyWorklogs = new DailyWorklogs(worklogDate, worklog,true);
            dailyWorklogs.Add((new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 7200), true));
            dailyWorklogs.Add((new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 1200), false));
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
            WorklogModel worklog = new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 3600);
            DailyWorklogs dailyWorklogs = new DailyWorklogs(worklogDate,worklog,true);
            dailyWorklogs.Add((new WorklogModel("user_2", "user_2", 1645185355, 1645178400, 1800), true));
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
            WorklogModel worklog = new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 3600);
            DailyWorklogs dailyWorklogs = new DailyWorklogs(worklogDate, worklog, true);
            dailyWorklogs.Add((new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 7200), true));
            dailyWorklogs.Add((new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 1200), false));
            dailyWorklogs.Add((new WorklogModel("user_2", "user_2", 1645185355, 1645178400, 1800), true));
            dailyWorklogs.Add((new WorklogModel("user_2", "user_2", 1645185355, 1645178400, 1800), false));
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

            //Act / Assert
            Assert.Throws<ArgumentNullException>(() => new DailyWorklogs(worklogDate, null, true));
        }
    }
}
