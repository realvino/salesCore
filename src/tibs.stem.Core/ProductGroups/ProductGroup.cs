using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.ProductGroups
{
    [Table("ProductGroup")]
   public class ProductGroup : FullAuditedEntity, IMustHaveTenant

    {
        public virtual string ProductGroupName { get; set; }

        public virtual string ProductGroupCode { get; set; }

        public virtual string Path { get; set; }

        public int TenantId { get; set; }
    }
}
