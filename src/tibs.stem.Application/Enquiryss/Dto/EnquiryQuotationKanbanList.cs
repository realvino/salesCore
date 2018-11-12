using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Enquiryss.Dto
{

    public class EnquiryQuotationKanbanListOld
    {
        public virtual int Id { get; set; }
        public virtual string EnquiryNo { get; set; }
        public virtual string Title { get; set; }
        public virtual int MileStoneId { get; set; }
        public virtual string MileStoneName { get; set; }
        public virtual int? MileStoneStatusId { get; set; }
        public virtual string MileStoneStatusName { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual string ContactName { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public QuotationKanbanList[] QuotationKanban { get; set; }

    }
    public class EnquiryQuotationKanbanList
    {
        public virtual int Id { get; set; }
        public virtual string EnquiryNo { get; set; }
        public virtual string Title { get; set; }
        public virtual int MileStoneId { get; set; }
        public virtual string MileStoneName { get; set; }
        public virtual int? MileStoneStatusId { get; set; }
        public virtual string MileStoneStatusName { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual string ContactName { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual bool IsQuotation { get; set; }
        public virtual int QuotationId { get; set; }
        public virtual string SubjectName { get; set; }
        public virtual DateTime? CloseDate { get; set; }
        public virtual decimal Total { get; set; }
        public virtual int QuotationCount { get; set; }
        public virtual string EnqQuotation { get; set; }
        public string QRefno { get; set; }
        public string Creator { get; set; }
        public string CreatorImg { get; set; }
        public string Salesperson { get; set; }
        public string SalespersonImg { get; set; }
        public string Remarks { get; set; }
    }

    public class QuotationKanbanList
    {
        public virtual int Id { get; set; }
        public virtual string SubjectName { get; set; }
        public virtual string ProposalNumber { get; set; }
        public virtual int? EnquiryId { get; set; }
        public virtual string EnquiryTitle { get; set; }
        public virtual int? QuotationTitleId { get; set; }
        public virtual string QuotationTitleName { get; set; }
        public virtual int? CompanyId { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual int? StatusId { get; set; }
        public virtual string StatusName { get; set; }
        public int TenantId { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual string ContactName { get; set; }
        public virtual int? MileStoneId { get; set; }

    }
    public class EnquiryQuotationKanbanArray
    {
        public bool Flag { get; set; }
        public string MilestoneName { get; set; }
        public decimal Total { get; set; }
        public EnquiryQuotationKanbanList[] EnquiryQuotationKanban { get; set; }
        public bool EndQuotation { get; internal set; }
    }
    public class EnquiryQuotationKanbanArrayOld
    {
        public bool Flag { get; set; }
        public string MilestoneName { get; set; }
        public EnquiryQuotationKanbanListOld[] EnquiryQuotationKanban { get; set; }
    }
}
