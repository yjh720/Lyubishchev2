using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lyubishchev.TimePeriods
{
    public class CreateUpdateTimePeriodDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        //[Required]
        //public TimeSpan Span { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
        public string Note { get; set; }
        public Guid CategoryId { get; set; }
        public CreateUpdateTimePeriodDto()
        {
            Start = DateTime.Now;
            End = DateTime.Now.AddHours(1);
        }
    }
}
