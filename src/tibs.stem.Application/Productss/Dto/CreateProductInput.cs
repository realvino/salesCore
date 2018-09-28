using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Products;

namespace tibs.stem.Productss.Dto
{
    [AutoMap(typeof(Product))]
    public class CreateProductInput
    {
        public virtual int Id { get; set; }
        public virtual string ProductCode { get; set; }
        public virtual string ProductName { get; set; }
        public bool? Discontinued { get; set; }
        public virtual string Description { get; set; }
        public virtual int ProductSubGroupId { get; set; }
        public virtual string Path { get; set; }
        public int TenantId { get; internal set; }
    }
    
}
