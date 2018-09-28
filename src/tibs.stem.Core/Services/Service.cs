using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.Services
{
    [Table("Service")]
    public class Service : FullAuditedEntity, IMustHaveTenant
    {
        public virtual string ServiceCode { get; set; }
        public virtual string ServiceName { get; set; }
        public int TenantId { get; set; }

    }
}
