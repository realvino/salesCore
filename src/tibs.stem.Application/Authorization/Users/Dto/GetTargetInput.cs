using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;

namespace tibs.stem.Authorization.Users.Dto
{
    public class GetTargetInput
    {
        public string Filter { get; set; }
        public int? UserId { get; set; }
    }
    public class GetTargetInput1 : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public int? UserId { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "UserName";
            }
        }
    }
}
