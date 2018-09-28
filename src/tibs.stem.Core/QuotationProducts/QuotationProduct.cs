using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using tibs.stem.PriceLevelProducts;
using tibs.stem.Products;
using tibs.stem.Quotations;

namespace tibs.stem.QuotationProducts
{
    [Table("QuotationProduct")]
    public class QuotationProduct : FullAuditedEntity, IMustHaveTenant
    {
        [ForeignKey("QuotationId")]
        public virtual Quotation Quotations { get; set; }
        public virtual int? QuotationId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }
        public virtual int? ProductId { get; set; }

        [ForeignKey("PriceLevelProductId")]
        public virtual PriceLevelProduct PriceLevelProducts { get; set; }
        public virtual int? PriceLevelProductId { get; set; }

        public virtual int Quantity { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal PriceUSD { get; set; }
        public virtual bool? Optional { get; set; }
        public virtual bool? Discount { get; set; }
        public virtual decimal EstimatedPrice { get; set; }
        public virtual decimal EstimatedPriceUSD { get; set; }
        public int TenantId { get; set; }
    }
}
