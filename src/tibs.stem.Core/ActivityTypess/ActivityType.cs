using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.ActivityTypess
{
    [Table("ActivityType")]
    public class ActivityType : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

    }
}
