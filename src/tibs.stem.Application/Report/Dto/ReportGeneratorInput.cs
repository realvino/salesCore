using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Reports;

namespace tibs.stem.Report.Dto
{
    [AutoMap(typeof(ReportGenerator))]
    public class ReportGeneratorInput
    {
        public virtual int Id { get; set; }
        public int TenantId { get; set; }
        public virtual string Name { get; set; }
        public virtual int ReportTypeId { get; set; }
        public virtual string MileStone { get; set; }
        public virtual string MileStoneStatus { get; set; }
        public virtual string Salesperson { get; set; }
        public virtual string Creator { get; set; }
        public virtual string Country { get; set; }
        public virtual string Currency { get; set; }
        public virtual string CustomerType { get; set; }
        public virtual string QuotationStatus { get; set; }
        public virtual DateTime EnquiryCreationTime { get; set; }
        public virtual int EnquiryCreationTypeId { get; set; }
        public virtual DateTime QuotationCreationTime { get; set; }
        public virtual int QuotationCreationTypeId { get; set; }
        public virtual DateTime SubmitttedDate { get; set; }
        public virtual int SubmitttedDateId { get; set; }
        public virtual DateTime WonDate { get; set; }
        public virtual int WonDateId { get; set; }
        public virtual DateTime LostDate { get; set; }
        public virtual int LostDateId { get; set; }
        public virtual string HiddenColumns { get; set; }
    }
}
