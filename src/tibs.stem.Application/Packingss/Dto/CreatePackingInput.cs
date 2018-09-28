using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Packings;

namespace tibs.stem.Packingss.Dto
{
    [AutoMap(typeof(Packing))]
    public class CreatePackingInput
    {
        public int Id { get; set; }
        public virtual string PackingCode { get; set; }
        public virtual string PackingName { get; set; }
        public int TenantId { get; set; }

    }
}
