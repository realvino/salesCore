using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.Deliverys
{
    [Table("Delivery")]
    public class Delivery: FullAuditedEntity, IMustHaveTenant
    {
        public virtual string DeliveryCode { get; set; }
        public virtual string DeliveryName { get; set; }
        public int TenantId { get; set; }
    }
}
