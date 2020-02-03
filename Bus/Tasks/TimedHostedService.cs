using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoRSSExample.Bus.Commands;
using MongoRSSExample.Bus.DTOs;
using MongoRSSExample.Bus.Queries;
using MongoRSSExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MongoRSSExample.Bus.Tasks
{
    /// <summary>
    /// Any kind of approach we need. I just use this one for demo only. 
    /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio
    /// </summary>
    public class TimedHostedService : IHostedService, IDisposable
    {
        // private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer;
        private RSSService _rssService;

        public TimedHostedService(ILogger<TimedHostedService> logger, RSSService rssService)
        {
            _logger = logger;
            _rssService = rssService;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60)); // 60 goes from app settings or /a database

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            // var count = Interlocked.Increment(ref executionCount);
            
            //Create an object
            RSSFeed rss = MyRSS.Instance(_rssService);
            // Create a command for the object
            RSSCommand command = new RSSCommand(rss);
            // Invoke the command 
            var commandInvoker = new CommandInvoker(command);
            // URL goes from app settings or a database
            commandInvoker.Download("https://www.nasdaq.com/feed/rssoutbound?category=Artificial+Intelligence"); 

            _logger.LogInformation("Timed Hosted Service is working.");
            // _logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
