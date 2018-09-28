using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.TenantTypes
{
    [Table("TenantType")]
    public class TenantType : FullAuditedEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
