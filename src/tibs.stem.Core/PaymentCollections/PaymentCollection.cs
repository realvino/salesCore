using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Currencies;
using tibs.stem.Payments;
using tibs.stem.Quotations;

namespace tibs.stem.PaymentCollections
{
    [Table("PaymentCollection")]
    public class PaymentCollection : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [ForeignKey("QuotationId")]
        public virtual Quotation Quotations { get; set; }
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
        [ForeignKey("CurrencyId")]
        public virtual Currency Currencys { get; set; }
        public virtual int? CurrencyId { get; set; }
        [ForeignKey("PaymentId")]
        public virtual Payment Payments { get; set; }
        public virtual int? PaymentId { get; set; }
    }
}
