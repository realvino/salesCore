using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.ActivityTypess;

namespace tibs.stem.ActivityTypes.Dto
{
    [AutoMap(typeof(ActivityType))]
    public class CreateActivityTypeInput
    {
        public int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public int TenantId { get; set; }

    }
}
