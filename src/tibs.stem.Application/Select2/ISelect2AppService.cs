using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Select2
{
    public interface ISelect2AppService : IApplicationService
    {
        Task<Select2Result> GetPriceLevel();
        Task<Select2Company> GetCompany();
        Task<Select2Result> GetMilestone();
        Task<Select2Result> GetCompanyContact(Select2ResultInput input);
        Task<Select2Result> GetMileStoneMileStatus(Select2ResultInput input);
        Task<Select2Result> GetCountry();
        Task<Select3Result> GetCurrency();
        Task<Select2Result> GetContactInfo();
        Task<Select2Result> GetAddressInfo();
        Task<Select2Result> GetContactType();
        Task<Select2Result> GeCompanyType();
        Task<Select2Result> GetActivityType();
        Task<Select2Result> GetProductGroup();
        Task<Select2Result> GetProductSubGroup();
        Task<Select2Result> GetTenantType();
        Task<Select2Result> GetSalesman();
        Task<Select2Result> GetQuotationTitle();
        Task<Select2Result> GetStatus();
        Task<Select2Result> GetFreight();
        Task<Select2Result> GetPayment();
        Task<Select2Result> GetPacking();
        Task<Select2Result> GetWarranty();
        Task<Select2Result> GetValidity();
        Task<Select2Result> GetDelivery();
        Task<Select2Result> GetReason();
        Task<Select2Result> GetProduct();
        Task<Select2Result> GetService();
        Task<Select2Result> GetTargetType();
        Task<Select2Result> GetSalesPerson();
        Task<Select5Result> GetEnquiryDetail(NullableIdDto input);
        Task<Select2Result> GetEnquiry(string Input);
        Task<Select2Result> GetPayments();
        Task<Select6Result> GetDueAmount(NullableIdDto input);
        Task<Select2Result> GetCompetitor();
        Task<Select2Result> GetDashboardSelect();
        Task<Select2Result> GetQuotationMilestone();
        Task<Select3Result> GetQuotationStatus();

    }
}
