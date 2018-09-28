using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.TenantTargetss;

namespace tibs.stem.Tenants.Dashboard.Dto
{
    [AutoMap(typeof(TenantTargets))]
    public class TenantTargetListDto
    {
        public int Id { get; set; }
        public DateTime? TargetDate { get; set; }
        public virtual int Value { get; set; }
        public int TenantId { get; set; }
    }
}
