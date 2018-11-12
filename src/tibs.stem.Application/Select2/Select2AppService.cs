using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.AutoMapper;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using Abp.Authorization.Users;
using tibs.stem.Countrys;
using tibs.stem.Currencies;
using tibs.stem.CustomerCompanys;
using tibs.stem.MileStones;
using tibs.stem.MileStoneStatuss;
using tibs.stem.MileStoneStatusDetails;
using tibs.stem.ActivityTypess;
using tibs.stem.ProductGroups;
using tibs.stem.ProductSubGroups;
using tibs.stem.TitleOfCourtes;
using tibs.stem.TenantTypes;
using tibs.stem.TitleOfQuotations;
using tibs.stem.QuotationStatuss;
using tibs.stem.Freights;
using tibs.stem.QPayments;
using tibs.stem.Packings;
using tibs.stem.Warrantys;
using tibs.stem.Validitys;
using tibs.stem.Deliverys;
using tibs.stem.Reasonss;
using tibs.stem.Products;
using tibs.stem.Services;
using tibs.stem.PriceLevels;
using tibs.stem.PriceLevelProducts;
using tibs.stem.Authorization.Roles;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using System;
using tibs.stem.TargetTypess;
using Abp.Authorization;
using tibs.stem.Enquirys;
using tibs.stem.Payments;
using tibs.stem.PaymentSchedules;
using tibs.stem.QuotationProducts;
using tibs.stem.PaymentCollections;
using tibs.stem.Quotations;

namespace tibs.stem.Select2
{
    public class Select2AppService : stemAppServiceBase, ISelect2AppService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IRepository<InfoType> _InfoTypeRepositary;
        private readonly IRepository<CustomerType> _CustomerTypeRepositary;
        private readonly IRepository<Company> _NewCompanyRepository;
        private readonly IRepository<Contact> _NewContactRepository;
        public  readonly IRepository<MileStone> _MilestoneRepository;
        private readonly IRepository<MileStoneStatus> _MileStoneStatusRepository;
        private readonly IRepository<MileStoneStatusDetail> _MileStoneStatusDetailRepository;
        private readonly IRepository<ActivityType> _ActivityTypeRepositary;
        private readonly IRepository<ProductGroup> _ProductGroupRepository;
        private readonly IRepository<ProductSubGroup> _ProductSubGroupRepository;
        private readonly IRepository<TitleOfCourtesy> _TitleRepository;
        private readonly IRepository<TenantType> _TenantTypeRepositary;
        private readonly IRepository<TitleOfQuotation> _TitleOfQuotationRepository;
        private readonly IRepository<QuotationStatus> _QuotationStatusRepository;
        private readonly IRepository<Freight> _FreightRepository;
        private readonly IRepository<QPayment> _QPaymentRepository;
        private readonly IRepository<Packing> _PackingRepository;
        private readonly IRepository<Warranty> _WarrantyRepository;
        private readonly IRepository<Validity> _ValidityRepository;
        private readonly IRepository<Delivery> _DeliveryRepository;
        private readonly IRepository<Reason> _ReasonRepository;
        private readonly IRepository<PriceLevel> _PriceLevelRepository;
        private readonly IRepository<PriceLevelProduct> _PriceLevelProductRepository;
        private readonly IRepository<Product> _ProductRepository;
        private readonly IRepository<Service> _ServiceRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<Role> _RoleRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<TargetTypes> _TargetTypeRepository;
        private readonly IRepository<Enquiry> _EnquiryRepository;
        private readonly IRepository<Payment> _PaymentRepository;
        private readonly IRepository<QuotationProduct> _QuotationProductRepository;
        private readonly IRepository<PaymentSchedule> _PaymentScheduleRepository;
        private readonly IRepository<PaymentCollection> _PaymentCollectionRepository;
        private readonly IRepository<Quotation> _QuotationRepository;

