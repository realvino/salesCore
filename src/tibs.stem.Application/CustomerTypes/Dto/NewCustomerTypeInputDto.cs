using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.CustomerCompanys;

namespace tibs.stem.NewCustomerTypes.Dto
{
    [AutoMapTo(typeof(CustomerType))]
    public class NewCustomerTypeInputDto
    {
        public int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual bool Company { get; set; }
        public int TenantId { get; set; }

    }
}
