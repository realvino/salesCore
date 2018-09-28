using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Targetss;

namespace tibs.stem.Authorization.Users.Dto
{
     
    [AutoMap(typeof(Targets))]
    public class TargetListDto
    {
        public int Id { get; set; }
        public virtual long UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual int TargetAmount { get; set; }
        public virtual int TargetTypeId { get; set; }
        public virtual string TargetTypeName { get; set; }
        public DateTime? ValidityDate { get; set; }
        public int TenantId { get; set; }
        public decimal Total { get; set; }

    }
}
