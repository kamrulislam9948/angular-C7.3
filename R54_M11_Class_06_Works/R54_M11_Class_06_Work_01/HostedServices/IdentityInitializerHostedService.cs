namespace R54_M11_Class_06_Work_01.HostedServices
{
    public class IdentityInitializerHostedService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        public IdentityInitializerHostedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IdentityDbInitializer>();
            await seeder.SeedAsync();

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
           await Task.CompletedTask;
        }
    }
}
