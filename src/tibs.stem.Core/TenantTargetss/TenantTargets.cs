using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.TenantTargetss
{
     
    [Table("TenantTargets")]
    public class TenantTargets : FullAuditedEntity , IMustHaveTenant
    {
        public DateTime TargetDate { get; set; }
        public virtual int Value { get; set; }
        public int TenantId { get; set; }
    }
}
