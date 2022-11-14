using AutoFixture.Xunit2;
using Moq;
using PublicHolidaysAssignment;
using PublicHolidaysAssignment.EnricoApi;
using PublicHolidaysAssignment.HelperMethods;
using PublicHolidaysAssignment.ModelDtos;
using PublicHolidaysAssignment.Models;
using PublicHolidaysAssignment.PublicHolidayServices;
using PublicHolidaysAssignment.Repository;
using System.Diagnostics.Metrics;

namespace PublicHolidaysTestUnits
{
    public class UnitTest1
    {
        [Theory, AutoData]
        public void TestChechDayStatusMethodWhenDatabaseQueryReturnsObject(ResponseDto<string> autoResponse, string country, string year,string region, DateTime date, DayStatus newDay)
        {
            var mockRep = new Mock<ICountryHolidayRepository>();
            var mockEnrico = new Mock<IEnricoApiService>();
            var mockCounter = new Mock<IConsecutiveCounter>();
            var mockJson = new Mock<IJsonDeserializer>();
            var mockDb = new Mock<HolidayDbContext>();
            var sut = new PublicHolidayService(mockRep.Object, mockEnrico.Object, mockDb.Object, mockCounter.Object, mockJson.Object);
            mockRep.Setup(x => x.QueryDayRecord(country, date)).Returns(newDay);
            mockEnrico.Setup(x => x.SpecificDayStatus(date, country)).Returns(autoResponse);
            var test = sut.CheckDayStatus(date,country);
            Assert.Equal(newDay.TypeOfDay, test.Message);
        }
        [Theory, AutoData]
        public void TestCheckDayStatusMethodWhenDatabaseQueryReturnsNull(ResponseDto<string> autoResponse, string country, string year, string region, DateTime date, DayStatus newDay)
        {
            var mockRep = new Mock<ICountryHolidayRepository>();
            var mockEnrico = new Mock<IEnricoApiService>();
            var mockCounter = new Mock<IConsecutiveCounter>();
            var mockJson = new Mock<IJsonDeserializer>();
            var mockDb = new Mock<HolidayDbContext>();
            var sut = new PublicHolidayService(mockRep.Object, mockEnrico.Object, mockDb.Object, mockCounter.Object, mockJson.Object);
            mockRep.Setup(x => x.QueryDayRecord(country, date)).Returns((DayStatus)null);
            mockEnrico.Setup(x => x.SpecificDayStatus(date, country)).Returns(autoResponse);
            var test = sut.CheckDayStatus(date, country);
            Assert.Equal(autoResponse.Message, test.Message);
        }
        [Theory, AutoData]
        public void TestCheckDayStatusMethodWhenDatabaseQueryReturnsNulls(ResponseDto<string> autoResponse, string country, string year, string region, DateTime date, DayStatus newDay)
        {
            var mockRep = new Mock<ICountryHolidayRepository>();
            var mockEnrico = new Mock<IEnricoApiService>();
            var mockCounter = new Mock<IConsecutiveCounter>();
            var mockJson = new Mock<IJsonDeserializer>();
            var mockDb = new Mock<HolidayDbContext>();
            var sut = new PublicHolidayService(mockRep.Object, mockEnrico.Object, mockDb.Object, mockCounter.Object, mockJson.Object);
            mockRep.Setup(x => x.QueryDayRecord(country, date)).Returns((DayStatus)null);
            mockEnrico.Setup(x => x.SpecificDayStatus(date, country)).Returns(autoResponse);
            var test = sut.CheckDayStatus(date, country);
            Assert.Equal(autoResponse.Message, test.Message);
        }
    }
}