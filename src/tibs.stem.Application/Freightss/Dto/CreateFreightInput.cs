using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Freights;

namespace tibs.stem.Freightss.Dto
{
    [AutoMap(typeof(Freight))]
    public class CreateFreightInput
    {
        public int Id { get; set; }
        public virtual string FreightCode { get; set; }
        public virtual string FreightName { get; set; }
        public int TenantId { get; set; }

    }
}
