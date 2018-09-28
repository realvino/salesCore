using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.Freights
{
    [Table("Freight")]
    public class Freight: FullAuditedEntity, IMustHaveTenant
    {
        public virtual string FreightCode { get; set; }
        public virtual string FreightName { get; set; }
        public int TenantId { get; set; }
    }
}
