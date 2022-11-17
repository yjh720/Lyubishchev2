using Lyubishchev.Domain.TimePeriods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Lyubishchev
{
    public class LyubishchevDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<TimePeriod, Guid> _timePeriodRepository;
        public LyubishchevDataSeederContributor(IRepository<TimePeriod, Guid> timePeriodRepository)
        {
            _timePeriodRepository = timePeriodRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _timePeriodRepository.GetCountAsync() > 0)
            {
                return;
            }

            await _timePeriodRepository.InsertAsync(
                new TimePeriod()
                {
                    Name = "睡觉",
                    End = DateTime.Now + TimeSpan.FromHours(1),
                    Note = "睡觉"
                },
                autoSave: true
                ); ;
        }
    }
}
