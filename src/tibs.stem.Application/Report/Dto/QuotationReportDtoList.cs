using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Report.Dto
{
    public class QuotationReportDtoList
    {
        public virtual int QuotationId { get; set; }
        public virtual string SubjectName { get; set; }
        public virtual string QRefno { get; set; }
        public virtual decimal Total { get; set; }
        public virtual int EnquiryId { get; set; }
        public virtual string EnquiryTitle { get; set; }
        public virtual string EnquiryNo { get; set; }
        public virtual string Remarks { get; set; }
        public virtual DateTime EnquiryClosureDate { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string ContactName { get; set; }
        public virtual string StatusName { get; set; }
        public virtual string Salesperson { get; set; }
        public virtual string Creator { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual string MileStones { get; set; }
        public virtual string MileStoneStatus { get; set; }
        public virtual DateTime SubmittedDate { get; set; }
        public virtual DateTime WonDate { get; set; }
        public virtual DateTime LostDate { get; set; }
    }
}
