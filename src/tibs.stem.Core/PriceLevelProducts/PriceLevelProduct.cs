using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using tibs.stem.PriceLevels;
using tibs.stem.Products;

namespace tibs.stem.PriceLevelProducts
{
    [Table("PriceLevelProduct")]
    public class PriceLevelProduct : FullAuditedEntity, IMustHaveTenant
    {
        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }
        public virtual int ProductId { get; set; }
        [ForeignKey("PriceLevelId")]
        public virtual PriceLevel PriceLevels { get; set; }
        public virtual int PriceLevelId { get; set; }
        public virtual double Price { get; set; }
        public int TenantId { get; set; }
    }
}
