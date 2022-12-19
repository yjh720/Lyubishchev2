using Lyubishchev.TimePeriodCategories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lyubishchev.TimePeriodCategories
{
    public class UpdateTimePeriodCategoryDto
    {
        [Required]
        [StringLength(TimePeriodCategoryConsts.MaxNameLength)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
