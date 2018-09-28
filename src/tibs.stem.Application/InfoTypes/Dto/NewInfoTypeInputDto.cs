using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.CustomerCompanys;

namespace tibs.stem.NewInfoTypes.Dto
{
    [AutoMapTo(typeof(InfoType))]
    public class NewInfoTypeInputDto
    {
        public int Id { get; set; }
        public virtual string ContactName { get; set; }
        public virtual bool Info { get; set; }
        public int TenantId { get; set; }

    }
}
