using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.TargetTypess;

namespace tibs.stem.TargetType.Dto
{
     
    [AutoMap(typeof(TargetTypes))]
    public class TargetTypeListDto
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

    }
}
