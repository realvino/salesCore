using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.CustomerCompanys
{
    [Table("ContactInformation")]
    public class ContactInfo : FullAuditedEntity
    {
        [ForeignKey("CompanyId")]
        public virtual Company Companys { get; set; }
        public virtual int? CompanyId { get; set; }

        [ForeignKey("ContacId")]
        public virtual Contact Contacts { get; set; }
        public virtual int? ContacId { get; set; }

        [ForeignKey("InfoTypeId")]
        public virtual InfoType InfoTypes { get; set; }
        public virtual int? InfoTypeId { get; set; }

        public virtual string InfoData { get; set; }
    }
}
