using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;

namespace tibs.stem.ProductSubGroups.Dto
{
    public class GetProductSubGpInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "ProductSubGroupName";
            }
        }
    }

}
