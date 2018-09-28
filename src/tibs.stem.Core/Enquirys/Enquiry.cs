using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using tibs.stem.CustomerCompanys;
using tibs.stem.MileStones;
using tibs.stem.MileStoneStatuss;

namespace tibs.stem.Enquirys
{

    [Table("Enquiry")]
    public class Enquiry : FullAuditedEntity,IMustHaveTenant
    {
        public int TenantId { get; set; }
        public virtual string EnquiryNo { get; set; }
        public virtual string Title { get; set; }

        [ForeignKey("MileStoneId")]
        public virtual MileStone MileStones { get; set; }
        public virtual int MileStoneId { get; set; }
        [ForeignKey("MileStoneStatusId")]
        public virtual MileStoneStatus MileStoneStatuss { get; set; }
        public virtual int? MileStoneStatusId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Companys { get; set; }
        public virtual int CompanyId { get; set; }
        [ForeignKey("ContactId")]
        public virtual Contact Contacts { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual string Remarks { get; set; }
        public virtual DateTime? CloseDate { get; set; }
        public decimal EstimationValue { get; set; }

    }
}
