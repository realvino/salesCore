using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.PaymentCollections;

namespace tibs.stem.Quotationss.Dto
{
    [AutoMapFrom(typeof(PaymentCollection))]
    public class PaymentCollectionLists
    {
        public virtual int Id { get; set; }
        public int TenantId { get; set; }
        public virtual int QuotationId { get; set; }
        public virtual string SalesInvoiceNUmber { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual decimal DueAmount { get; set; }
        public virtual string ChequeNo { get; set; }
        public virtual string VoucherNo { get; set; }
        public virtual DateTime? ChequeDate { get; set; }
        public virtual DateTime? BankDate { get; set; }
        public virtual string Remarks { get; set; }
        public virtual bool? Received { get; set; }
        public virtual int? CurrencyId { get; set; }
        public virtual string CurrencyName { get; set; }
        public virtual int? PaymentId { get; set; }
        public virtual string PaymentName { get; set; }
    }
}
