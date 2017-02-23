using Quartz.Spi;
using QuartzDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
