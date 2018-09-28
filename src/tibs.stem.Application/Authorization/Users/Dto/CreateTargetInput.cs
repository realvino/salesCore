using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Targetss;

namespace tibs.stem.Authorization.Users.Dto
{
    [AutoMap(typeof(Targets))]
    public class CreateTargetInput
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public int TargetAmount { get; set; }
        public virtual int TargetTypeId { get; set; }
        public DateTime ValidityDate { get; set; }
        public int TenantId { get; set; }
        public decimal Total { get; set; }

    }
}
