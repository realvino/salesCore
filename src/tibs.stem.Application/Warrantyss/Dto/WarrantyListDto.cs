using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Warrantys;

namespace tibs.stem.Warrantyss.Dto
{
    [AutoMapFrom(typeof(Warranty))]
    public class WarrantyListDto
    {
        public int Id { get; set; }
        public virtual string WarrantyCode { get; set; }

        public virtual string WarrantyName { get; set; }
        public int TenantId { get; set; }

    }
}
