using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace QuartzDI
{
    public static class QuartzExtensions
    {
        public static async void UseQuartz(this IApplicationBuilder app)
        {
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            var jobFactory = app.ApplicationServices.GetService(typeof(IJobFactory));

            scheduler.JobFactory = (IJobFactory)jobFactory;
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<IJob>()
                .WithIdentity("myJob", "group1") // name "myJob", group "group1"
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);
        }

        public static void AddQuartz(this IServiceCollection services)
        {
            services.AddScoped<IJobFactory, MyJobFactory>();
            services.AddTransient<IJob, MyJob>();
        }
    }
}
