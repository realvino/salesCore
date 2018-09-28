using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Currencyy.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Currencyy
{
    public interface ICurrencyAppService : IApplicationService
    {
        Task<FileDto> GetCurrencyToExcel();
        ListResultDto<CurrencyListDto> GetCurrency(GetCurrencyInput input);
        Task<GetCurrencyNew> GetCurrencyNew();
        Task CreateOrUpdateCurrency(CreateCurrencyInput input);
        Task<GetCurrency> GetCurrencyForEdit(EntityDto input);
        Task<GetCustomCurrency> GetCustomCurrencyForEdit(EntityDto input);
        Task DeleteCurrency(EntityDto input);
        Task CreateOrUpdateCustomCurrency(CreateCustomCurrencyInput input);
        Task CurrencyConversion(CurrencyListDto input);
        Task DeleteCustomCurrency(EntityDto input);
    }
}
