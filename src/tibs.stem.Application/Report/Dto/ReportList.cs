using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Report.Dto
{
    public class ReportList
    {
        public virtual int Id { get; set; }
        public virtual string SubjectName { get; set; }
        public virtual string ProposalNumber { get; set; }
        public virtual string ProjectRef { get; set; }
        public virtual decimal Total { get; set; }
        public virtual string SalesOrderNumber { get; set; }
        public virtual string CustomerPONumber { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public virtual int? EnquiryId { get; set; }
        public virtual string EnquiryName { get; set; }
        public virtual string EnquiryNumber { get; set; }
        public virtual string EnquiryClosureDate { get; set; }
        public virtual int? QuotationTitleId { get; set; }
        public virtual string QuotationTitleName { get; set; }
        public virtual int? CompanyId { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual string ContactName { get; set; }
        public virtual int? StatusId { get; set; }
        public virtual string StatusName { get; set; }
        public virtual int? FreightId { get; set; }
        public virtual string FreightName { get; set; }
        public virtual int? PaymentId { get; set; }
        public virtual string PaymentName { get; set; }
        public virtual int? PackingId { get; set; }
        public virtual string PackingName { get; set; }
        public virtual int? WarrantyId { get; set; }
        public virtual string WarrantyName { get; set; }
        public virtual int? ValidityId { get; set; }
        public virtual string ValidityName { get; set; }
        public virtual int? DeliveryId { get; set; }
        public virtual string DeliveryName { get; set; }
        public virtual int? CurrencyId { get; set; }
        public virtual string CurrencyName { get; set; }
        public virtual long? SalesmanId { get; set; }
        public virtual string Salesman { get; set; }
        public virtual int? ReasonId { get; set; }
        public virtual string ReasonName { get; set; }
        public virtual string CreationTime { get; set; }
        public virtual int? MileStoneId { get; set; }
        public virtual string MileStones { get; set; }
        public virtual bool Submitted { get; set; }
        public virtual DateTime? SubmittedDate { get; set; }
        public virtual bool Won { get; set; }
        public virtual DateTime? WonDate { get; set; }
        public virtual bool Lost { get; set; }
        public virtual DateTime? LostDate { get; set; }
        public virtual bool Vat { get; set; }
        public virtual decimal VatPercentage { get; set; }
        public virtual decimal VatAmount { get; set; }
    }
}
