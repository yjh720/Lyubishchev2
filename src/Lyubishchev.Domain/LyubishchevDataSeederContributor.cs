using Lyubishchev.Domain.TimePeriods;
using Lyubishchev.TimePeriodCategories;
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
        private readonly ITimePeriodCategoryRepository _timePeriodCategoryRepository;
        private readonly TimePeriodCategoryManager _timePeriodCategoryManager;
        public LyubishchevDataSeederContributor(
            IRepository<TimePeriod, Guid> timePeriodRepository,
            ITimePeriodCategoryRepository timePeriodCategoryRepository,
            TimePeriodCategoryManager timePeriodCategoryManager)
        {
            _timePeriodRepository = timePeriodRepository;
            _timePeriodCategoryRepository = timePeriodCategoryRepository;
            _timePeriodCategoryManager = timePeriodCategoryManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {

            if(await _timePeriodRepository.GetCountAsync() > 0)
            {
                return;
            }

            //ADD SEED DATA FOR TIMEPERIODCATEGORY

            var life = await _timePeriodCategoryRepository.InsertAsync(
                                await _timePeriodCategoryManager.CreateAsync(
                                    "生活日常",
                                    "生活日常")
                                );

            var hire = await _timePeriodCategoryRepository.InsertAsync(
                await _timePeriodCategoryManager.CreateAsync(
                    "招聘",
                    "招聘")
                );


            //ADD SEED DATA FOR TIMEPERIOD

            await _timePeriodRepository.InsertAsync(
                new TimePeriod()
                {
                    Name = "睡觉",
                    End = DateTime.Now + TimeSpan.FromHours(1),
                    Note = "睡觉",
                    CategoryId = life.Id
                },
                autoSave: true
                ) ;

            await _timePeriodRepository.InsertAsync(
                new TimePeriod()
                {
                    Name = "吃饭",
                    End = DateTime.Now + TimeSpan.FromHours(1),
                    Note = "吃饭",
                    CategoryId= life.Id
                },
                autoSave: true
                );

        }
    }
}
