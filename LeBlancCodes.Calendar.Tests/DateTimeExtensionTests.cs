using System;
using NUnit.Framework;

namespace LeBlancCodes.Calendar.Tests
{
    [TestFixture]
    [TestOf(typeof(DateTimeExtensions))]
    public class DateTimeExtensionTests
    {
        [Test]
        public void TestIsWeekday()
        {
            var factory = new DateTimeFactory();
            var dto = factory.CreateDateTimeOffset(2017, 12, 31);

            Assert.AreEqual(dto.DayOfWeek, DayOfWeek.Sunday);
            Assert.False(dto.DayOfWeek.IsWeekday());
            Assert.False(dto.IsWeekday());

            dto += TimeSpan.FromDays(1);
            Assert.AreEqual(dto.DayOfWeek, DayOfWeek.Monday);
            Assert.True(dto.DayOfWeek.IsWeekday());
            Assert.True(dto.IsWeekday());

            dto += TimeSpan.FromDays(1);
            Assert.AreEqual(dto.DayOfWeek, DayOfWeek.Tuesday);
            Assert.True(dto.DayOfWeek.IsWeekday());
            Assert.True(dto.IsWeekday());

            dto += TimeSpan.FromDays(1);
            Assert.AreEqual(dto.DayOfWeek, DayOfWeek.Wednesday);
            Assert.True(dto.DayOfWeek.IsWeekday());
            Assert.True(dto.IsWeekday());

            dto += TimeSpan.FromDays(1);
            Assert.AreEqual(dto.DayOfWeek, DayOfWeek.Thursday);
            Assert.True(dto.DayOfWeek.IsWeekday());
            Assert.True(dto.IsWeekday());

            dto += TimeSpan.FromDays(1);
            Assert.AreEqual(dto.DayOfWeek, DayOfWeek.Friday);
            Assert.True(dto.DayOfWeek.IsWeekday());
            Assert.True(dto.IsWeekday());

            dto += TimeSpan.FromDays(1);
            Assert.AreEqual(dto.DayOfWeek, DayOfWeek.Saturday);
            Assert.False(dto.DayOfWeek.IsWeekday());
            Assert.False(dto.IsWeekday());
        }

        [Test]
        public void TestSet()
        {
            var factory = new DateTimeFactory();
            var dto = factory.CreateDateTimeOffset(2017, 04, 12, 8, 9, 10, 130);

            Assert.AreEqual(dto.Set(12), factory.CreateDateTimeOffset(2017, 04, 12, 12));
            Assert.AreEqual(dto.Set(minute: 12), factory.CreateDateTimeOffset(2017, 04, 12, 0, 12));
            Assert.AreEqual(dto.Set(second: 12), factory.CreateDateTimeOffset(2017, 04, 12, 0, 0, 12));
        }

        [Test]
        public void TestSetTimeZone()
        {
            var factory = new DateTimeFactory();
            var dto = factory.CreateDateTimeOffset(2017, 03, 03);
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var offset = timeZone.BaseUtcOffset - factory.TimeZone.BaseUtcOffset;
            var newDto = dto.SetTimeZone(timeZone);
            Assert.AreEqual(dto.DateTime, newDto.DateTime);
            Assert.AreEqual(dto - newDto, offset);
        }
    }
}
