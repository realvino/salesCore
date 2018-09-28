using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.Validitys
{
    [Table("Validity")]
    public class Validity: FullAuditedEntity, IMustHaveTenant
    {
        public virtual string ValidityCode { get; set; }
        public virtual string ValidityName { get; set; }
        public int TenantId { get; set; }
    }
}
