using System;
using System.Collections.Generic;
using WorklogService.Models.Reports;
using WorklogService.Models.Worklogs;
using Xunit;

namespace WorklogServiceTest
{
    public class WeeklyWorklogsTest
    {
        private readonly DateTime start;
        private readonly WeeklyWorklogs _weeklyWorklogs;
        public WeeklyWorklogsTest()
        {
            start = new DateTime(2022, 2, 14);
            _weeklyWorklogs = new WeeklyWorklogs(start, new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 43200));
        }

        [Fact]
        public void AddNullWorklogToWeeklyWorklogTest()
        {
            //Act / Assert
            Assert.Throws<ArgumentNullException>(() => _weeklyWorklogs.Add(null));
        }
        [Fact]
        public void OneWorklogWeekylWorklogsTest()
        {
            //Arrange
            WeeklyReport expected = new WeeklyReport
            {
                Start = start,
                UserPercents = new List<UserPercent>
                {
                    new UserPercent
                    {
                        UserId = "user_1",
                        Percent = 37.5
                    }
                }
            };

            //Act
            WeeklyReport report = _weeklyWorklogs.GetReport();

            //Assert
            Assert.Equal(expected,report);
        }

        [Fact]
        public void OneWorklogMoreThan100WeekylWorklogsTest()
        {
            //Arrange
            WeeklyWorklogs weeklyWorklogs = new WeeklyWorklogs(start, new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 126720));
            WeeklyReport expected = new WeeklyReport
            {
                Start = start,
                UserPercents = new List<UserPercent>
                {
                    new UserPercent
                    {
                        UserId = "user_1",
                        Percent = 110
                    }
                }
            };

            //Act
            WeeklyReport report = weeklyWorklogs.GetReport();

            //Assert
            Assert.Equal(expected, report);
        }

        [Fact]
        public void ThreeWorklogWeekylWorklogsTest()
        {
            //Arrange
            _weeklyWorklogs.Add(new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 54000));
            _weeklyWorklogs.Add(new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 3600));
            WeeklyReport expected = new WeeklyReport
            {
                Start = start,
                UserPercents = new List<UserPercent>
                {
                    new UserPercent
                    {
                        UserId = "user_1",
                        Percent = 87.5
                    }
                }
            };

            //Act
            WeeklyReport report = _weeklyWorklogs.GetReport();

            //Assert
            Assert.Equal(expected, report);
        }

        [Fact]
        public void OneForUserTwoThreeWorklogForUserOneWeekylWorklogsTest()
        {
            //Arrange
            _weeklyWorklogs.Add(new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 54000));
            _weeklyWorklogs.Add(new WorklogModel("user_1", "user_1", 1645185355, 1645178400, 3600));
            _weeklyWorklogs.Add(new WorklogModel("user_2", "user_2", 1645185355, 1645178400, 54000));
            WeeklyReport expected = new WeeklyReport
            {
                Start = start,
                UserPercents = new List<UserPercent>
                {
                    new UserPercent
                    {
                        UserId = "user_1",
                        Percent = 87.5
                    },
                    new UserPercent
                    {
                        UserId = "user_2",
                        Percent = 46.88
                    }
                }
            };

            //Act
            WeeklyReport report = _weeklyWorklogs.GetReport();

            //Assert
            Assert.Equal(expected, report);
        }
    }
}
