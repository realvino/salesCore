using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.VatAmountSettings;

namespace tibs.stem.Tenants.Dashboard.Dto
{
    [AutoMap(typeof(TenantVatAmountSetting))]
    public class SettingVatAmountInput
    {
        public virtual int Id { get; set; }
        public virtual decimal VatAmount { get; set; }
        public virtual int TenantId { get; set; }
    }
}
