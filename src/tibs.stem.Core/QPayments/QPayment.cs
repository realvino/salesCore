using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.QPayments
{
    [Table("QPayment")]
    public class QPayment : FullAuditedEntity, IMustHaveTenant
    {
        public virtual string PaymentCode { get; set; }
        public virtual string PaymentName { get; set; }
        public int TenantId { get; set; }

    }
}
