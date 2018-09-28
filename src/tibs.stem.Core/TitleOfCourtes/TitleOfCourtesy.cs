using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.TitleOfCourtes
{
    [Table("TitleOfCourtesy")]
    public class TitleOfCourtesy : FullAuditedEntity
    {
        public string Name { get; set; }
    }
}
