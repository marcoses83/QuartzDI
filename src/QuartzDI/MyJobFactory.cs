using Quartz.Spi;
using System;
using Quartz;

namespace QuartzDI
{
    public class MyJobFactory : IJobFactory
    {
        private readonly IServiceProvider _services;

        public MyJobFactory(IServiceProvider services)
        {
            _services = services;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_services.GetService(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            if (job != null)
            {
                ((IDisposable)job).Dispose();
            }
        }
    }
}
