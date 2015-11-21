using System;
using NodaTime;
using NodaTime.Testing;
using NUnit.Framework;
using Serilog;
using NSubstitute;

namespace DevOps.EmptyChild.Tests
{
    [TestFixture]
    public class CheckInfoJobShould
    {
        [Test]
        public void ThrowArgumentNullExceptionOnNullLogger()
        {
            var clock = new FakeClock(Instant.FromDateTimeOffset(DateTimeOffset.Now));

            Shouldly.Should.Throw<ArgumentNullException>(() =>
            {
                var job = new Jobs.CheckInJob(clock, null);
                job.CheckIn();
            });
        }

        [Test]
        public void ThrowArgumentNullExceptionOnNullClock()
        {
            Shouldly.Should.Throw<ArgumentNullException>(() =>
            {
                var job = new Jobs.CheckInJob(null, null);
                job.CheckIn();
            });
        }

        [Test]
        public void LogStuff()
        {
            var clock = new FakeClock(Instant.FromDateTimeOffset(DateTimeOffset.Now));
            var logger = Substitute.For<ILogger>();

            var job = new Jobs.CheckInJob(clock, logger);
            job.CheckIn();

            logger.Received().Information(Arg.Is("Job Running @ {now}"), Arg.Is(clock.Now));
        }
    }
}
