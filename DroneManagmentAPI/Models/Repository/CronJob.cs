
using NCrontab;

namespace DroneManagmentAPI.Models.Repository
{
    public class CronJob : Microsoft.Extensions.Hosting.BackgroundService, IHostedService
    {
        private readonly IServiceScopeFactory _IserviceScopeFactory;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;

        private string Schedule => "0 0 11 * * *";  //Runs every day at 10am
        //private string Schedule => "0 * * * * *";  //Runs every min
        private DroneRepository _droneRepository;

        public CronJob(IServiceScopeFactory serviceScopeFactory)
        {
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            _IserviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                if (DateTime.Now > _nextRun)
                {
                    Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }
        private void Process()
        {
            try
            {
                //logic here
                using (var scope = _IserviceScopeFactory.CreateScope())
                {
                    _droneRepository = scope.ServiceProvider.GetService<DroneRepository>();

                    var HolLst = _droneRepository.CheckDroneBattery();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
