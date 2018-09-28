using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tibs.stem.PriceLevels
{
    [Table("PriceLevel")]
    public class PriceLevel : FullAuditedEntity, IMustHaveTenant
    {
        public string PriceLevelCode { get; set; }
        public string PriceLevelName { get; set; }
        public int TenantId { get; set; }
    }
   
}
