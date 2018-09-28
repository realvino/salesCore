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
    [AutoMap(typeof(Currency))]
    public class CreateCurrencyInput 
    {
        public  long  Id { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual decimal ConversionRatio { get; set; }
    }
}
