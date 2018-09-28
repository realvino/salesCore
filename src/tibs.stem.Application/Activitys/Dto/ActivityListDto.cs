using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace tibs.stem.Activitys.Dto
{
    [AutoMap(typeof(Activity))]
    public class ActivityListDto
    {
        public virtual int Id { get; set; }
        public virtual int ActivityTypeId { get; set; }
        public virtual string ActTypeName { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }

        public virtual int? EnquiryId { get; set; }
        public virtual string EnquiryNo { get; set; }
        public virtual DateTime? ScheduleTime { get; set; }
        public virtual bool Status { get; set; }
        public int TenantId { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual string ContactName { get; set; }
    }
}
