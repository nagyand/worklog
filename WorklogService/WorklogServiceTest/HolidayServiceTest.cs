using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorklogService.DataAccess.Holiday;
using WorklogService.Models;
using WorklogService.Services;
using Xunit;

namespace WorklogServiceTest
{
    public class HolidayServiceTest
    {
        private readonly Mock<IHolidayData> _holidayInformationMock;
        public HolidayServiceTest()
        {
            _holidayInformationMock = new Mock<IHolidayData>();
            _holidayInformationMock.Setup(s => s.GetHolidays(It.IsAny<string>()))
                       .Returns(new List<HolidayModel> {
                            new HolidayModel{UserId = "user_2", Date = new DateTime(2022,1,14) },
                            new HolidayModel{UserId = "user_2", Date = new DateTime(2022,1,13) },
                            new HolidayModel{UserId = "user_3", Date = new DateTime(2022,2,10) }
                       });
        }
        [Fact]
        public void NoHolidayInformationIsOnHolidayFalse()
        {
            //Arrange
            Mock<IHolidayData> holidayMock = new Mock<IHolidayData>();
            holidayMock.Setup(s => s.GetHolidays(It.IsAny<string>()))
                       .Returns(new List<HolidayModel>());
            HolidayService holidayService = new HolidayService(holidayMock.Object);

            //Act
            bool isOnHoliday = holidayService.IsOnHoliday("user_1", DateTime.Now);

            //Assert
            Assert.False(isOnHoliday);
        }

        [Fact]
        public void UserHasNoHolidayIsOnHolidayFalse()
        {
            //Arrange
            HolidayService holidayService = new HolidayService(_holidayInformationMock.Object);

            //Act
            bool isOnHoliday = holidayService.IsOnHoliday("user_1", DateTime.Now);

            //Assert
            Assert.False(isOnHoliday);
        }

        [Fact]
        public void UserHasHolidayIsOnHolidayFalse()
        {
            //Arrange
            HolidayService holidayService = new HolidayService(_holidayInformationMock.Object);

            //Act
            bool isOnHoliday = holidayService.IsOnHoliday("user_2", new DateTime(2022,1,10));

            //Assert
            Assert.False(isOnHoliday);
        }

        [Fact]
        public void UserHasHolidayIsOnHolidayTrue()
        {
            //Arrange
            HolidayService holidayService = new HolidayService(_holidayInformationMock.Object);

            //Act
            bool isOnHoliday = holidayService.IsOnHoliday("user_2", new DateTime(2022, 1, 14));

            //Assert
            Assert.True(isOnHoliday);
        }
    }
}
