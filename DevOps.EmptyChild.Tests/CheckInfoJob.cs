using System;
using NodaTime;
using NodaTime.Testing;
using NUnit.Framework;
using Serilog;

namespace DevOps.EmptyChild.Tests
{
    [TestFixture]
    public class CheckInfoJobShould
    {
        [Test]
        public void ThrowArgumentNullExceptionOnNullLogger()
        {
            IClock clock = new FakeClock(Instant.FromDateTimeOffset(DateTimeOffset.Now));
            ILogger logger = null;

            Shouldly.Should.Throw<ArgumentNullException>(() =>
            {
                var job = new Jobs.CheckInJob(clock, logger);
                job.CheckIn();
            });
        }

        [Test]
        public void ThrowArgumentNullExceptionOnNullClock()
        {
            IClock clock = null;
            ILogger logger = null;

            Shouldly.Should.Throw<ArgumentNullException>(() =>
            {
                var job = new Jobs.CheckInJob(clock, logger);
                job.CheckIn();
            });
        }
    }
}
