using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Services;

namespace tibs.stem.Servicess.Dto
{
    [AutoMap(typeof(Service))]
    public class CreateServiceInput
    {
        public int Id { get; set; }
        public virtual string ServiceCode { get; set; }
        public virtual string ServiceName { get; set; }
        public int TenantId { get; set; }
    }
}
