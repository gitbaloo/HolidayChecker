using NUnit.Framework;
using System;

namespace HolidayCalendar.Tests;

public class HolidayTests
{
    private HolidayCalendar fixture;

    [SetUp]
    public void Setup()
    {
        var httpClient = new HttpClient();
        fixture = new HolidayCalendar(httpClient);
    }
    [Test]
    public async Task GIVEN_XmasDay_WHEN_IsHoliday_THEN_return_true()
    {
        // Arrange
        var date = new DateTime(2023, 12, 25);

        // Act
        var result = await fixture.IsHoliday(date);

        // Assert
        Assert.IsTrue(result);
    }
    
    [Test]
    public async Task GIVEN_regular_weekday_WHEN_IsHoliday_THEN_return_false()
    {
        // Arrange
        var date = new DateTime(2023, 4, 21);

        // Act
        var result = await fixture.IsHoliday(date);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task GIVEN_April2023_WHEN_GetHolidays_THEN_return_EasterDays()
    {
        // Arrange
        var startDate = new DateTime(2023, 4, 1);
        var endDate = new DateTime(2023, 4, 30);

        // Act
        var allHolidays = await fixture.GetHolidays(startDate, endDate);
        var nationalHolidays = allHolidays.Where(holiday => holiday.NationalHoliday).ToList();

        // Assert
        // Assert.IsFalse(result.Any(holiday => holiday.Date == (new DateTime(2023, 4, 2)) && holiday.NationalHoliday)); // Palm Sunday
        Assert.IsTrue(nationalHolidays.Any(holiday => holiday.Date == (new DateTime(2023, 4, 6)) && holiday.NationalHoliday)); // Maundy Thursday
        Assert.IsTrue(nationalHolidays.Any(holiday => holiday.Date == (new DateTime(2023, 4, 7)) && holiday.NationalHoliday)); // Good Friday
        Assert.IsTrue(nationalHolidays.Any(holiday => holiday.Date == (new DateTime(2023, 4, 9)) && holiday.NationalHoliday)); // Easter Sunday
        Assert.IsTrue(nationalHolidays.Any(holiday => holiday.Date == (new DateTime(2023, 4, 10)) && holiday.NationalHoliday)); // Easter Monday
        Assert.AreEqual(4, nationalHolidays.Count);
    }

    [Test]
    public async Task IsXmasEveANationalHoliday()
    {
        var date = new DateTime(2023, 12, 24);

        // Act
        var result = await fixture.IsNationalHoliday(date);

        // Assert
        Assert.IsFalse(result);
    }
}
