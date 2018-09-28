using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using tibs.stem.Authorization.Users;
using tibs.stem.TargetTypess;

namespace tibs.stem.Targetss
{
     
    [Table("Targets")]
    public class Targets : FullAuditedEntity, IMustHaveTenant
    {
       
        [ForeignKey("UserId")]
        public virtual User Users { get; set; }
        public virtual long UserId { get; set; }
        public int TargetAmount { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("TargetTypeId")]
        public virtual TargetTypes TargetType { get; set; }
        public virtual int TargetTypeId { get; set; }
        public DateTime ValidityDate { get; set; }
        public int TenantId { get; set; }
    }
}
