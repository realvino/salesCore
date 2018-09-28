using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.NewCustomerTypes.Dto
{
    public class NewCustomerTypeListDto
    {
        public int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual bool Company { get; set; }
        public int TenantId { get; set; }
    }
}
