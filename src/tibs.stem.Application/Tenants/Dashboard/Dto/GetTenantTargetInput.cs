using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Tenants.Dashboard.Dto
{
    public class GetTenantTargetInput
    {
        public string Filter { get; set; }
    }
    public class GetTenantTargetInput1 : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Value";
            }
        }
    }
}
