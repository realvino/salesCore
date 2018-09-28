using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using tibs.stem.ActivityTypess;
using tibs.stem.CustomerCompanys;
using tibs.stem.Enquirys;

namespace tibs.stem.Activitys
{
    [Table("Activity")]
    public class Activity : FullAuditedEntity, IMustHaveTenant
    {

        [ForeignKey("ActivityTypeId")]
        public virtual ActivityType ActivityTypes { get; set; }
        public virtual int ActivityTypeId { get; set; }

        public virtual string Title { get; set; } 
        
        public virtual string Description { get; set; }

        [ForeignKey("EnquiryId")]
        public virtual Enquiry Enquirys { get; set; }
        public virtual int? EnquiryId { get; set; }

        public virtual DateTime? ScheduleTime { get; set; }
        public virtual bool Status { get; set; }
        
        [ForeignKey("ContactId")]
        public virtual Contact Contacts { get; set; }
        public virtual int? ContactId { get; set; }
        public int TenantId { get; set; }
    }
}
