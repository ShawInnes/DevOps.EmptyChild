using DevOps.EmptyChild.Jobs;
using Hangfire;

namespace DevOps.EmptyChild.Services
{
    public class EmptyChildService
    {
        public void Start()
        {
            RecurringJob.AddOrUpdate<CheckInJob>(p => p.CheckIn(), Cron.Minutely);
        }

        public void Stop()
        {

        }
    }
}