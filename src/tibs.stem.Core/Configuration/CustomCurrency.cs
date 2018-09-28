using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.Currencies
{
    [Table("CustomCurrency")]
    public class CustomCurrency : FullAuditedEntity, IMustHaveTenant
    {
        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual decimal ConversionRatio { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual Currency Currencys { get; set; }
        public virtual int CurrencyId { get; set; }
        public int TenantId { get; set; }

        public virtual bool Online { get; set; }

    }
}
