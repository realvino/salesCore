using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.Countrys
{
    [Table("Country")]
    public class Country : FullAuditedEntity
    {
        public virtual string CountryName { get; set; }

        public virtual string CountryCode { get; set; }

        public virtual string ISDCode { get; set; }
    }
}
