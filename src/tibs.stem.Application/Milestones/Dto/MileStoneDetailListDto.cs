using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.MileStoneStatusDetails;

namespace tibs.stem.Milestones.Dto
{
    [AutoMap(typeof(MileStoneStatusDetail))]
    public class MileStoneDetailListDto
    {
        public virtual int MilestoneId { get; set; }
        public virtual int MilestoneStatusId { get; set; }
        public virtual string StatusName { get; set; }
        public virtual int Id { get; set; }
        public virtual bool EndOfQuotation { get; set; }


    }
}
