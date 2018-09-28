using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Currencies;

namespace tibs.stem.Currencyy.Dto
{
    [AutoMap(typeof(CustomCurrency))]
    public class CustomCurrencyListDto
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal ConversionRatio { get; set; }
        public virtual int CurrencyId { get; set; }
        public int TenantId { get; set; }
        public int Id { get; internal set; }
        public virtual bool Online { get; set; }

    }
}
