using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Activitys;

namespace tibs.stem.ActivityCommands
{
   
    [Table("ActivityCommand")]
    public class ActivityCommand : FullAuditedEntity
    {

        [ForeignKey("ActivityId")]
        public virtual Activity Activitys { get; set; }
        public virtual int ActivityId { get; set; }
        public virtual string Commands { get; set; }
    }
}
