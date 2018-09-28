using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using tibs.stem.MileStones;
using tibs.stem.MileStoneStatuss;

namespace tibs.stem.MileStoneStatusDetails
{
    [Table("MileStoneStatusDetail")]
    public class MileStoneStatusDetail : FullAuditedEntity
    {
        [ForeignKey("MileStoneId")]
        public virtual MileStone MileStones { get; set; }
        public virtual int MileStoneId { get; set; }

        [ForeignKey("MileStoneStatusId")]
        public virtual MileStoneStatus EnquiryStatuss { get; set; }
        public virtual int MileStoneStatusId { get; set; } 

    }
}
