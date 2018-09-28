using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace tibs.stem.ProductSubGroups.Dto
{
    [AutoMapFrom(typeof(ProductSubGroup))]
    public class ProductSubGpListDto
    {
        public int Id { get; set; }
        public virtual string ProductSubGroupName { get; set; }
        public virtual string ProductSubGroupCode { get; set; }
        public virtual int ProductGroupId { get; set; }
        public virtual string ProductGroupName { get; set; }
        public int TenantId { get; set; }
        public virtual string Path { get; set; }

    }
}
