using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzDI
{
    public class MyJob : IJob, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
