using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.QuotationStatuss;

namespace tibs.stem.QuotationStatusss.Dto
{
    [AutoMap(typeof(QuotationStatus))]
    public class CreateQuotationStatusInput
    {
        public int Id { get; set; }
        public virtual string QuotationStatusCode { get; set; }
        public virtual string QuotationStatusName { get; set; }
        public int TenantId { get; set; }
        public virtual int? MileStoneId { get; set; }
        public virtual bool New { get; set; }
        public virtual bool Submitted { get; set; }
        public virtual bool Revised { get; set; }
        public virtual bool Won { get; set; }
        public virtual bool Lost { get; set; }
    }
}
