using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.TargetTypess
{
     
    [Table("TargetTypes")]
    public class TargetTypes : FullAuditedEntity
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

    }
}
