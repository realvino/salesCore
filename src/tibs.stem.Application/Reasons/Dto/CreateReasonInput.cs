using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Reasonss;

namespace tibs.stem.Reasons.Dto
{
     
    [AutoMap(typeof(Reason))]
    public class CreateReasonInput
    {
        public int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public int TenantId { get; internal set; }

    }
}
