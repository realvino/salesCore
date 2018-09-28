using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Products;

namespace tibs.stem.Productss.Dto
{
    [AutoMapFrom(typeof(Product))]
    public class ProductListDto
    {
        public int Id { get; set; }
        public virtual string ProductCode { get; set; }
        public virtual string ProductName { get; set; }
        public bool? Discontinued { get; set; }
        public virtual string Description { get; set; }
        public virtual int ProductSubGroupId { get; set; }
        public string ProductSubGroupName { get; set; }
        public string ProductGroupName { get; set; }
        public int ProducGroupId { get; set; }
        public virtual string Path { get; set; }

    }
}
