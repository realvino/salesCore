using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.CustomerCompanys
{
    [Table("CustomerType")]
    public class CustomerType : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public virtual string Title { get; set; }
        public virtual bool? Company { get; set; }

    }
}
