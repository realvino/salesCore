using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.ActivityCommands;

namespace tibs.stem.ActivityCommandss.Dto
{
    [AutoMapFrom(typeof(ActivityCommand))]
   public class ActivityCommandListDto
    {
        public virtual int Id { get; set; }
        public virtual int ActivityId { get; set; }
        public virtual string Commands { get; set; }
    }
}
