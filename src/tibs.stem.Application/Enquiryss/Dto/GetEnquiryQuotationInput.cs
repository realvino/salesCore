using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Enquiryss.Dto
{
    public class GetEnquiryQuotationInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public int EnquiryId { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "SubjectName";
            }
        }
    }
}
