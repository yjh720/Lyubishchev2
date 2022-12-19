using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lyubishchev.TimePeriods
{
    public class TimePeriodDto:AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        //public TimeSpan Span { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Note { get; set; }
        public TimePeriodDto()
        {
            Start = DateTime.Now;
            End = DateTime.Now.AddHours(1);
        }
    }
}
