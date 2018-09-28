using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using tibs.stem.ProductGroups;

namespace tibs.stem.ProductSubGroups
{
    [Table("ProductSubGroup")]
    public class ProductSubGroup : FullAuditedEntity, IMustHaveTenant
    {
        public virtual string ProductSubGroupName { get; set; }
        public virtual string ProductSubGroupCode { get; set; }

        [ForeignKey("ProductGroupId")]
        public virtual ProductGroup productGroups { get; set; }
        public virtual int ProductGroupId { get; set; }
        public virtual string Path { get; set; }
        public int TenantId { get; set; }

    }

}
