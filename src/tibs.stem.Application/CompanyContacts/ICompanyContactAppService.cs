using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.NewCompanyContacts.Dto;

namespace tibs.stem.NewCompanyContacts
{
    public interface ICompanyContactAppService : IApplicationService
    {
        Task<FileDto> GetCompanyToExcel();
        Task<FileDto> GetContactToExcel();
        Task<PagedResultDto<NewCompanyListDto>> GetCompanys(GetCompanyInput input);
        Task<int> CreateOrUpdateCompanyOrContact(CreateCompanyOrContact input);
        Task<Array> GetCompanyForEdit(NullableIdDto input);
        Task CreateOrUpdateAddressInfo(CreateAddressInfo input);
        Task CreateOrUpdateContactInfo(CreateContactInfo input);
        Task<GetNewAddressInfo> GetAddressInfoForEdit(NullableIdDto input);
        Task<GetNewContactInfo> GetContactInfoForEdit(NullableIdDto input);
        Task GetDeleteAddressInfo(EntityDto input);
        Task GetDeleteContactInfo(EntityDto input);
        Task GetDeleteCompany(EntityDto input);
        Task<PagedResultDto<NewContactListDto>> GetContacts(GetContactInput input);
        Task GetDeleteContact(EntityDto input);
        Task<Array> GetContactForEdit(NullableIdDto input);
        ListResultDto<NewContactListDto> GetCompanyContacts(NullableIdDto input);

        //Task<int> ContactUpdate(CreateCompanyOrContact input);

    }
}