        public Select2AppService(
            IRepository<PriceLevel> PriceLevelRepository,
            IRepository<Payment> PaymentRepository,
            IRepository<Country> countryRepository,
            IRepository<PaymentCollection> PaymentCollectionRepository,
            IRepository<Currency> currencyRepository,
            IRepository<InfoType> InfoTypeRepositary,
            IRepository<Quotation> QuotationRepository,
            IRepository<QuotationProduct> QuotationProductRepository,
            IRepository<PaymentSchedule> PaymentScheduleRepository,
            IRepository<CustomerType> CustomerTypeRepositary,
            IRepository<Company> NewCompanyRepository,
            IRepository<Contact> NewContactRepository,
            IRepository<MileStone> MilestoneRepository,
            IRepository<MileStoneStatus> MileStoneStatusRepository,
            IRepository<MileStoneStatusDetail> MileStoneStatusDetailRepository,
            IRepository<ActivityType> ActivityTypeRepositary,
            IRepository<ProductGroup> ProductGroupRepository,
            IRepository<ProductSubGroup> ProductSubGroupRepository,
            IRepository<TitleOfCourtesy> TitleRepository,
            IRepository<TenantType> TenantTypeRepositary,
            IRepository<TitleOfQuotation> TitleOfQuotationRepository,
            IRepository<QuotationStatus> QuotationStatusRepository,
            IRepository<Freight> FreightRepository,
            IRepository<QPayment> QPaymentRepository,
            IRepository<Packing> PackingRepository,
            IRepository<Warranty> WarrantyRepository,
            IRepository<Validity> ValidityRepository,
            IRepository<Delivery> DeliveryRepository,
            IRepository<Reason> ReasonRepository,
            IRepository<Product> ProductRepository,
            IRepository<Service> ServiceRepository,
            IRepository<PriceLevelProduct> PriceLevelProductRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> RoleRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<Enquiry> EnquiryRepository,
            IAbpSession session,
            IRepository<TargetTypes> TargetTypeRepository
            )
        {
            _PriceLevelRepository = PriceLevelRepository;
            _countryRepository = countryRepository;
            _PaymentRepository = PaymentRepository;
            _currencyRepository = currencyRepository;
            _QuotationRepository = QuotationRepository;
            _InfoTypeRepositary = InfoTypeRepositary;
            _CustomerTypeRepositary = CustomerTypeRepositary;
            _NewCompanyRepository = NewCompanyRepository;
            _NewContactRepository = NewContactRepository;
            _MilestoneRepository = MilestoneRepository;
            _MileStoneStatusRepository = MileStoneStatusRepository;
            _MileStoneStatusDetailRepository = MileStoneStatusDetailRepository;
            _ActivityTypeRepositary = ActivityTypeRepositary;
            _ProductGroupRepository = ProductGroupRepository;
            _ProductSubGroupRepository = ProductSubGroupRepository;
            _TitleRepository = TitleRepository;
            _TenantTypeRepositary = TenantTypeRepositary;
            _TitleOfQuotationRepository = TitleOfQuotationRepository;
            _QuotationStatusRepository = QuotationStatusRepository;
            _FreightRepository = FreightRepository;
            _QPaymentRepository = QPaymentRepository;
            _PackingRepository = PackingRepository;
            _WarrantyRepository = WarrantyRepository;
            _ValidityRepository = ValidityRepository;
            _DeliveryRepository = DeliveryRepository;
            _ReasonRepository = ReasonRepository;
            _PriceLevelProductRepository = PriceLevelProductRepository;
            _ProductRepository = ProductRepository;
            _ServiceRepository = ServiceRepository;
            _userRoleRepository = userRoleRepository;
            _RoleRepository = RoleRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _TargetTypeRepository = TargetTypeRepository;
            _EnquiryRepository = EnquiryRepository;
            _QuotationProductRepository = QuotationProductRepository;
            _PaymentScheduleRepository = PaymentScheduleRepository;
            _PaymentCollectionRepository = PaymentCollectionRepository;
        }

