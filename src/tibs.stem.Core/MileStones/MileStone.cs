using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.MileStones
{
    [Table("MileStone")]
   public class MileStone : FullAuditedEntity,IMustHaveTenant
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public int TenantId { get; set; }
        public virtual bool IsQuotation { get; set; }
        public virtual bool EndOfQuotation { get; set; }


    }
}
