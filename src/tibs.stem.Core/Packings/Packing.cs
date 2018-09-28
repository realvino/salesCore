using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.Packings
{
    [Table("Packing")]
    public class Packing : FullAuditedEntity, IMustHaveTenant
    {
        public virtual string PackingCode { get; set; }
        public virtual string PackingName { get; set; }
        public int TenantId { get; set; }

    }
}
