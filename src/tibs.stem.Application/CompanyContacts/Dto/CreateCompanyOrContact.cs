using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.CustomerCompanys;

namespace tibs.stem.NewCompanyContacts.Dto
{
    public class CreateCompanyOrContact
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? CustomerTypeId { get; set; }
        public virtual int? CompanyId { get; set; }
        public virtual long? AccountManagerId { get; set; }
        public virtual int? TitleId { get; set; }
        public virtual string LastName { get; set; }
        public virtual int? CurrencyId { get; set; }
        public virtual int? CountryId { get; set; }
    }

    [AutoMap(typeof(Company))]
    public class CreateCompany
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? CustomerTypeId { get; set; }
        public virtual long? AccountManagerId { get; set; }
        public virtual int? CountryId { get; set; }
        public virtual int? CurrencyId { get; set; }
        public int TenantId { get; set; }
    }

    [AutoMapFrom(typeof(Company))]
    public class NewCompanyListDto
    {
        public virtual int Id { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual int? NewCustomerTypeId { get; set; }
        public virtual string CustomerTypeName { get; set; }
        public virtual long? AccountManagerId { get; set; }
        public virtual string ManagedBy { get; set; }
        public virtual int? CurrencyId { get; set; }
        public virtual string CurrencyCode { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual int? CountryId { get; set; }

    }

    [AutoMap(typeof(Contact))]
    public class CreateContact
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? CustomerTypeId { get; set; }
        public virtual int? CompanyId { get; set; }
        public virtual int? TitleId { get; set; }
        public virtual string LastName { get; set; }
        public int TenantId { get; set; }

    }


    [AutoMapFrom(typeof(Contact))]
    public class NewContactListDto
    {
        public virtual int Id { get; set; }
        public virtual string ContactName { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual int? NewCustomerTypeId { get; set; }
        public virtual string CustomerTypeName { get; set; }
        public virtual string ContactTypeName { get; set; }
        public virtual int? TitleId { get; set; }
        public virtual string Title { get; set; }
        public virtual string LastName { get; set; }
        public string IndustryName { get; set; }
    }

    [AutoMapTo(typeof(AddressInfo))]
    public class CreateAddressInfo
    {
        public virtual int Id { get; set; }
        public virtual int? CompanyId { get; set; }
        public virtual int? ContacId { get; set; }
        public virtual int? InfoTypeId { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }

    }

    [AutoMapTo(typeof(ContactInfo))]
    public class CreateContactInfo
    {
        public virtual int Id { get; set; }
        public virtual int? CompanyId { get; set; }
        public virtual int? ContacId { get; set; }
        public virtual int? InfoTypeId { get; set; }
        public virtual string InfoData { get; set; }
    }

    public class GetNewContact
    {
        public CreateContact Contact { get; set; }
    }
    public class GetNewAddressInfo
    {
        public CreateAddressInfo CreateAddressInfos { get; set; }
    }
    public class GetNewContactInfo
    {
        public CreateContactInfo CreateContactInfos { get; set; }
    }
}
