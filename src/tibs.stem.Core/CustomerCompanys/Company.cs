using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Authorization.Users;
using tibs.stem.Countrys;
using tibs.stem.Currencies;

namespace tibs.stem.CustomerCompanys
{
    [Table("Company")]
    public class Company : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public virtual string Name { get; set; }

        [ForeignKey("CustomerTypeId")]
        public virtual CustomerType CustomerTypes { get; set; }
        public virtual int? CustomerTypeId { get; set; }

        [ForeignKey("AccountManagerId")]
        public virtual User AbpAccountManager { get; set; }
        public virtual long? AccountManagerId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Countrys { get; set; }
        public virtual int? CountryId { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual Currency Currencys { get; set; }
        public virtual int? CurrencyId { get; set; }
    }
}
