using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Enquirys;

namespace tibs.stem.Enquiryss.Dto
{
    [AutoMap(typeof(Enquiry))]
    public class EnquiryInput
    {
        public virtual int Id { get; set; }
        public virtual string EnquiryNo { get; set; }
        public virtual string Title { get; set; }
        public virtual int MileStoneId { get; set; }
        public virtual int? MileStoneStatusId { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual string Remarks { get; set; }
        public virtual DateTime? CloseDate { get; set; }
        public int TenantId { get; set; }
        public decimal EstimationValue { get; set; }

    }
}
