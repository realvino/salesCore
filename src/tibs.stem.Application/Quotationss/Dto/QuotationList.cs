using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.QuotationProducts;
using tibs.stem.Quotations;
using tibs.stem.QuotationServices;

namespace tibs.stem.Quotationss.Dto
{
    [AutoMapFrom(typeof(Quotation))]
    public class QuotationList
    {
        public virtual int Id { get; set; }
        public virtual bool IsQuotationRevised { get; set; }
        public virtual string SubjectName { get; set; }
        public virtual string ProposalNumber { get; set; }
        public virtual string ProjectRef { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual string DateString { get; set; }
        public virtual DateTime? ClosureDate { get; set; }
        public virtual bool? Revised { get; set; }
        public virtual bool? Archived { get; set; }
        public virtual decimal Total { get; set; }
        public virtual string SalesOrderNumber { get; set; }
        public virtual DateTime? LostDate { get; set; }
        public virtual decimal OverallDiscount { get; set; }
        public virtual decimal OverallDiscountinUSD { get; set; }
        public virtual string CustomerPONumber { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public virtual int? EnquiryId { get; set; }
        public virtual string EnquiryTitle { get; set; }
        public virtual int? QuotationTitleId { get; set; }
        public virtual string QuotationTitleName { get; set; }
        public virtual int? CompanyId { get; set; }
        public virtual string CompanyName { get; set; }
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
        public virtual int? ReasonId { get; set; }
        public virtual string ReasonName { get; set; }
        public int TenantId { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual string ContactName { get; set; }
        public virtual int? MileStoneId { get; set; }
        public virtual bool Submitted { get; set; }
        public virtual DateTime? SubmittedDate { get; set; }
        public virtual bool Won { get; set; }
        public virtual DateTime? WonDate { get; set; }
        public virtual bool Lost { get; set; }
        public virtual decimal OverAllTotal { get; set; }
        public virtual int? CompetitorId { get; set; }
        public virtual string CompetitorName { get; set; }
        public virtual bool Vat { get; set; }
        public virtual decimal VatPercentage { get; set; }
        public virtual decimal VatAmount { get; set; }
        public DateTime CreationTime { get; internal set; }
    }

    [AutoMapFrom(typeof(QuotationProduct))]
    public class QuotationProductList
    {
        public int Id { get; set; }
        public virtual int? QuotationId { get; set; }
        public virtual string QuotationName { get; set; }
        public virtual int? ProductId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual int? PriceLevelProductId { get; set; }
        public virtual string PriceLevelProductName { get; set; }
        public virtual decimal PriceUSD { get; set; }
        public virtual decimal EstimatedPriceUSD { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal Price { get; set; }
        public virtual bool? Optional { get; set; }
        public virtual bool? Discount { get; set; }
        public virtual decimal EstimatedPrice { get; set; }
        public int TenantId { get; set; }


    }

    [AutoMapFrom(typeof(QuotationService))]
    public class QuotationServiceList
    {
        public int Id { get; set; }
        public virtual int? QuotationId { get; set; }
        public virtual string QuotationName { get; set; }
        public virtual int? ServiceId { get; set; }
        public virtual string ServiceCode { get; set; }
        public virtual string ServiceName { get; set; }
        public virtual decimal Price { get; set; }
        public int TenantId { get; set; }
        public virtual decimal CovertPrice { get; set; }
    }

    public class GetQuotationPreview
    {
        public GetPreview preview { get; set; }
    }

    public class GetPreview
    {

        public int QuotationId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual decimal Total { get; set; }
    }

}
