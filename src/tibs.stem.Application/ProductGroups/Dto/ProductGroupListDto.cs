using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace tibs.stem.ProductGroups.Dto
{
    [AutoMapFrom(typeof(ProductGroup))]
   public class ProductGroupListDto
    {
        public int Id { get; set; }
        public virtual string ProductGroupName { get; set; }
        public virtual string ProductGroupCode { get; set; }
        public int TenantId { get; set; }
        public virtual string Path { get; set; }


    }
}
