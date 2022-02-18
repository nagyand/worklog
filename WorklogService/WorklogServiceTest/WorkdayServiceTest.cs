using Moq;
using System;
using System.Collections.Generic;
using WorklogService.DataAccess.WorkdayException;
using WorklogService.Models;
using WorklogService.Services.Workday;
using Xunit;

namespace WorklogServiceTest
{
    public class WorkdayServiceTest
    {
        private readonly Mock<IWorkdayExceptionData> _workdayExceptionMock;
        public WorkdayServiceTest()
        {
            _workdayExceptionMock = new Mock<IWorkdayExceptionData>();
            _workdayExceptionMock.Setup(s => s.GetWorkdayExceptions()).Returns(new List<WorkdayExceptionModel>
            {
                new WorkdayExceptionModel
                {
                    Date = new DateTime(2022,2,19),
                    IsWork = true
                },
                new WorkdayExceptionModel
                {
                    Date = new DateTime(2022,2,21),
                    IsWork = false
                }
            });
        }
        [Fact]
        public void TrueOnNormalWorkday()
        {
            //Arrange

            WorkdayService workdayService = new WorkdayService(_workdayExceptionMock.Object);

            //Act
            bool result = workdayService.IsWorkday(new DateTime(2022, 2, 15));

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("2022-01-15")]
        [InlineData("2022-02-20")]
        public void FalseOnWeekEnd(string testDate)
        {
            //Arrange
            WorkdayService workdayService = new WorkdayService(_workdayExceptionMock.Object);
            DateTime date = DateTime.Parse(testDate);
            //Act
            bool result = workdayService.IsWorkday(date);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void TrueOnWorkingSaturday()
        {
            //Arrange
            WorkdayService workdayService = new WorkdayService(_workdayExceptionMock.Object);
            //Act
            bool result = workdayService.IsWorkday(new DateTime(2022, 2, 19));

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void FalseOnHolidayMonday()
        {
            //Arrange
            WorkdayService workdayService = new WorkdayService(_workdayExceptionMock.Object);
            //Act
            bool result = workdayService.IsWorkday(new DateTime(2022, 2, 21));

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GetPreviousWorkdayMondayWeekend()
        {
            //Arrange
            WorkdayService workdayService = new WorkdayService(_workdayExceptionMock.Object);

            //Act
            DateTime result = workdayService.GetPreviousWorkday(new DateTime(2022, 1, 24));

            //Assert
            Assert.Equal(new DateTime(2022,1,21).Date, result.Date);
        }

        [Fact]
        public void GetPreviousWorkdayMondayShortWeekend()
        {
            //Arrange
            Mock<IWorkdayExceptionData> workdayException = new Mock<IWorkdayExceptionData>();
            workdayException.Setup(s => s.GetWorkdayExceptions())
                            .Returns(new List<WorkdayExceptionModel> { new WorkdayExceptionModel {Date = new DateTime(2022,2,12), IsWork = true } });
            WorkdayService workdayService = new WorkdayService(workdayException.Object);

            //Act
            DateTime result = workdayService.GetPreviousWorkday(new DateTime(2022, 2, 14));

            //Assert
            Assert.Equal(new DateTime(2022, 2, 12).Date, result.Date);
        }

        [Fact]
        public void GetPreviousWorkdayLongWeekend()
        {
            //Arrange
            Mock<IWorkdayExceptionData> workdayException = new Mock<IWorkdayExceptionData>();
            workdayException.Setup(s => s.GetWorkdayExceptions())
                            .Returns(new List<WorkdayExceptionModel> { new WorkdayExceptionModel { Date = new DateTime(2022, 2, 14), IsWork = false }, new WorkdayExceptionModel { Date = new DateTime(2022, 2, 15), IsWork = false } });
            WorkdayService workdayService = new WorkdayService(workdayException.Object);

            //Act
            DateTime result = workdayService.GetPreviousWorkday(new DateTime(2022, 2, 16));

            //Assert
            Assert.Equal(new DateTime(2022, 2, 11).Date, result.Date);
        }

        [Fact]
        public void GetPreviousWorkdayNormalWeekday()
        {
            //Arrange
            WorkdayService workdayService = new WorkdayService(_workdayExceptionMock.Object);

            //Act
            DateTime result = workdayService.GetPreviousWorkday(new DateTime(2022, 2, 16));

            //Assert
            Assert.Equal(new DateTime(2022, 2, 15).Date, result.Date);
        }
    }
}