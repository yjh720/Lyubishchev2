using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lyubishchev.Domain.TimePeriods
{
    public class TimePeriod: AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        //public TimeSpan Span { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Note { get; set; }
        public Guid CategoryId { get; set; }
        //Tag like work, study, read
        //dynamiclly add Enum
        //public List<string> Tag { get; set; } = new List<string> { "management", "recruitment"};
        public TimePeriod()
        {
            Start = DateTime.Now;
            End = DateTime.Now.AddHours(1);
        }

        public TimeSpan GetTimeSpan()
        {
            return Start - End;
        }
    }
}
