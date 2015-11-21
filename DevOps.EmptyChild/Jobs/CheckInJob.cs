using System;
using DevOps.EmptyChild.Models;
using NodaTime;
using RestSharp;
using Serilog;

namespace DevOps.EmptyChild.Jobs
{
    public class CheckInJob
    {
        private readonly IClock _clock;
        private readonly ILogger _logger;
        private readonly IRestClient _client;

        public CheckInJob(IClock clock, ILogger logger, IRestClient client)
        {
            if (clock == null) throw new ArgumentNullException(nameof(clock));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (client == null) throw new ArgumentNullException(nameof(client));

            _clock = clock;
            _logger = logger;
            _client = client;
        }

        public void CheckIn()
        {
            _logger.Information("Sending Payload @ {now}", _clock.Now);

            var request = new RestRequest("/hello");
            var payload = new Payload
            {
                HostName = "hostname",
                Addresses = new[] { "127.0.0.1", "10.0.1.94" },
                Metadata = "cookbook-developer"
            };

            request.AddJsonBody(payload);
            _client.Post<Payload>(request);
        }
    }
}