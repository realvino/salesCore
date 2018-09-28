using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.PriceLevelProducts;

namespace tibs.stem.Productss.Dto
{
    [AutoMapFrom(typeof(PriceLevelProduct))]
    public class PriceLevelProductList
    {
        public int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int PriceLevelId { get; set; }
        public virtual string PriceLevelName { get; set; }
        public virtual double Price { get; set; }
        public int TenantId { get; set; }
    }
    [AutoMap(typeof(PriceLevelProduct))]
    public class CreatePriceLevelProductInput
    {
        public int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int PriceLevelId { get; set; }
        public virtual double Price { get; set; }
        public int TenantId { get; set; }
    }
    public class PriceLevelProductListInput
    {
        public virtual int ProductId { get; set; }
    }
    public class PriceLevelInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GetPriceLevelProduct
    {
        public PriceLevelInput[] PriceLevel { get; set; }
    }
}
