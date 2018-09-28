using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;

namespace tibs.stem.Quotationss.Dto
{
    public class GetQuotationInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "SubjectName";
            }
        }
    }

    public class GetQuotationsInput
    {
        public int QuotationId { get; set; }
    }
}
