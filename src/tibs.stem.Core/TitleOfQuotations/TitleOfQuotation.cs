using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.TitleOfQuotations
{
    [Table("TitleOfQuotation")]
    public class TitleOfQuotation : FullAuditedEntity, IMustHaveTenant
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public int TenantId { get; set; }

    }
}
