using System;
using System.Threading;
using System.Threading.Tasks;
using CalendarApi.DataAccessLayer.cs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CalendarApi
{
    public class ApplicationStartupTask : IHostedService
    {
        private readonly IServiceProvider serviceProvider;

        public ApplicationStartupTask(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a new scope to retrieve scoped services
            using var scope = serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<CalendarDbContext>();
            await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
