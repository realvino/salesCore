using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.MileStoneStatusDetails;

namespace tibs.stem.Milestones.Dto
{
   // [AutoMapTo(typeof(MileStoneStatusDetail))]
    [AutoMap(typeof(MileStoneStatusDetail))]
    public class CreateMileStoneDetailInput
    {
        public virtual int MileStoneId { get; set; }

        public virtual int MileStoneStatusId { get; set; }

        public int Id { get; set; }
    }
}
