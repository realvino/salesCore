using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using tibs.stem.Quotations;
using tibs.stem.Services;

namespace tibs.stem.QuotationServices
{
    [Table("QuotationService")]
    public class QuotationService : FullAuditedEntity, IMustHaveTenant
    {
        [ForeignKey("QuotationId")]
        public virtual Quotation Quotations { get; set; }
        public virtual int? QuotationId { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Services { get; set; }
        public virtual int? ServiceId { get; set; }

        public virtual decimal Price { get; set; }
        public int TenantId { get; set; }
        public virtual decimal CovertPrice { get; set; }
    }
}