        public async Task<Select4Result> GetproductPriceLevel(Select2ResultInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select4Result sr = new Select4Result();
                var contact = (from c in _PriceLevelProductRepository.GetAll() where c.ProductId == input.Id select c).ToArray();
                if (contact.Length > 0)
                {
                    var contactarray = (from c in contact select new datadto4 { Id = c.Id, Name = c.PriceLevels.PriceLevelName, Price = c.Price }).ToArray();
                    sr.select4data = contactarray;
                }
                return sr;
            }
        }
        public async Task<Select3Result> GetCurrency()
        {
            Select3Result sr = new Select3Result();
            var currency = (from c in _currencyRepository.GetAll() select c).ToArray();
            if (currency.Length > 0)
            {
                var currencyarray = (from c in currency select new datadto3 { Id = c.Id, Name = c.Name, Code = c.Code }).ToArray();
                sr.select3data = currencyarray;
            }

            return sr; 

        }
        public async Task<Select2Result> GetPriceLevel()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var src = (from c in _PriceLevelRepository.GetAll() where c.IsDeleted == false select c).ToArray();

                if (src.Length > 0)
                {
                    var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.PriceLevelName }).ToArray();
                    sr.select2data = srcarray;
                }
                return sr;
            }
        }
        public async Task<Select2Result> GetTargetType()
        {
            Select2Result sr = new Select2Result();
            var src = (from c in _TargetTypeRepository.GetAll() select c).ToArray();

            if (src.Length > 0)
            {
                var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }
        public async Task<Select2Result> GetActivityType()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {

                Select2Result sr = new Select2Result();
                var src = (from c in _ActivityTypeRepositary.GetAll() where c.IsDeleted == false select c).ToArray();

                if (src.Length > 0)
                {
                    var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                    sr.select2data = srcarray;
                }
                return sr;
            }
        }
        public async Task<Select2Company> GetCompany()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Company sr = new Select2Company();
                var company = (from c in _NewCompanyRepository.GetAll() select c).ToArray();
                if (company.Length > 0)
                {
                    try
                    {
                        var companyarray = (from c in company
                                            select new Companydto
                                            {
                                                Id = c.Id,
                                                Name = c.Name,
                                                SalesId = c.AccountManagerId != null ? c.AccountManagerId : 0,
                                                SalesName = ""
                                            }).ToArray();
                        foreach (var lis in companyarray)
                        {
                            if (lis.SalesId > 0)
                            {
                                var user = (from c in UserManager.Users where c.Id == lis.SalesId select c).FirstOrDefault();
                                lis.SalesName = user.UserName;
                            }

                        }
                        sr.select2data = companyarray;
                    }
                    catch (Exception rx)
                    {

                    }

                }
                return sr;
            }
        }
        public async Task<Select2Result> GetSalesman()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                {
                    var Account = (from c in UserManager.Users
                                   join userrole in _userRoleRepository.GetAll() on c.Id equals userrole.UserId
                                   join role in _RoleRepository.GetAll() on userrole.RoleId equals role.Id
                                   where role.DisplayName == "Sales Executive"
                                   select c).ToArray();

                    var Accounts = (from c in Account select new datadto { Id = (int)c.Id, Name = c.UserName }).ToArray();
                    sr.select2data = Accounts;
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetMilestone()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {

                Select2Result sr = new Select2Result();
                var milestone = (from c in _MilestoneRepository.GetAll() select c).ToArray();
                if (milestone.Length > 0)
                {
                    var milestonearray = (from c in milestone select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                    sr.select2data = milestonearray;
                }
                return sr;
            }
        }
        public Select2Result GetEnqMilestone()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {

                Select2Result sr = new Select2Result();
                var milestone = (from c in _MilestoneRepository.GetAll() where c.IsQuotation == false select c).ToArray();
                if (milestone.Length > 0)
                {
                    var milestonearray = (from c in milestone select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                    sr.select2data = milestonearray;
                }
                return sr;
            }
        }
        public async Task<Select2Result> GetCompanyContact(Select2ResultInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {

                Select2Result sr = new Select2Result();
                var contact = (from c in _NewContactRepository.GetAll() where c.CompanyId == input.Id select c).ToArray();
                if (contact.Length > 0)
                {
                    var contactarray = (from c in contact select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                    sr.select2data = contactarray;
                }
                return sr;
            }
        }
        public async Task<Select2Result> GetMileStoneMileStatus(Select2ResultInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {

                Select2Result sr = new Select2Result();
                var milestonestatus = (from c in _MileStoneStatusDetailRepository.GetAll() where c.MileStoneId == input.Id select c);
                if (milestonestatus.Count() > 0)
                {
                    var milestonestatusarray = (from c in milestonestatus select new datadto { Id = c.MileStoneStatusId }).ToArray();
                    foreach (var lis in milestonestatusarray)
                    {
                        lis.Name = (from r in _MileStoneStatusRepository.GetAll() where r.Id == lis.Id select r.Name).FirstOrDefault();
                    }
                    sr.select2data = milestonestatusarray;
                }
                return sr;
            }
        }
        public async Task<Select2Result> GetCountry()
        {
            Select2Result sr = new Select2Result();
            var country = (from c in _countryRepository.GetAll() select c).ToArray();
            if (country.Length > 0)
            {
                var countryarray = (from c in country select new datadto { Id = c.Id, Name = c.CountryName }).ToArray();
                sr.select2data = countryarray;
            }
            return sr;
        }
        public async Task<Select2Result> GetContactInfo()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {

                Select2Result sr = new Select2Result();
                var src = (from c in _InfoTypeRepositary.GetAll() where c.Info == true select c).ToArray();

                if (src.Length > 0)
                {
                    var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.ContactName }).ToArray();
                    sr.select2data = srcarray;
                }
                return sr;
            }
        }
        public async Task<Select2Result> GetAddressInfo()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var src = (from c in _InfoTypeRepositary.GetAll() where c.Info == false select c).ToArray();

                if (src.Length > 0)
                {
                    var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.ContactName }).ToArray();
                    sr.select2data = srcarray;
                }
                return sr;
            }
        }
        public async Task<Select2Result> GetCompetitor()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();

                var company = (from c in _NewCompanyRepository.GetAll()
                               join r in _CustomerTypeRepositary.GetAll() on c.CustomerTypeId equals r.Id
                               where r.Title == "Competitor"
                               select c).ToArray();

                if (company.Length > 0)
                {
                    var srcarray = (from c in company select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                    sr.select2data = srcarray;
                }
                return sr;
            }
        }

        public async Task<Select2Result> GeCompanyType()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var src = (from c in _CustomerTypeRepositary.GetAll() where c.Company == true select c).ToArray();

                if (src.Length > 0)
                {
                    var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.Title }).ToArray();
                    sr.select2data = srcarray;
                }
                return sr;
            }
        }
        public async Task<Select2Result> GetContactType()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var src = (from c in _CustomerTypeRepositary.GetAll() where c.Company == false select c).ToArray();
                if (src.Length > 0)
                {
                    var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.Title }).ToArray();
                    sr.select2data = srcarray;
                }
                return sr;
            }
        }
        public async Task<Select2Result> GetProductGroup()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var productGroup = (from c in _ProductGroupRepository.GetAll() select c).ToArray();
                if (productGroup.Length > 0)
                {
                    var productGrouparray = (from c in productGroup select new datadto { Id = c.Id, Name = c.ProductGroupName }).ToArray();
                    sr.select2data = productGrouparray;
                }
                return sr;
            }
        }
        public async Task<Select2Result> GetProductSubGroup()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var productSubGroup = (from c in _ProductSubGroupRepository.GetAll() select c).ToArray();
                if (productSubGroup.Length > 0)
                {
                    var productSubGrouparray = (from c in productSubGroup select new datadto { Id = c.Id, Name = c.ProductSubGroupName }).ToArray();
                    sr.select2data = productSubGrouparray;
                }
                return sr;
            }
        }
        public async Task<Select2Result> GetTitle()
        {
            Select2Result sr = new Select2Result();
            var title = (from c in _TitleRepository.GetAll() select c).ToArray();

            if (title.Length > 0)
            {
                var titleDtos = (from c in title select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = titleDtos;
            }

            return sr;
        }
        public async Task<Select2Result> GetTenantType()
        {
            Select2Result sr = new Select2Result();
            var src = (from c in _TenantTypeRepositary.GetAll() where c.IsDeleted == false select c).ToArray();
            if (src.Length > 0)
            {
                var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }
        public async Task<Select2Result> GetQuotationTitle()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var quotationtitle = (from c in _TitleOfQuotationRepository.GetAll() select c).ToArray();

                if (quotationtitle.Length > 0)
                {
                    var quotationtitleDtos = (from c in quotationtitle select new datadto { Id = c.Id, Name = c.Code }).ToArray();
                    sr.select2data = quotationtitleDtos;
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetStatus()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var status = (from c in _QuotationStatusRepository.GetAll() select c).ToArray();

                if (status.Length > 0)
                {
                    var statusDtos = (from c in status select new datadto { Id = c.Id, Name = c.QuotationStatusName }).ToArray();
                    sr.select2data = statusDtos;
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetFreight()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var freight = (from c in _FreightRepository.GetAll() select c).ToArray();

                if (freight.Length > 0)
                {
                    var freightDtos = (from c in freight select new datadto { Id = c.Id, Name = c.FreightName }).ToArray();
                    sr.select2data = freightDtos;
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetPayment()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var payment = (from c in _QPaymentRepository.GetAll() select c).ToArray();

                if (payment.Length > 0)
                {
                    var paymentDtos = (from c in payment select new datadto { Id = c.Id, Name = c.PaymentName }).ToArray();
                    sr.select2data = paymentDtos;
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetPacking()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var packing = (from c in _PackingRepository.GetAll() select c).ToArray();

                if (packing.Length > 0)
                {
                    var packingDtos = (from c in packing select new datadto { Id = c.Id, Name = c.PackingName }).ToArray();
                    sr.select2data = packingDtos;
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetWarranty()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var warranty = (from c in _WarrantyRepository.GetAll() select c).ToArray();

                if (warranty.Length > 0)
                {
                    var warrantyDtos = (from c in warranty select new datadto { Id = c.Id, Name = c.WarrantyName }).ToArray();
                    sr.select2data = warrantyDtos;
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetValidity()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var validity = (from c in _ValidityRepository.GetAll() select c).ToArray();

                if (validity.Length > 0)
                {
                    var validityDtos = (from c in validity select new datadto { Id = c.Id, Name = c.ValidityName }).ToArray();
                    sr.select2data = validityDtos;
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetDelivery()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var delivery = (from c in _DeliveryRepository.GetAll() select c).ToArray();

                if (delivery.Length > 0)
                {
                    var deliveryDtos = (from c in delivery select new datadto { Id = c.Id, Name = c.DeliveryName }).ToArray();
                    sr.select2data = deliveryDtos;
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetReason()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var reason = (from c in _ReasonRepository.GetAll() select c).ToArray();

                if (reason.Length > 0)
                {
                    var reasonDtos = (from c in reason select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                    sr.select2data = reasonDtos;
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetProduct()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var product = (from c in _ProductRepository.GetAll() select c).ToArray();

                if (product.Length > 0)
                {
                    var productDtos = (from c in product select new datadto { Id = c.Id, Name = c.ProductName }).ToArray();
                    sr.select2data = productDtos;
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetService()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var service = (from c in _ServiceRepository.GetAll() select c).ToArray();

                if (service.Length > 0)
                {
                    var serviceDtos = (from c in service select new datadto { Id = c.Id, Name = c.ServiceCode }).ToArray();
                    sr.select2data = serviceDtos;
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetSalesPerson()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                {
                    string SliderQuery = "SELECT * FROM [dbo].[View_SalesPersonSlider] where TenantId = " + _session.TenantId;

                    DataTable viewtable = new DataTable();
                    ConnectionAppService db = new ConnectionAppService();
                    SqlConnection con = new SqlConnection(db.ConnectionString());
                    con.Open();
                    SqlCommand cmd = new SqlCommand(SliderQuery, con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(viewtable);
                    }
                    con.Close();
                    var Account = (from DataRow dr in viewtable.Rows
                                   select new datadto
                                   {
                                       Id = Convert.ToInt32(dr["Id"]),
                                       Name = Convert.ToString(dr["Name"])
                                   });

                    sr.select2data = Account.ToArray();
                }

                return sr;
            }
        }
        public async Task<Select2Result> GetEnquiry(string Input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var inquiry = (from c in _EnquiryRepository.GetAll().Where(t => t.Title.Contains(Input)) select c).ToArray();

                if (inquiry.Length > 0)
                {
                    var productDtos = (from c in inquiry select new datadto { Id = c.Id, Name = c.Title }).ToArray();
                    sr.select2data = productDtos;
                }

                return sr;
            }
        }
        public async Task<Select5Result> GetEnquiryDetail(NullableIdDto input)
        {
            var output = new Select5Result();
            var query = _EnquiryRepository.GetAll().Where(p => p.Id == input.Id);
            var enquiry = (from a in query
                           select new datadto5
                           {
                               Id = a.Id,
                               Name = a.Title,
                               CompanyId = a.CompanyId,
                               CompanyName = a.Companys.Name,
                               ContactId = a.ContactId ?? 0,
                               ContactName = a.ContactId != null ? a.Contacts.Name + " " + a.Contacts.LastName : "",
                               SalesId = (int)(a.Companys.AccountManagerId ?? 0),
                               SalesName = a.Companys.AbpAccountManager.UserName ?? "",
                               CurrencyId = a.Companys.Currencys.Id > 0 ? a.Companys.Currencys.Id : 0,
                               CurrencyName = a.Companys.Currencys.Code ?? ""

                           }).FirstOrDefault();

            output = new Select5Result
            {
                select2data = enquiry,
            };

            return output;
        }
        public async Task<Select2Result> GetPayments()
        {
            Select2Result sr = new Select2Result();
            var Payments = (from c in _PaymentRepository.GetAll() select c).ToArray();
            if (Payments.Length > 0)
            {
                var PaymentsArray = (from c in Payments select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = PaymentsArray;
            }
            return sr;
        }
        public async Task<Select6Result> GetDueAmount(NullableIdDto input)
        {
            var output = new Select6Result();


            var Total = (from c in _QuotationRepository.GetAll() where c.Id == input.Id select c.Vat == true ? Math.Round((c.ExchangeRate * (c.Total - c.OverallDiscountinUSD + c.VatAmount)), 2) : Math.Round((c.ExchangeRate * (c.Total - c.OverallDiscountinUSD)), 2)).FirstOrDefault();
            var Collction = (from c in _PaymentCollectionRepository.GetAll() where c.QuotationId == input.Id && c.Received == true select c.Amount).Sum();

            if (Collction > 0)
            {
                Collction = Total - Collction;
            } else
            {
                Collction = Total;
            }

            Select6ResultInput sr = new Select6ResultInput
            {
                TotalAmount = Total,
                DueAmount = Collction
            };

            output = new Select6Result
            {
                select6data = sr
            };

            return output;
        }
        public async Task<Select2Result> GetDashboardSelect()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select2Result sr = new Select2Result();
                var selectArray = new List<datadto>();
                if (PermissionChecker.IsGranted("Pages.Tenant.Dashboard"))
                {
                    selectArray.Add(new datadto { Id = 1, Name = "Default" });
                }
                if (PermissionChecker.IsGranted("Pages.Tenant.Dashboard.SalesDashboard"))
                {
                    selectArray.Add(new datadto { Id = 2, Name = "Sales Dashboard" });
                }
                if (PermissionChecker.IsGranted("Pages.Tenant.Leads.Leads"))
                {
                    selectArray.Add(new datadto { Id = 3, Name = "Lead Kanban" });
                }
                if (PermissionChecker.IsGranted("Pages.Tenant.Leads.Leads.Gridview"))
                {
                    selectArray.Add(new datadto { Id = 4, Name = "Lead Grid" });
                }
                if (PermissionChecker.IsGranted("Pages.Tenant.Quotation.Quotations"))
                {
                    selectArray.Add(new datadto { Id = 5, Name = "Quotation Grid" });
                }

                sr.select2data = selectArray.ToArray();

                return sr;
            }
        }
        public async Task<Select2Result> GetQuotationMilestone()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {

                Select2Result sr = new Select2Result();
                var milestone = (from c in _MilestoneRepository.GetAll() where c.IsQuotation == true select c).ToArray();
                if (milestone.Length > 0)
                {
                    var milestonearray = (from c in milestone select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                    sr.select2data = milestonearray;
                }
                return sr;
            }
        }
        public async Task<Select3Result> GetQuotationStatus()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                Select3Result sr = new Select3Result();
                var statusDtos = new List<datadto3>();
                var submittedStatus = _QuotationStatusRepository.GetAll().Where(p => p.Submitted == true).FirstOrDefault();
                if (submittedStatus != null)
                {
                    statusDtos.Add(new datadto3 { Id = submittedStatus.Id, Name = submittedStatus.QuotationStatusName, Code = "Submitted" });
                }

                var revisedStatus = _QuotationStatusRepository.GetAll().Where(p => p.Revised == true).FirstOrDefault();
                if (revisedStatus != null)
                {
                    statusDtos.Add(new datadto3 { Id = revisedStatus.Id, Name = revisedStatus.QuotationStatusName, Code = "Revised" });
                }

                var wonStatus = _QuotationStatusRepository.GetAll().Where(p => p.Won == true).FirstOrDefault();
                if (wonStatus != null)
                {
                    statusDtos.Add(new datadto3 { Id = wonStatus.Id, Name = wonStatus.QuotationStatusName, Code = "Won" });
                }

                var lostStatus = _QuotationStatusRepository.GetAll().Where(p => p.Lost == true).FirstOrDefault();
                if (lostStatus != null)
                {
                    statusDtos.Add(new datadto3 { Id = lostStatus.Id, Name = lostStatus.QuotationStatusName, Code = "Lost" });
                }

                sr.select3data = statusDtos.ToArray();
                return sr;
            }
        }

    }

    public class datadto3
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class Select3Result
    {
        public datadto3[] select3data { get; set; }
    }
    public class Select4Result
    {
        public datadto4[] select4data { get; set; }
    }
    public class datadto4
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
    public class datadto5
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int SalesId { get; set; }
        public string SalesName { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }

    }
    public class Select5Result
    {
        public datadto5 select2data { get; set; }
    }
    public class datadto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class Select2Result
    {
        public datadto[] select2data { get; set; }
    }
    public class Select2ResultInput
    {
        public int Id { get; set; }
    }

    public class Select6Result
    {
        public Select6ResultInput select6data { get; set; }
    }
    public class Select6ResultInput
    {
        public decimal TotalAmount { get; set; }
        public decimal DueAmount { get; set; }

    }

    public class Select2Company
    {
        public Companydto[] select2data { get; set; }
    }
    public class Companydto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long? SalesId { get; set; }
        public string SalesName { get; set; }
    }

}
