using System;
using DevOps.EmptyChild.Models;
using NodaTime;
using NodaTime.Testing;
using NUnit.Framework;
using Serilog;
using NSubstitute;
using RestSharp;

namespace DevOps.EmptyChild.Tests
{
    [TestFixture]
    public class CheckInfoJobShould
    {
        [Test]
        public void ThrowArgumentNullException()
        {
            Shouldly.Should.Throw<ArgumentNullException>(() =>
            {
                var job = new Jobs.CheckInJob(null, null, null);
                job.CheckIn();
            });
        }

        [Test]
        public void LogCorrectValue()
        {
            var clock = new FakeClock(Instant.FromDateTimeOffset(DateTimeOffset.Now));
            var logger = Substitute.For<ILogger>();
            var client = Substitute.For<IRestClient>();

            var job = new Jobs.CheckInJob(clock, logger, client);
            job.CheckIn();

            logger.Received().Information(Arg.Is("Sending Payload @ {now}"), Arg.Is(clock.Now));
        }

        [Test]
        public void PostRestPayload()
        {
            var clock = new FakeClock(Instant.FromDateTimeOffset(DateTimeOffset.Now));
            var logger = Substitute.For<ILogger>();
            var client = Substitute.For<IRestClient>();
            client.BaseUrl = new Uri("http://localhost");

            var job = new Jobs.CheckInJob(clock, logger, client);
            job.CheckIn();

            client.ReceivedWithAnyArgs().Post<Payload>(new RestRequest("/hello"));
        }
    }
}
