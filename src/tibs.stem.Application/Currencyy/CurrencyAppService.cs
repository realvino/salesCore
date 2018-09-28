using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Authorization;
using tibs.stem.Currencies;
using tibs.stem.Currencyy.Dto;
using tibs.stem.Dto;
using System.Net.Http;
using System.Net.Http.Headers;
using Abp.Linq.Extensions;
using tibs.stem;
using tibs.stem.Currencyy.Exporting;

namespace tibs.stem.Currencyy
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Master_Currency)]
    public class CurrencyAppService : stemAppServiceBase, ICurrencyAppService
    {
        private readonly IRepository<Currency> _CurrencyRepository;
        private readonly IRepository<CustomCurrency> _CustomcurrencyRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICurrencyExcelExporter _currencyExcelExporter;

        public CurrencyAppService(
            IRepository<Currency> CurrencyRepository,
            IRepository<CustomCurrency> CustomcurrencyRepository,
            IUnitOfWorkManager unitOfWorkManager,
            ICurrencyExcelExporter currencyExcelExporter
            )
        {
            _CurrencyRepository = CurrencyRepository;
            _CustomcurrencyRepository = CustomcurrencyRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _currencyExcelExporter = currencyExcelExporter;

        }

        public async Task<FileDto> GetCurrencyToExcel()
        {


            var currencyquery = from c in _CurrencyRepository.GetAll()
                                select new CurrencyListDto
                                {
                                    Code = c.Code,
                                    Name = c.Name,
                                    ConversionRatio = c.ConversionRatio,
                                    Id = c.Id
                                };


            var CurrencyListDtos = currencyquery.MapTo<List<CurrencyListDto>>();

            return _currencyExcelExporter.ExportToFile(CurrencyListDtos);
        }

        public ListResultDto<CurrencyListDto> GetCurrency(GetCurrencyInput input)
        {
            var Currency = _CurrencyRepository
                .GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.Name.Contains(input.Filter) ||
                         p.Code.Contains(input.Filter)
                )
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Code)
                .ToList();

            return new ListResultDto<CurrencyListDto>(Currency.MapTo<List<CurrencyListDto>>());
        }

        public async Task<GetCurrencyNew> GetCurrencyNew()
        {
            var output = new GetCurrencyNew
            {
            };
            var currencyquery = from c in _CurrencyRepository.GetAll()
                                select new CurrencyListDto
                                {
                                    Code = c.Code,
                                    Name = c.Name,
                                    ConversionRatio = c.ConversionRatio,
                                    Id = c.Id
                                };

            var currency = currencyquery.ToArray();
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                if (currencyquery != null)
                {
                    var query = from c in _CurrencyRepository.GetAll()
                                join cc in _CustomcurrencyRepository.GetAll() on c.Id equals cc.CurrencyId
                                select new CustomCurrencyListDto
                                {
                                    Code = cc.Code,
                                    Name = cc.Name,
                                    ConversionRatio = cc.ConversionRatio,
                                    CurrencyId = cc.CurrencyId,
                                    TenantId = cc.TenantId,
                                    Online = cc.Online,
                                    Id = cc.Id
                                };
                    var Custcurrency = query.ToArray();
                    output = new GetCurrencyNew
                    {
                        Currency = currency,
                        CustomCurrency = Custcurrency
                    };
                }
              
            }

            return output;

        }
        public async Task<GetCurrency> GetCurrencyForEdit(EntityDto input)
        {
             var output = new GetCurrency
             {
             };
           
                 var currency = _CurrencyRepository
                     .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

                 output.Currency = currency.MapTo<CreateCurrencyInput>();
           
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_Currency_Edit)]
        public async Task<GetCustomCurrency> GetCustomCurrencyForEdit(EntityDto input)
        {
            var output = new GetCustomCurrency
            {
            };

            var currency = _CustomcurrencyRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.CustomCurrency = currency.MapTo<CreateCustomCurrencyInput>();

            return output;
        }

        public async Task CreateOrUpdateCurrency(CreateCurrencyInput input)
        {
            if (input.Id != 0)
            {
                await UpdateCurrencyAsync(input);
            }
            else
            {
                await CreateCurrencyAsync(input);
            }
        }

        public virtual async Task CreateCurrencyAsync(CreateCurrencyInput input)
        {
            var Currency = input.MapTo<Currency>();
            DateTime myUtcDateTime = DateTime.Now;
            try
            {
                var val = _CurrencyRepository
                  .GetAll().Where(p => p.Code == input.Code || p.Name == input.Name).FirstOrDefault();
                if (val == null)
                {
                    await _CurrencyRepository.InsertAsync(Currency);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Currency Name '" + input.Name + "' or Currency Code '" + input.Code + "'...");
                }
            }
            catch(Exception obj)
            {
                string dd = obj.Message.ToString();
            }
        }

        public virtual async Task UpdateCurrencyAsync(CreateCurrencyInput input)
        {
            var Currency = input.MapTo<Currency>();
            var val = _CurrencyRepository
              .GetAll().Where(p => (p.Code == input.Code || p.Name == input.Name) && p.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                await _CurrencyRepository.UpdateAsync(Currency);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Currency Name '" + input.Name + "' or Currency Code '" + input.Code + "'...");
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_Currency_Create)]
        public async Task CreateOrUpdateCustomCurrency(CreateCustomCurrencyInput input)
        {
            if (input.Id != 0)
            {
                await UpdateCustomCurrencyAsync(input);
            }
            else
            {
                await CreateCustomCurrencyAsync(input);
            }
        }

        public virtual async Task CreateCustomCurrencyAsync(CreateCustomCurrencyInput input)
        {
            if (input.Online == true)
            {
                try
                {
                    string url = string.Format("http://download.finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=l1", "USD", input.Code);
                    HttpClient client = new HttpClient();
                    var response = await client.GetStringAsync(url);
                    decimal exchangeRate = decimal.Parse(response, System.Globalization.CultureInfo.InvariantCulture);
                    input.ConversionRatio = Math.Round(exchangeRate, 3);
                }
                catch (Exception ex)
                {
                    input.ConversionRatio = 0;
                }
            }
            input.TenantId = (int)(AbpSession.TenantId);
            var CustCurrency = input.MapTo<CustomCurrency>();
                var val = _CustomcurrencyRepository
                  .GetAll().Where(p => p.CurrencyId == input.CurrencyId && p.TenantId == input.TenantId).FirstOrDefault();
                if (val == null)
                {
                    await _CustomcurrencyRepository.InsertAsync(CustCurrency);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Currency...");
                }
        }

        public virtual async Task UpdateCustomCurrencyAsync(CreateCustomCurrencyInput input)
        {

            if (input.Online == true)
            {
                try {
                    string url = string.Format("http://download.finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=l1", "USD", input.Code);
                    HttpClient client = new HttpClient();
                    var response = await client.GetStringAsync(url);
                    decimal exchangeRate = decimal.Parse(response, System.Globalization.CultureInfo.InvariantCulture);
                    input.ConversionRatio = Math.Round(exchangeRate, 3);
                } catch (Exception ex)
                {
                    input.ConversionRatio = 0;
                }
               
            }
            input.TenantId = (int)(AbpSession.TenantId);
            var Currency = input.MapTo<CustomCurrency>();
            var val = _CustomcurrencyRepository
              .GetAll().Where(p => p.CurrencyId == input.CurrencyId && p.TenantId == input.TenantId && p.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                await _CustomcurrencyRepository.UpdateAsync(Currency);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Currency ...");
            }
        }

        public async Task DeleteCurrency(EntityDto input)
        {
            await _CurrencyRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_Currency_Delete)]
        public async Task DeleteCustomCurrency(EntityDto input)
        {

            await _CustomcurrencyRepository.DeleteAsync(input.Id);   
        }

        public virtual async Task CurrencyConversion(CurrencyListDto input)
        {

            //string url = string.Format("http://finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=l1", "USD", input.Code);
            //string response = new WebClient().DownloadString(url);
            //decimal exchangeRate = decimal.Parse(response, System.Globalization.CultureInfo.InvariantCulture);
            //decimal rate = Math.Round(exchangeRate, 3);

            try
            {
                string url = string.Format("http://finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=l1", "USD", "AED");
                HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                var response = await client.GetStringAsync(url);

                decimal exchangeRate = decimal.Parse(response, System.Globalization.CultureInfo.InvariantCulture);
                decimal rate = Math.Round(exchangeRate, 3);

            }
            catch (Exception obj)
            {
                string dd = obj.Message.ToString();
            }
        }


        public class FindDelete
        {
            public int id { get; set; }

            public string name { get; set; }
        }
        public class CurrencyCon
        {
            public int id { get; set; }

            public string Code { get; set; }
        }
    }
}
