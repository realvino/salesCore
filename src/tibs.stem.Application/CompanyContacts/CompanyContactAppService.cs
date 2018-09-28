using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.UI;
using System.Data;
using tibs.stem;
using Microsoft.EntityFrameworkCore;
using tibs.stem.NewCompanyContacts.Dto;
using tibs.stem.CustomerCompanys;
using tibs.stem.NewCompanyContacts;
using Abp.Domain.Uow;
using tibs.stem.CompanyContacts.Exporting;
using tibs.stem.Dto;
using tibs.stem.Enquirys;
using tibs.stem.Authorization;
using Abp.Authorization;

namespace tibs.stem.NewCompanyContacts
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Company)]
    public class CompanyContactAppService : stemAppServiceBase, ICompanyContactAppService
    {
        private readonly IRepository<CustomerType> _NewCustomerTypeRepository;
        private readonly IRepository<Company> _NewCompanyRepository;
        private readonly IRepository<Contact> _NewContactRepository;
        private readonly IRepository<InfoType> _NewInfoTypeRepository;
        private readonly IRepository<AddressInfo> _NewAddressInfoRepository;
        private readonly IRepository<ContactInfo> _NewContactInfoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICompanyExcelExporter _companyExcelExporter;
        private readonly IContactExcelExporter _contactExcelExporter;
        private readonly IRepository<Enquiry> _EnquiryRepository;
        

        public CompanyContactAppService(
            ICompanyExcelExporter companyExcelExporter,
            IContactExcelExporter contactExcelExporter,
            IRepository<Enquiry> EnquiryRepository,
            IRepository<ContactInfo> NewContactInfoRepository, 
            IRepository<AddressInfo> NewAddressInfoRepository, 
            IRepository<InfoType> NewInfoTypeRepository,
            IRepository<Contact> NewContactRepository, 
            IRepository<CustomerType> NewCustomerTypeRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<Company> NewCompanyRepository)
        {
            _companyExcelExporter = companyExcelExporter;
            _contactExcelExporter = contactExcelExporter;
            _NewCustomerTypeRepository = NewCustomerTypeRepository;
            _NewCompanyRepository = NewCompanyRepository;
            _NewContactRepository = NewContactRepository;
            _NewInfoTypeRepository = NewInfoTypeRepository;
            _NewAddressInfoRepository = NewAddressInfoRepository;
            _NewContactInfoRepository = NewContactInfoRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _EnquiryRepository = EnquiryRepository;
        }
        public async Task<FileDto> GetContactToExcel()
        {

            var query = _NewContactRepository.GetAll();
            var contact = (from a in query
                           select new NewContactListDto
                           {
                               Id = a.Id,
                               ContactName = a.Name,
                               LastName = a.LastName,
                               Title = a.TitleOfCourtesies.Name,
                               CompanyName = a.Companys.Name,
                               CustomerTypeName = a.Companys.CustomerTypes.Title,
                               NewCustomerTypeId = a.CustomerTypeId,
                               ContactTypeName = a.CustomerTypes.Title
                           });

            var NewContactListDtos = contact.MapTo<List<NewContactListDto>>();

            return _contactExcelExporter.ExportToFile(NewContactListDtos);
        }
        public async Task<FileDto> GetCompanyToExcel()
        {

            var query = _NewCompanyRepository.GetAll();
            var companys = (from a in query
                            select new NewCompanyListDto
                            {
                                Id = a.Id,
                                CompanyName = a.Name,
                                CustomerTypeName = a.CustomerTypes.Title,
                                NewCustomerTypeId = a.CustomerTypeId,
                                AccountManagerId = a.AccountManagerId,
                                ManagedBy = a.AbpAccountManager.UserName,
                                CountryCode = a.CountryId != null ? a.Countrys.CountryName : "",
                                CurrencyCode = a.CurrencyId != null ? a.Currencys.Code : ""
                            });

            var NewCompanyListDtos = companys.MapTo<List<NewCompanyListDto>>();

            return _companyExcelExporter.ExportToFile(NewCompanyListDtos);
        }

        public async Task<PagedResultDto<NewCompanyListDto>> GetCompanys(GetCompanyInput input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                var query = _NewCompanyRepository.GetAll()
                  .WhereIf(
                  !input.Filter.IsNullOrEmpty(),
                  p => p.Name.Contains(input.Filter) ||
                      p.CustomerTypes.Title.Contains(input.Filter)
                  );
                var company = (from a in query
                               select new NewCompanyListDto
                               {
                                   Id = a.Id,
                                   CompanyName = a.Name,
                                   CustomerTypeName = a.CustomerTypes.Title,
                                   NewCustomerTypeId = a.CustomerTypeId,
                                   AccountManagerId = a.AccountManagerId,
                                   ManagedBy = a.AbpAccountManager.UserName,
                                   CountryCode = a.CountryId != null ? a.Countrys.CountryName : "",
                                   CurrencyCode = a.CurrencyId != null ? a.Currencys.Code : ""
                               });
                var companyCount = await company.CountAsync();
                var companylist = await company
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();

                try
                {

                }
                catch (Exception es)
                {

                }
                var companylistoutput = companylist.MapTo<List<NewCompanyListDto>>();

                return new PagedResultDto<NewCompanyListDto>(
                    companyCount, companylistoutput);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Company_Edit)]
        public async Task<Array> GetCompanyForEdit(NullableIdDto input)
        {
            var Addinfo = _NewAddressInfoRepository.GetAll().Where(p => p.CompanyId == input.Id);

            var contactinfo = _NewContactInfoRepository.GetAll().Where(p => p.CompanyId == input.Id);

            var SubListout = new List<GetNewCompany>();


            var comp = _NewCompanyRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

             if(comp != null)            
             {
                SubListout.Add(new GetNewCompany {
                    Id = comp.Id,
                    Name = comp.Name,
                    NewCustomerTypeId = comp.CustomerTypeId != null ? comp.CustomerTypeId : 0,
                    AccountManagerId = comp.AccountManagerId != null ? comp.AccountManagerId : 0,
                   // NewCustomerTypeName = comp.CustomerTypeId != null ? comp.CustomerTypes.Title : "",
                    CountryId = comp.CountryId != null ? comp.CountryId :0,
                 //   CountryName = comp.CountryId != null ? comp.Countrys.CountryName : "",
                    CurrencyId = comp.CurrencyId != null ? comp.CurrencyId : 0,
                  //  CurrencyCode = comp.CurrencyId != null ? comp.Currencys.Code : "",
                    AddressInfo = (from r in Addinfo
                                   select new CreateAddressInfo
                                   {
                                       Id = r.Id,
                                       CompanyId = r.CompanyId != null ? r.CompanyId : 0,
                                       ContacId = r.ContacId != null ? r.ContacId : 0,
                                       InfoTypeId = r.InfoTypeId,
                                       Address1 = r.Address1,
                                       Address2 = r.Address2,
                                   }).ToArray(),

                    Contactinfo = (from r in contactinfo
                                   select new CreateContactInfo
                                   {
                                       Id = r.Id,
                                       CompanyId = r.CompanyId != null ? r.CompanyId : 0,
                                       ContacId = r.ContacId != null ? r.ContacId : 0,
                                       InfoTypeId = r.InfoTypeId,
                                       InfoData = r.InfoData
                                   }).ToArray()
                });

            }

            return SubListout.ToArray();
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Company_Create)]
        public async Task<int> CreateOrUpdateCompanyOrContact(CreateCompanyOrContact input)
        {
            var id = 0;

            if (input.Id == 0 && input.CompanyId != null)
            {
                id = await CreateContact(input);
            }
            else if (input.Id == 0 && input.CompanyId == null)
            {
                id = await CreateCompany(input);
            }
            else if (input.Id != 0 && input.CompanyId != null)
            {
                id = await UpdateContact(input);

            }
            else if (input.Id != 0 && input.CompanyId == null)
            {
                id = await UpdateCompany(input);

            }

            return id;
        }
        public virtual async Task<int> CreateCompany(CreateCompanyOrContact input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            var id = 0;
            var data = _NewCompanyRepository.GetAll().Where(p => p.Name == input.Name && p.CustomerTypeId == input.CustomerTypeId && p.TenantId == TenantId).ToList();
            if (data.Count == 0)
            {
                CreateCompany company = new CreateCompany();
                company.Name = input.Name;
                company.CustomerTypeId = input.CustomerTypeId;
                company.AccountManagerId = input.AccountManagerId;
                company.CountryId = input.CountryId;
                company.CurrencyId = input.CurrencyId;
                company.TenantId = TenantId;
                var companys = company.MapTo<Company>();
                id = _NewCompanyRepository.InsertAndGetId(companys);
            }
            else
            {
                id = data[0].Id;
            }
            return id;
        }
        public virtual async Task<int> UpdateCompany(CreateCompanyOrContact input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            var data = _NewCompanyRepository.GetAll().Where(p => p.Name == input.Name && p.CustomerTypeId == input.CustomerTypeId && p.TenantId == TenantId && p.Id != input.Id).ToList();
            if (data.Count == 0)
            {
                CreateCompany company = new CreateCompany();
                company.Id = input.Id;
                company.Name = input.Name;
                company.CustomerTypeId = input.CustomerTypeId;
                company.AccountManagerId = input.AccountManagerId;
                company.CountryId = input.CountryId;
                company.CurrencyId = input.CurrencyId;
                company.TenantId = TenantId;
                var companys = await _NewCompanyRepository.GetAsync(input.Id);
                ObjectMapper.Map(company, companys);
                await _NewCompanyRepository.UpdateAsync(companys);
            }
            return input.Id;
        }
        public virtual async Task<int> CreateContact(CreateCompanyOrContact input)
        {
            var id = 0;
            int TenantId = (int)(AbpSession.TenantId);
            CreateContact contact = new CreateContact();
            contact.Name = input.Name;
            contact.CustomerTypeId = input.CustomerTypeId;
            contact.CompanyId = input.CompanyId;
            contact.LastName = input.LastName;
            contact.TitleId = input.TitleId;
            contact.TenantId = TenantId;
            var contacts = contact.MapTo<Contact>();
            id = _NewContactRepository.InsertAndGetId(contacts);
            return id;

        }
        public virtual async Task<int> UpdateContact(CreateCompanyOrContact input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            CreateContact contact = new CreateContact();
            contact.Id = input.Id;
            contact.Name = input.Name;
            contact.CustomerTypeId = input.CustomerTypeId;
            contact.CompanyId = input.CompanyId;
            contact.LastName = input.LastName;
            contact.TitleId = input.TitleId;
            contact.TenantId = TenantId;
            var contacts = await _NewContactRepository.GetAsync(input.Id);
            ObjectMapper.Map(contact, contacts);
            await _NewContactRepository.UpdateAsync(contacts);
            return input.Id;

        }
        public async Task CreateOrUpdateAddressInfo(CreateAddressInfo input)
        {
            if (input.Id == 0)
            {
                await CreateAddressInfo(input);
            }
            else
            {
                await UpdateAddressInfo(input);

            }
        }

        public virtual async Task CreateAddressInfo(CreateAddressInfo input)
        {
            var AddressInfos = input.MapTo<AddressInfo>();
            await _NewAddressInfoRepository.InsertAsync(AddressInfos);
        }
        public virtual async Task UpdateAddressInfo(CreateAddressInfo input)
        {
            var AddressInfo = await _NewAddressInfoRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, AddressInfo);
            await _NewAddressInfoRepository.UpdateAsync(AddressInfo);
        }

        public async Task CreateOrUpdateContactInfo(CreateContactInfo input)
        {
            if (input.Id == 0)
            {
                await CreateContactInfo(input);
            }
            else
            {
                await UpdateContactInfo(input);

            }
        }

        public virtual async Task CreateContactInfo(CreateContactInfo input)
        {
            var ContactInfos = input.MapTo<ContactInfo>();
            await _NewContactInfoRepository.InsertAsync(ContactInfos);
        }

        public virtual async Task UpdateContactInfo(CreateContactInfo input)
        {
            var ContactInfo = await _NewContactInfoRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, ContactInfo);
            await _NewContactInfoRepository.UpdateAsync(ContactInfo);
        }

        public async Task<GetNewAddressInfo> GetAddressInfoForEdit(NullableIdDto input)
        {
            var output = new GetNewAddressInfo { };
            var AddressInfo = _NewAddressInfoRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.CreateAddressInfos = AddressInfo.MapTo<CreateAddressInfo>();
            return output;
        }
        public async Task<GetNewContactInfo> GetContactInfoForEdit(NullableIdDto input)
        {
            var output = new GetNewContactInfo { };
            var ContactInfo = _NewContactInfoRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.CreateContactInfos = ContactInfo.MapTo<CreateContactInfo>();
            return output;
        }

        public async Task GetDeleteAddressInfo(EntityDto input)
        {
            await _NewAddressInfoRepository.DeleteAsync(input.Id);
        }

        public async Task GetDeleteContactInfo(EntityDto input)
        {
            await _NewContactInfoRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Company_Delete)]
        public async Task GetDeleteCompany(EntityDto input)
        {
            int Company = (from r in _NewContactRepository.GetAll() where r.CompanyId == input.Id select r).Count();
            if (Company <= 0)
            {
                await _NewCompanyRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Company cannot be deleted");
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Contact_Delete)]
        public async Task GetDeleteContact(EntityDto input)
        {
            int Contact = (from r in _EnquiryRepository.GetAll() where r.ContactId == input.Id select r).Count();
            if (Contact <= 0)
            {
                await _NewContactRepository.DeleteAsync(input.Id);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Contact cannot be deleted");
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Contact_Edit)]
        public async Task<Array> GetContactForEdit(NullableIdDto input)
        {
            var Addinfo = _NewAddressInfoRepository.GetAll().Where(p => p.ContacId == input.Id);

            var contactinfo = _NewContactInfoRepository.GetAll().Where(p => p.ContacId == input.Id);

            var SubListout = new List<GetNewContacts>();

            var comp = _NewContactRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            if (comp != null)
            {
                SubListout.Add(new GetNewContacts
                {
                    Id = comp.Id,
                    Name = comp.Name,
                    NewCustomerTypeId = comp.CustomerTypeId,
                    CompanyId = comp.CompanyId != null ? comp.CompanyId : 0,
                    TitleId = comp.TitleId,
                    LastName = comp.LastName,

                    AddressInfo = (from r in Addinfo
                                   select new CreateAddressInfo
                                   {
                                       Id = r.Id,
                                       CompanyId = r.CompanyId != null ? r.CompanyId : 0,
                                       ContacId = r.ContacId != null ? r.ContacId : 0,
                                       InfoTypeId = r.InfoTypeId,
                                       Address1 = r.Address1,
                                       Address2 = r.Address2
                                   }).ToArray(),

                    Contactinfo = (from r in contactinfo
                                   select new CreateContactInfo
                                   {
                                       Id = r.Id,
                                       CompanyId = r.CompanyId != null ? r.CompanyId : 0,
                                       ContacId = r.ContacId != null ? r.ContacId : 0,
                                       InfoTypeId = r.InfoTypeId,
                                       InfoData = r.InfoData
                                   }).ToArray()
                });

            }

            return SubListout.ToArray();
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Contact)]
        public async Task<PagedResultDto<NewContactListDto>> GetContacts(GetContactInput input)
        {
            var query = _NewContactRepository.GetAll().Where(c => c.CompanyId != null)
                          .WhereIf(
                          !input.Filter.IsNullOrEmpty(),
                          p => p.Name.Contains(input.Filter) ||
                              p.CustomerTypes.Title.Contains(input.Filter)
                          );
            var contact = (from a in query
                           select new NewContactListDto
                           {
                               Id = a.Id,
                               ContactName = a.Name,
                               LastName = a.LastName,
                               Title = a.TitleOfCourtesies.Name,
                               CompanyName = a.Companys.Name,
                               CustomerTypeName = a.Companys.CustomerTypes.Title,
                               NewCustomerTypeId = a.CustomerTypeId,
                               ContactTypeName = a.CustomerTypes.Title
                           });
            var contactCount = await contact.CountAsync();
            var contactlist = await contact
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var contactlistoutput = contactlist.MapTo<List<NewContactListDto>>();
            return new PagedResultDto<NewContactListDto>(
                contactCount, contactlistoutput);
        }

        public ListResultDto<NewContactListDto> GetCompanyContacts(NullableIdDto input)
        {
            var contact = _NewContactRepository
               .GetAll().Where(p => p.CompanyId == input.Id);

            var data = (from a in contact
                        select new NewContactListDto
                        {
                            Id = a.Id,
                            ContactName = a.Name,
                            LastName = a.LastName,
                            Title = a.TitleOfCourtesies.Name,
                            CompanyName = a.Companys.Name,
                            CustomerTypeName = a.Companys.CustomerTypes.Title,
                            NewCustomerTypeId = a.CustomerTypeId,
                            ContactTypeName = a.CustomerTypes.Title
                        });

            return new ListResultDto<NewContactListDto>(data.MapTo<List<NewContactListDto>>());
        }


        //public virtual async Task<int> ContactUpdate(CreateCompanyOrContact input)
        //{
        //    CreateContact contact = new CreateContact();
        //    contact.Id = input.Id;
        //    contact.Name = input.Name;
        //    contact.NewCustomerTypeId = input.NewCustomerTypeId;
        //    contact.NewCompanyId = input.NewCompanyId;
        //    contact.LastName = input.LastName;
        //    contact.TitleId = input.TitleId;
        //    contact.IndustryId = input.IndustryId;
        //    var contacts = await _NewContactRepository.GetAsync(input.Id);
        //    ObjectMapper.Map(contact, contacts);
        //    await _NewContactRepository.UpdateAsync(contacts);
        //    return input.Id;

        //}


    }
}
