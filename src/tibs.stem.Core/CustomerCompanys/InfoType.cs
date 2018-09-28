using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.CustomerCompanys
{
    [Table("InfoType")] 
    public class InfoType : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public virtual string ContactName { get; set; }
        public virtual bool? Info { get; set; }
    }
}
