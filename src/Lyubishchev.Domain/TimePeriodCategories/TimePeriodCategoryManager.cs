using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Lyubishchev.TimePeriodCategories
{
    public class TimePeriodCategoryManager : DomainService
    {
        private readonly ITimePeriodCategoryRepository _timePeriodCategoryRepository;

        public TimePeriodCategoryManager(ITimePeriodCategoryRepository timePeriodCategoryRepository)
        {
            _timePeriodCategoryRepository = timePeriodCategoryRepository;
        }

        public async Task<TimePeriodCategory> CreateAsync(
            [NotNull] string name,
            [CanBeNull] string description = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingTimePeriodCategory = await _timePeriodCategoryRepository.FindByNameAsync(name);
            if (existingTimePeriodCategory != null)
            {
                throw new TimePeriodCategoryAlreadyExistsException(name);
            }

            return new TimePeriodCategory(
                GuidGenerator.Create(),
                name,
                description
                );
        }

        public async Task ChangeNameAsync(
            [NotNull] TimePeriodCategory timePeriodCategory,
            [NotNull] string newName)
        {
            Check.NotNull(timePeriodCategory, nameof(timePeriodCategory));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingTimePeriodCategory = await _timePeriodCategoryRepository.FindByNameAsync(newName);
            if (existingTimePeriodCategory != null && existingTimePeriodCategory.Id != timePeriodCategory.Id)
            {
                throw new TimePeriodCategoryAlreadyExistsException(newName);
            }

            timePeriodCategory.ChangeName(newName);
        }
    }
}
