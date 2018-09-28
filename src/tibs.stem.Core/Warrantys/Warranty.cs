using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.Warrantys
{
    [Table("Warranty")]
    public class Warranty : FullAuditedEntity, IMustHaveTenant
    {
        public virtual string WarrantyCode { get; set; }
        public virtual string WarrantyName { get; set; }
        public int TenantId { get; set; }

    }
}
