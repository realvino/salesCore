using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.ActivityCommands;

namespace tibs.stem.ActivityCommandss.Dto
{
    [AutoMapTo(typeof(ActivityCommand))]
    public class CreateActivityCommandInput
    {
        public virtual int Id { get; set; }
        public virtual int ActivityId { get; set; }
        public virtual string Commands { get; set; }

    }
}
