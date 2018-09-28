using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.CustomerCompanys
{
    [Table("AddressInformation")]
    public class AddressInfo : FullAuditedEntity
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

        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }

        //[ForeignKey("CityId")]
        //public virtual City Citys { get; set; }
        //public virtual int? CityId { get; set; }
    }
}
