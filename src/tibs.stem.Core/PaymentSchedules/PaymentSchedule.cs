using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Quotations;

namespace tibs.stem.PaymentSchedules
{
    [Table("PaymentSchedule")]
    public class PaymentSchedule : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [ForeignKey("QuotationId")]
        public virtual Quotation Quotations { get; set; }
        public virtual int QuotationId { get; set; }
        public virtual DateTime? ScheduledDate { get; set; }
        public virtual decimal? Total { get; set; }
    }
}
