using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace tibs.stem.QPayments.Dto
{
    [AutoMap(typeof(QPayment))]
    public class QPaymentInputDto
    {
        public int Id { get; set; }
        public virtual string PaymentCode { get; set; }
        public virtual string PaymentName { get; set; }
        public int TenantId { get; set; }
    }
}
