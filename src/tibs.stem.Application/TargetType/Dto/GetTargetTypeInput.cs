using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;

namespace tibs.stem.TargetType.Dto
{
    public class GetTargetTypeInput
    {
        public string Filter { get; set; }
    }
    public class GetTargetTypeInput1 : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name";
            }
        }
    }
}
