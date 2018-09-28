using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Tenants.Dashboard.Dto
{
    public class SliderDataList
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ProfilePicture { get; set; }
        public virtual string Email { get; set; }
    }
}
