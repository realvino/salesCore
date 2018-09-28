using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.PaymentSchedules;

namespace tibs.stem.Quotationss.Dto
{
    [AutoMapFrom(typeof(PaymentSchedule))]
    public class PaymentScheduleLists
    {
        public virtual int Id { get; set; }
        public int TenantId { get; set; }
        public virtual int QuotationId { get; set; }
        public virtual DateTime? ScheduledDate { get; set; }
        public virtual decimal? Total { get; set; }
        public virtual long? UserId { get; set; }
        public virtual string UserName { get; set; }
    }
}
