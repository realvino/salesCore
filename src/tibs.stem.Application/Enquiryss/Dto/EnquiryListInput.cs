using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;

namespace tibs.stem.Enquiryss.Dto
{
    public class EnquiryListInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "EnquiryNo";
            }
        }
    }
}
