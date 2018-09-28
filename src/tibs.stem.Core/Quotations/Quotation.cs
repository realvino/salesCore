using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using tibs.stem.Authorization.Users;
using tibs.stem.Currencies;
using tibs.stem.CustomerCompanys;
using tibs.stem.Deliverys;
using tibs.stem.Enquirys;
using tibs.stem.Freights;
using tibs.stem.Packings;
using tibs.stem.QPayments;
using tibs.stem.QuotationStatuss;
using tibs.stem.Reasonss;
using tibs.stem.TitleOfQuotations;
using tibs.stem.Validitys;
using tibs.stem.Warrantys;
using tibs.stem.QPayments;
using tibs.stem.MileStones;

namespace tibs.stem.Quotations
{
    [Table("Quotation")]
    public class Quotation : FullAuditedEntity, IMustHaveTenant
    {
        [ForeignKey("EnquiryId")]
        public virtual Enquiry Enquirys { get; set; }
        public virtual int? EnquiryId { get; set; }

        [ForeignKey("QuotationTitleId")]
        public virtual TitleOfQuotation QuotationTitle { get; set; }
        public virtual int? QuotationTitleId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Companys { get; set; }
        public virtual int? CompanyId { get; set; }

        [ForeignKey("CompetitorId")]
        public virtual Company Competitor { get; set; }
        public virtual int? CompetitorId { get; set; }

        [ForeignKey("ContactId")]
        public virtual Contact Contacts { get; set; }
        public virtual int? ContactId { get; set; }

        [ForeignKey("StatusId")] 
        public virtual QuotationStatus Status { get; set; }
        public virtual int? StatusId { get; set; }

        [ForeignKey("FreightId")]
        public virtual Freight Freights { get; set; }
        public virtual int? FreightId { get; set; }

        [ForeignKey("PaymentId")]
        public virtual QPayment Payment { get; set; }
        public virtual int? PaymentId { get; set; }

        [ForeignKey("PackingId")]
        public virtual Packing Packings { get; set; }
        public virtual int? PackingId { get; set; }

        [ForeignKey("WarrantyId")]
        public virtual Warranty Warrantys { get; set; }
        public virtual int? WarrantyId { get; set; }

        [ForeignKey("ValidityId")]
        public virtual Validity Validitys { get; set; }
        public virtual int? ValidityId { get; set; }

        [ForeignKey("DeliveryId")]
        public virtual Delivery Deliverys { get; set; }
        public virtual int? DeliveryId { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual Currency Currencys { get; set; }
        public virtual int? CurrencyId { get; set; }

        [ForeignKey("SalesmanId")]
        public virtual User Salesman { get; set; }
        public virtual long? SalesmanId { get; set; }

        [ForeignKey("ReasonId")]
        public virtual Reason Reasons { get; set; }
        public virtual int? ReasonId { get; set; }

        public virtual DateTime? Date { get; set; }
        public virtual string SubjectName { get; set; }
        public virtual string ProposalNumber {get; set;}
        public virtual string ProjectRef {get; set;}
        public virtual DateTime? ClosureDate { get; set; }
        public virtual bool? Revised { get; set; }
        public virtual bool? Archived { get; set; }
        public virtual decimal Total { get; set; }
        public virtual string SalesOrderNumber { get; set; }
        public virtual decimal OverallDiscount { get; set; }
        public virtual decimal OverallDiscountinUSD { get; set; }
        public virtual string CustomerPONumber { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public int TenantId { get; set; }
        [ForeignKey("MileStoneId")]
        public virtual MileStone MileStones { get; set; }
        public virtual int? MileStoneId { get; set; }
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
