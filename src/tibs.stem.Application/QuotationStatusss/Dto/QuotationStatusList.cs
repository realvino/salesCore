using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.QuotationStatuss;

namespace tibs.stem.QuotationStatusss.Dto
{
    [AutoMapFrom(typeof(QuotationStatus))]
    public class QuotationStatusList : FullAuditedEntity
    {
        public  new int Id { get; set; }
        public virtual string QuotationStatusCode { get; set; }
        public virtual string QuotationStatusName { get; set; }
        public int TenantId { get; set; }
        public virtual int? MileStoneId { get; set; }
        public virtual int? MileStoneName { get; set; }
        public virtual bool New { get; set; }
        public virtual bool Submitted { get; set; }
        public virtual bool Revised { get; set; }
        public virtual bool Won { get; set; }
        public virtual bool Lost { get; set; }
    }
}
