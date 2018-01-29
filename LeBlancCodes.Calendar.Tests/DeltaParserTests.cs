using System;
using NUnit.Framework;

namespace LeBlancCodes.Calendar.Tests
{
    [TestFixture]
    [TestOf(typeof(DeltaParser))]
    public class DeltaParserTests
    {
        [TestCaseSource(nameof(ParserTests))]
        public void TestParser(string value, TimeSpan result)
        {
            var delta = DeltaParser.ParseDelta(value);
            Assert.AreEqual(result, delta);
        }

        private static readonly object[] ParserTests =
        {
            new object[] {"4 hours", TimeSpan.FromHours(4)},
            new object[] {"5 hours -30 minutes", TimeSpan.FromHours(5) + TimeSpan.FromMinutes(-30)},
            new object[]
            {
                "15 minutes 30 seconds 6 hours -1 day 12 hours",
                TimeSpan.FromMinutes(15) +
                TimeSpan.FromSeconds(30) +
                TimeSpan.FromHours(6) +
                TimeSpan.FromDays(-1) +
                TimeSpan.FromHours(12)
            },
            new object[] {"3m5h30s", TimeSpan.FromMinutes(3) + TimeSpan.FromHours(5) + TimeSpan.FromSeconds(30)},
        };
    }
}
