using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Currencies;

namespace tibs.stem.Currencyy.Dto
{
    [AutoMapFrom(typeof(Currency))]
    public class CurrencyListDto 
    {
        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual decimal ConversionRatio { get; set; }

        public int Id { get; set; }
    }
}
