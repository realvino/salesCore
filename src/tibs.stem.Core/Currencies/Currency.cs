using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.Currencies
{
   [Table("Currency")]
   public class Currency : FullAuditedEntity
    {
        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual decimal ConversionRatio { get; set; }

    }
}
