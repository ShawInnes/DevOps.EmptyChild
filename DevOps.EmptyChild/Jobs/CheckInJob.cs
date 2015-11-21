using System;
using NodaTime;
using Serilog;

namespace DevOps.EmptyChild.Jobs
{
    public class CheckInJob 
    {
        private readonly IClock _clock;
        private readonly ILogger _logger;

        public CheckInJob(IClock clock, ILogger logger)
        {
            if (clock == null) throw new ArgumentNullException(nameof(clock));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _clock = clock;
            _logger = logger;
        }

        public void CheckIn()
        {
            _logger.Information("Job Running @ {now}", _clock.Now);
        }
    }
}