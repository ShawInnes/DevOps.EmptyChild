using System;
using Hangfire;
using Serilog;

namespace DevOps.EmptyChild.Services
{
    internal class EmptyChildService
    {
        public void Start()
        {
            Log.Information("Start()");

            RecurringJob.AddOrUpdate(() => Log.Information("Hi, This is a Job"), Cron.Minutely);
        }

        public void AfterStart()
        {
            Log.Information("AfterStart()");
        }

        public void Stop()
        {
            Log.Information("Stop()");
        }
    }
}