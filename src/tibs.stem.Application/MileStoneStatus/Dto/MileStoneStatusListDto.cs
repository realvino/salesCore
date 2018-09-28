using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.MileStoneStatuss.Dto
{
    [AutoMapFrom(typeof(MileStoneStatus))]
    public class MileStoneStatusListDto 
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Color { get; set; }
        public int Id { get; set; }
        public int TenantId { get; set; }

    }
}
