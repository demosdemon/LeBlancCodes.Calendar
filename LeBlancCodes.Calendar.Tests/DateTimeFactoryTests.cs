using System;
using NUnit;
using NUnit.Framework;

namespace LeBlancCodes.Calendar.Tests
{
    [TestFixture]
    [TestOf(typeof(DateTimeFactory))]
    public class DateTimeFactoryTests
    {
        private const string Format = "yyyy-MM-ddTHH:mm:sszzz";
        private static TimeZoneInfo TimeZone => TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

        [Test]
        public void TestInitialization()
        {
            Assert.That(() => new DateTimeFactory(), Is.Not.Null);
        }

        [Test]
        public void TestCreateOverflow()
        {
            var factory = new DateTimeFactory {TimeZone = TimeZone};
            var result = factory.CreateDateTimeOffset(2012, 12, 30, 24, 90, 100);
            Assert.AreEqual("2012-12-31T01:31:40-06:00", result.ToString(Format));
            result = factory.CreateDateTimeOffset(2013, 1, 1, 0, 0, -30);
            Assert.AreEqual("2012-12-31T23:59:30-06:00", result.ToString(Format));
            result = factory.CreateDateTimeOffset(2018, 1, 24, 36, 80, -1000000);
            Assert.AreEqual("2018-01-13T23:33:20-06:00", result.ToString(Format));
        }
    }
}
