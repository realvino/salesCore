using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Report.Dto
{
    public class GetFilterReportInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public virtual int ReportTypeId { get; set; }
        public virtual int ReportViewId { get; set; }
        public virtual string Salesperson { get; set; }
        public virtual string Creator { get; set; }
        public virtual string Country { get; set; }
        public virtual string CustomerType { get; set; }
        public virtual string Currency { get; set; }
        public virtual string MileStone { get; set; }
        public virtual string MileStoneStatus { get; set; }
        public virtual string QuotationStatus { get; set; }
        public virtual string EnquiryCreationTime { get; set; }
        public virtual int EnquiryCreationTimeId { get; set; }
        public virtual string QuotationCreationTime { get; set; }
        public virtual int QuotationCreationTimeId { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual int SubmittedDateId { get; set; }
        public virtual string WonDate { get; set; }
        public virtual int WonDateId { get; set; }
        public virtual string LostDate { get; set; }
        public virtual int LostDateId { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "";
            }
        }

    }
}
