using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Services;

namespace tibs.stem.Servicess.Dto
{
    [AutoMapFrom(typeof(Service))]
    public class ServiceList : FullAuditedEntity
    {
        public int Id { get; set; }
        public virtual string ServiceCode { get; set; }
        public virtual string ServiceName { get; set; }
        public int TenantId { get; set; }
    }
}
