using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.VatAmountSettings
{
    [Table("SettingVatAmount")]
    public class TenantVatAmountSetting: FullAuditedEntity, IMustHaveTenant
    {
        public virtual decimal VatAmount { get; set; }
        public virtual int TenantId { get; set; }
    }
}
