using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Lyubishchev.TimePeriodCategories
{
    public class TimePeriodCategoryAlreadyExistsException : BusinessException
    {
        public TimePeriodCategoryAlreadyExistsException(string name)
            : base(LyubishchevDomainErrorCodes.TimePeriodCategoryAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
