using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using tibs.stem.ProductSubGroups;

namespace tibs.stem.Products
{
    [Table("Product")]
    public class Product : FullAuditedEntity, IMustHaveTenant
    {
        public virtual string ProductCode { get; set; }
        public virtual string ProductName { get; set; }
        public bool? Discontinued { get; set; }
        public virtual string Description { get; set; } 

        [ForeignKey("ProductSubGroupId")]
        public virtual ProductSubGroup ProductSubGroup { get; set; }
        public virtual int ProductSubGroupId { get; set; }

        public virtual string Path { get; set; }
        public int TenantId { get; set; }

    }

}
