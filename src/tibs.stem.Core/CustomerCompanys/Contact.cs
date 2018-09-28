using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.TitleOfCourtes;

namespace tibs.stem.CustomerCompanys
{
    [Table("Contact")]
    public class Contact : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public virtual string Name { get; set; }

        [ForeignKey("CustomerTypeId")]
        public virtual CustomerType CustomerTypes { get; set; }
        public virtual int? CustomerTypeId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Companys { get; set; }
        public virtual int? CompanyId { get; set; }

        [ForeignKey("TitleId")]
        public virtual TitleOfCourtesy TitleOfCourtesies { get; set; }
        public virtual int? TitleId { get; set; }

        public virtual string LastName { get; set; }

        //[ForeignKey("IndustryId")]
        //public virtual Industry Industries{ get; set; }
        //public virtual int? IndustryId { get; set; }


    }
}
