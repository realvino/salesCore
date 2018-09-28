using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using tibs.stem.MileStones;

namespace tibs.stem.QuotationStatuss
{
    [Table("QuotationStatus")]
    public class QuotationStatus : FullAuditedEntity, IMustHaveTenant
    {
        public virtual string QuotationStatusCode { get; set; }
        public virtual string QuotationStatusName { get; set; }
        public int TenantId { get; set; }

        [ForeignKey("MileStoneId")]
        public virtual MileStone MileStones { get; set; }
        public virtual int? MileStoneId { get; set; }

        public virtual bool New { get; set; }
        public virtual bool Submitted { get; set; }
        public virtual bool Revised { get; set; }
        public virtual bool Won { get; set; }
        public virtual bool Lost { get; set; }


    }
}
