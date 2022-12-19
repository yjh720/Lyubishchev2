using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lyubishchev.TimePeriodCategories
{
    public class TimePeriodCategory : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; set; }
        private TimePeriodCategory()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal TimePeriodCategory(
            Guid id,
           [NotNull] string name,
           [CanBeNull] string description = null) : base(id)
        {
            SetName(name);
            Description = description;
        }

        internal TimePeriodCategory ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name,
                nameof(name),
                maxLength:TimePeriodCategoryConsts.MaxNameLength
                );
        }
    }
}
