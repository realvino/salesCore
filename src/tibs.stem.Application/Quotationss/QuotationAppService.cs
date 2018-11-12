using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Quotations;
using tibs.stem.Quotationss.Dto;
using tibs.stem.QuotationProducts;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using tibs.stem.QuotationServices;
using tibs.stem.Currencies;
using tibs.stem.Enquirys;
using System.Data;
using System.Data.SqlClient;
using tibs.stem.PaymentSchedules;
using tibs.stem.PaymentCollections;
using System.Net;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using tibs.stem.Validitys;
using tibs.stem.Warrantys;
using tibs.stem.QPayments;
using tibs.stem.Freights;
using tibs.stem.Deliverys;
using tibs.stem.Packings;
using tibs.stem.QuotationStatuss;
using tibs.stem.VatAmountSettings;
using Abp.Authorization;
using tibs.stem.Authorization;

namespace tibs.stem.Quotationss
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_Quotations)]
    public class QuotationAppService : stemAppServiceBase, IQuotationAppService
    {
        private readonly IRepository<Quotation> _QuotationRepository;
        private readonly IRepository<QuotationService> _QuotationServiceRepository;
        private readonly IRepository<QuotationStatus> _QuotationStatusRepository;
        private readonly IRepository<Currency> _CurrencyRepository;
        private readonly IRepository<CustomCurrency> _CustomCurrencyRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<Enquiry> _EnquiryRepository;
        private readonly IRepository<QuotationProduct> _QuotationProductRepository;
        private readonly IRepository<PaymentSchedule> _PaymentScheduleRepository;
        private readonly IRepository<PaymentCollection> _PaymentCollectionRepository;
        private readonly IRepository<Validity> _ValidityRepository;
        private readonly IRepository<Warranty> _WarrantyRepository;
        private readonly IRepository<QPayment> _QpaymentRepository;
        private readonly IRepository<Freight> _FreightRepository;
        private readonly IRepository<Delivery> _DeliveryRepository;
        private readonly IRepository<Packing> _PackingRepository;
        private readonly IRepository<TenantVatAmountSetting> _TenantVatAmountRepository;


        public QuotationAppService(
            IRepository<CustomCurrency> CustomCurrencyRepository, 
            IRepository<Currency> CurrencyRepository,
            IRepository<Validity> ValidityRepository,
            IRepository<Warranty> WarrantyRepository,
            IRepository<QPayment> QpaymentRepository,
            IRepository<TenantVatAmountSetting> TenantVatAmountRepository,
            IRepository<Freight> FreightRepository,
            IRepository<Delivery> DeliveryRepository,
            IRepository<Packing> PackingRepository,
            IRepository<Quotation> QuotationRepository, 
            IRepository<QuotationProduct> QuotationProductRepository, 
            IRepository<QuotationService> QuotationServiceRepository,
            IUnitOfWorkManager unitOfWorkManager, IRepository<QuotationStatus> QuotationStatusRepository,
            IAbpSession session,
            IRepository<Enquiry> EnquiryRepository,
            IRepository<PaymentSchedule> PaymentScheduleRepository,
            IRepository<PaymentCollection> PaymentCollectionRepository)
        {
            _QuotationRepository = QuotationRepository;
            _TenantVatAmountRepository = TenantVatAmountRepository;
            _QuotationProductRepository = QuotationProductRepository;
            _QuotationServiceRepository = QuotationServiceRepository;
            _QuotationStatusRepository = QuotationStatusRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _CurrencyRepository = CurrencyRepository;
            _CustomCurrencyRepository = CustomCurrencyRepository;
            _session = session;
            _EnquiryRepository = EnquiryRepository;
            _PaymentScheduleRepository = PaymentScheduleRepository;
            _PaymentCollectionRepository = PaymentCollectionRepository;
            _ValidityRepository = ValidityRepository;
            _WarrantyRepository = WarrantyRepository;
            _QpaymentRepository = QpaymentRepository;
            _FreightRepository = FreightRepository;
            _DeliveryRepository = DeliveryRepository;
            _PackingRepository = PackingRepository;

        }

        public async Task<PagedResultDto<QuotationList>> GetQuotation(GetQuotationInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _QuotationRepository.GetAll().Where(p => p.Revised != true)
                  .WhereIf(
                  !input.Filter.IsNullOrEmpty(),
                  p => p.SubjectName.Contains(input.Filter) ||
                       p.Enquirys.Title.Contains(input.Filter) ||
                       p.QuotationTitle.Name.Contains(input.Filter) ||
                       p.Companys.Name.Contains(input.Filter) ||
                       p.Status.QuotationStatusName.Contains(input.Filter)
                  );
                var quotation = (from a in query
                                 select new QuotationList
                                 {
                                     Id = a.Id,
                                     SubjectName = a.SubjectName,
                                     ProposalNumber = a.ProposalNumber,
                                     ProjectRef = a.ProjectRef,
                                     Date = a.Date,
                                     DateString = a.CreationTime.ToString(),
                                     CreationTime = a.CreationTime,
                                     ClosureDate = a.ClosureDate,
                                     Revised = a.Revised,
                                     Archived = a.Archived,
                                     Total = a.Vat == true ? Math.Round((a.ExchangeRate * (a.VatAmount + a.Total - a.OverallDiscountinUSD)), 2) : Math.Round((a.ExchangeRate * (a.Total - a.OverallDiscountinUSD)), 2),
                                     SalesOrderNumber = a.SalesOrderNumber,
                                     LostDate = a.LostDate,
                                     OverallDiscount = a.OverallDiscount,
                                     CustomerPONumber = a.CustomerPONumber,
                                     ExchangeRate = a.ExchangeRate,
                                     EnquiryId = a.EnquiryId,
                                     EnquiryTitle = a.EnquiryId != null ? a.Enquirys.Title : "",
                                     QuotationTitleId = a.QuotationTitleId,
                                     QuotationTitleName = a.QuotationTitleId != null ? a.QuotationTitle.Code : "",
                                     CompanyId = a.CompanyId,
                                     CompanyName = a.CompanyId != null ? a.Companys.Name : "",
                                     StatusId = a.StatusId,
                                     StatusName = a.StatusId != null ? a.Status.QuotationStatusName : "",
                                     FreightId = a.FreightId,
                                     FreightName = a.FreightId != null ? a.Freights.FreightName : "",
                                     PaymentId = a.PaymentId,
                                     PaymentName = a.PackingId != null ? a.Payment.PaymentName : "",
                                     PackingId = a.PackingId,
                                     PackingName = a.PackingId != null ? a.Packings.PackingName : "",
                                     WarrantyId = a.WarrantyId,
                                     WarrantyName = a.WarrantyId != null ? a.Warrantys.WarrantyName : "",
                                     ValidityId = a.ValidityId,
                                     ValidityName = a.ValidityId != null ? a.Validitys.ValidityName : "",
                                     DeliveryId = a.DeliveryId,
                                     DeliveryName = a.DeliveryId != null ? a.Deliverys.DeliveryName : "",
                                     CurrencyId = a.CurrencyId,
                                     CurrencyName = a.CurrencyId != null ? a.Currencys.Name : "",
                                     SalesmanId = a.SalesmanId,
                                     ReasonId = a.ReasonId,
                                     Submitted = a.Submitted,
                                     Won = a.Won,
                                     Lost = a.Lost,
                                     WonDate = a.WonDate,
                                     SubmittedDate = a.SubmittedDate,
                                     ReasonName = a.ReasonId != null ? a.Reasons.Name : "",
                                     ContactId = a.ContactId,
                                     ContactName = a.ContactId != null ? a.Contacts.Name + a.Contacts.LastName : "",
                                     Vat = a.Vat,
                                     VatPercentage = a.VatPercentage,
                                     VatAmount = a.VatAmount
                                 });
                var quotationCount = await quotation.CountAsync();
                var quotationlist = await quotation
                    .OrderByDescending(p => p.CreationTime)
                    .PageBy(input)
                    .ToListAsync();

                var quotationlistoutput = quotationlist.MapTo<List<QuotationList>>();

                return new PagedResultDto<QuotationList>(quotationCount, quotationlistoutput);
            } 
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit)]
        public async Task<GetQuotation> GetQuotationForEdit(NullableIdDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var output = new GetQuotation
                {
                };

                var a = _QuotationRepository.GetAll().Where(p => p.Id == input.Id)
                        .Include(q => q.Enquirys)
                        .Include(q => q.Companys)
                        .Include(q => q.Competitor)
                        .Include(q => q.Contacts)
                        .Include(q => q.Currencys)
                        .Include(q => q.Deliverys)
                        .Include(q => q.Freights)
                        .Include(q => q.MileStones)
                        .Include(q => q.Packings)
                        .Include(q => q.Payment)
                        .Include(q => q.QuotationTitle)
                        .Include(q => q.Reasons)
                        .Include(q => q.Salesman)
                        .Include(q => q.Status)
                        .Include(q => q.Validitys)
                        .Include(q => q.Warrantys)
                        .FirstOrDefault();
                var quotation = new QuotationList ();
                try
                {
                    quotation = new QuotationList
                    {
                        Id = a.Id,
                        SubjectName = a.SubjectName,
                        ProposalNumber = a.ProposalNumber,
                        ProjectRef = a.ProjectRef,
                        Date = a.Date,
                        ClosureDate = a.ClosureDate,
                        Revised = a.Revised,
                        Archived = a.Archived,
                        Total = a.Total,
                        IsQuotationRevised = a.StatusId > 0 ? a.Status.Revised : false,
                        OverAllTotal = Math.Round((a.ExchangeRate * (a.Total - a.OverallDiscountinUSD)), 2),
                        SalesOrderNumber = a.SalesOrderNumber,
                        LostDate = a.LostDate,
                        OverallDiscount = a.OverallDiscount,
                        OverallDiscountinUSD = a.OverallDiscountinUSD,
                        CustomerPONumber = a.CustomerPONumber,
                        ExchangeRate = a.ExchangeRate,
                        EnquiryId = a.EnquiryId,
                        EnquiryTitle = a.EnquiryId != null ? a.Enquirys.Title : "",
                        QuotationTitleId = a.QuotationTitleId,
                        QuotationTitleName = a.QuotationTitleId != null ? a.QuotationTitle.Code : "",
                        CompanyId = a.CompanyId,
                        CompanyName = a.CompanyId != null ? a.Companys.Name : "",
                        MileStoneId = a.MileStoneId,
                        StatusId = a.StatusId,
                        StatusName = a.StatusId != null ? a.Status.QuotationStatusName : "",
                        FreightId = a.FreightId,
                        FreightName = a.FreightId != null ? a.Freights.FreightName : "",
                        PaymentId = a.PaymentId,
                        PaymentName = a.PackingId != null ? a.Payment.PaymentName : "",
                        PackingId = a.PackingId,
                        PackingName = a.PackingId != null ? a.Packings.PackingName : "",
                        WarrantyId = a.WarrantyId,
                        WarrantyName = a.WarrantyId != null ? a.Warrantys.WarrantyName : "",
                        ValidityId = a.ValidityId,
                        ValidityName = a.ValidityId != null ? a.Validitys.ValidityName : "",
                        DeliveryId = a.DeliveryId,
                        DeliveryName = a.DeliveryId != null ? a.Deliverys.DeliveryName : "",
                        CurrencyId = a.CurrencyId,
                        CurrencyName = a.CurrencyId != null ? a.Currencys.Code : "",
                        SalesmanId = a.SalesmanId,
                        ReasonId = a.ReasonId,
                        Submitted = a.Submitted,
                        Won = a.Won,
                        Lost = a.Lost,
                        WonDate = a.WonDate,
                        SubmittedDate = a.SubmittedDate,
                        ReasonName = a.ReasonId != null ? a.Reasons.Name : "",
                        TenantId = a.TenantId,
                        ContactId = a.ContactId,
                        ContactName = a.ContactId != null ? a.Contacts.Name + a.Contacts.LastName : "",
                        CompetitorId = a.CompetitorId,
                        CompetitorName = a.CompetitorId != null ? a.Competitor.Name : "",
                        Vat = a.Vat,
                        VatPercentage = a.VatPercentage,
                        VatAmount = a.VatAmount

                    };
                }
                catch(Exception ex)
                {
                    throw new UserFriendlyException("Ooops!", "You Are Not Authorized");
                }
                 

                output.quotations = quotation.MapTo<QuotationList>();

                return output;
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_Quotations_Create)]
        public async Task <int> CreateOrUpdateQuotation(CreateQuotationInput input)
        {
            var id = 0;

            if (input.Id != 0)
            {
              id = await UpdateQuotation(input);
            }
            else
            {

              id =  await CreateQuotation(input);
            }
            return id;
        }
        public async Task <int> CreateQuotation(CreateQuotationInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var DefaultVatPercentage = _TenantVatAmountRepository.GetAll().Where(p => p.TenantId == _session.TenantId).FirstOrDefault();
                input.Vat = true;
               
                if (DefaultVatPercentage != null)
                {
                    input.VatPercentage = DefaultVatPercentage.VatAmount;
                }
                input.VatAmount = 0;

                var user = await UserManager.GetUserByIdAsync((long)input.SalesmanId);
                var grantedPermissions = await UserManager.GetGrantedPermissionsAsync(user);
                var count = grantedPermissions.Where(p => p.Name == "Pages.Tenant.Managemant.Leads").ToList();
                var sd = 0;
                if (count.Count() > 0)
                {
                    decimal exchangerate = 1;
                    int tenantid = (int)_session.TenantId;
                    if (input.CurrencyId != null)
                    {
                        var currency = _CustomCurrencyRepository.GetAll().Where(p => p.CurrencyId == input.CurrencyId && p.TenantId == tenantid).FirstOrDefault();
                        if (currency == null)
                        {
                            exchangerate = _CurrencyRepository.GetAll().Where(p => p.Id == input.CurrencyId).Select(p => p.ConversionRatio).FirstOrDefault();
                        }
                        else
                        {
                            exchangerate = currency.ConversionRatio;
                        }
                    }

                    if (input.EnquiryId > 0 && (input.MileStoneId == 0 || input.MileStoneId == null))
                    {
                        var enquiryMile = (from r in _EnquiryRepository.GetAll().Where(p => p.Id == input.EnquiryId) select r.MileStoneId).FirstOrDefault();
                        input.MileStoneId = enquiryMile;
                    }

                    input.ExchangeRate = exchangerate;

                    var date = DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                    input.StatusId = _QuotationStatusRepository.GetAll().Where(o => o.New == true).Select(o => o.Id).DefaultIfEmpty().First();
                    input.ValidityId = _ValidityRepository.GetAll().Select(o => o.Id).DefaultIfEmpty().First();
                    input.WarrantyId = _WarrantyRepository.GetAll().Select(o => o.Id).DefaultIfEmpty().First();
                    input.PackingId = _PackingRepository.GetAll().Select(o => o.Id).DefaultIfEmpty().First();
                    input.FreightId = _FreightRepository.GetAll().Select(o => o.Id).DefaultIfEmpty().First();
                    input.PaymentId = _QpaymentRepository.GetAll().Select(o => o.Id).DefaultIfEmpty().First();
                    input.DeliveryId = _DeliveryRepository.GetAll().Select(o => o.Id).DefaultIfEmpty().First();
                    var quotation = input.MapTo<Quotation>();

                    var val3 = _QuotationRepository
                     .GetAll().Where(p => p.SubjectName == input.SubjectName).FirstOrDefault();

                    if (val3 == null)
                    {
                        try
                        {

                            sd = await _QuotationRepository.InsertAndGetIdAsync(quotation);
                            var val = _QuotationRepository.GetAll().Where(p => p.CreationTime.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) == date).Count() + 1;

                            quotation.ProposalNumber = "Q" + date + _session.TenantId + val.ToString("000") + "-R0";

                            var val2 = _QuotationRepository.GetAll().Where(p => p.ProposalNumber == quotation.ProposalNumber).Count();

                            while (val2 > 0)
                            {
                                val = val + 1;
                                quotation.ProposalNumber = "Q" + date + _session.TenantId + val.ToString("000") + "-R0";
                                val2 = _QuotationRepository.GetAll().Where(p => p.ProposalNumber == quotation.ProposalNumber).Count();
                            }
                            quotation.Date = (from r in _QuotationRepository.GetAll() where r.Id == sd select r.CreationTime).FirstOrDefault();
                            quotation.Revised = false;
                            await _QuotationRepository.UpdateAsync(quotation);
                        }
                        catch (Exception ex)
                        {
                            string ter;
                            ter = ex.InnerException.ToString();
                        }

                    }
                    else
                    {
                        throw new UserFriendlyException("Ooops!", "Duplicate Data Occured...");
                    }
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "This Quotation Contained invalid Salesperson");
                }

                return sd;
            }
        }

        public async Task<int> QuotationRevision(int QuotationId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                int NewQuotationId = 0;
                ConnectionAppService db = new ConnectionAppService();
                DataTable ds = new DataTable();
                using (SqlConnection con = new SqlConnection(db.ConnectionString()))
                {
                    SqlCommand sqlComm = new SqlCommand("Sp_QuotationRevision", con);
                    sqlComm.Parameters.AddWithValue("@QuotationId", QuotationId);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                    {
                        da.Fill(ds);
                    }
                    var RQuotationId = ds.Rows[0]["Id"].ToString();
                    NewQuotationId = int.Parse(RQuotationId);
                }
                return NewQuotationId;
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_Quotations_Delete)]
        public async Task DeleteQuotation(EntityDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                //await _QuotationRepository.DeleteAsync(input.Id);
                ConnectionAppService db = new ConnectionAppService();
                DataTable ds = new DataTable();
                using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
                {
                    SqlCommand sqlComm = new SqlCommand("Sp_DeleteAllDetail", conn);
                    sqlComm.Parameters.AddWithValue("@Id", input.Id);
                    sqlComm.Parameters.AddWithValue("@TableId", 1);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    sqlComm.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public async void UpdateQuotationTotal(UpdateQuotationTotal input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                decimal QuotationTotal = 0;

                var QutationProducts = (from r in _QuotationProductRepository.GetAll()
                                        where r.QuotationId == input.QuotationId
                                        select r).ToList();

                var QutationServices = (from r in _QuotationServiceRepository.GetAll()
                          where r.QuotationId == input.QuotationId
                          select r).ToList();

                foreach (var Qp in QutationProducts)
                {
                    decimal total = (decimal)(Qp.EstimatedPriceUSD);
                    QuotationTotal = QuotationTotal + total;
                }
                foreach (var Qs in QutationServices)
                {
                    decimal total = (decimal)(Qs.Price);
                    QuotationTotal = QuotationTotal + total;
                }

                var qut = (from r in _QuotationRepository.GetAll()
                           where r.Id == input.QuotationId
                           select r).FirstOrDefault();

                var qutmap = qut.MapTo<Quotation>();
                qutmap.Total = QuotationTotal;

                if (qutmap.Vat == true)
                {
                    if (qutmap.OverallDiscount > 0)
                    {
                        qutmap.VatAmount = ((qutmap.Total - qutmap.OverallDiscountinUSD) * qutmap.VatPercentage) / 100;
                    }
                    else
                    {
                        qutmap.VatAmount = (qutmap.Total * qutmap.VatPercentage) / 100;
                    }
                }
                else
                {
                    qutmap.VatAmount = 0;
                }

                qutmap.Total = QuotationTotal;
                await _QuotationRepository.UpdateAsync(qutmap);
            }

        }
        public ListResultDto<QuotationProductList> GetQuotationProduct(GetQuotationsInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _QuotationProductRepository.GetAll().Where(p => p.QuotationId == input.QuotationId);
                var qut = _QuotationRepository.GetAll().Where(p => p.Id == input.QuotationId).FirstOrDefault();
                var quotationProduct = (from a in query
                                        select new QuotationProductList
                                        {
                                            Id = a.Id,
                                            QuotationId = a.QuotationId,
                                            QuotationName = a.QuotationId != null ? a.Quotations.SubjectName : "",
                                            ProductId = a.ProductId,
                                            ProductName = a.ProductId != null ? a.Products.ProductName : "",
                                            Quantity = a.Quantity,
                                            Price = Math.Round(a.PriceUSD * qut.ExchangeRate),
                                            Optional = a.Optional,
                                            Discount = a.Discount,
                                            EstimatedPrice = Math.Round(a.EstimatedPriceUSD * qut.ExchangeRate),
                                            TenantId = a.TenantId,
                                            EstimatedPriceUSD = a.EstimatedPriceUSD,
                                            PriceUSD = a.PriceUSD,
                                            PriceLevelProductId = a.PriceLevelProductId,
                                            PriceLevelProductName = a.PriceLevelProductId != null ? a.PriceLevelProducts.PriceLevels.PriceLevelName : ""

                                        }).ToList();


                return new ListResultDto<QuotationProductList>(quotationProduct.MapTo<List<QuotationProductList>>());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit_EditQuotationProduct)]
        public async Task<GetQuotationProduct> GetQuotationProductForEdit(NullableIdDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                // int tenantid = (int)_session.TenantId;
                var output = new GetQuotationProduct
                {
                };

                var query = _QuotationProductRepository
                    .GetAll().Where(p => p.Id == input.Id);
                var quotationProduct = (from a in query
                                        select new QuotationProductList
                                        {
                                            Id = a.Id,
                                            QuotationId = a.QuotationId,
                                            QuotationName = a.QuotationId != null ? a.Quotations.SubjectName : "",
                                            ProductId = a.ProductId,
                                            ProductName = a.ProductId != null ? a.Products.ProductName : "",
                                            Quantity = a.Quantity,
                                            Price = a.Price,
                                            Optional = a.Optional,
                                            Discount = a.Discount,
                                            EstimatedPrice = a.EstimatedPrice,
                                            TenantId = a.TenantId,
                                            EstimatedPriceUSD = a.EstimatedPriceUSD,
                                            PriceUSD = a.PriceUSD,
                                            PriceLevelProductId = a.PriceLevelProductId,
                                            PriceLevelProductName = a.PriceLevelProductId != null ? a.PriceLevelProducts.PriceLevels.PriceLevelName : ""

                                        }).FirstOrDefault();
                decimal currconver = 1;

                if (quotationProduct != null)
                {
                    var Quotation = _QuotationRepository.GetAll().Where(p => p.Id == quotationProduct.QuotationId).FirstOrDefault();
                    //var currency = _CustomCurrencyRepository.GetAll().Where(p => p.CurrencyId == Quotation.CurrencyId && p.TenantId == tenantid).FirstOrDefault();
                    //if (currency == null)
                    //{
                    //    currconver = _CurrencyRepository.GetAll().Where(p => p.Id == Quotation.CurrencyId).Select(p => p.ConversionRatio).FirstOrDefault();
                    //}
                    //else
                    //{
                    //    currconver = currency.ConversionRatio;
                    //}
                    currconver = Quotation.ExchangeRate;
                    quotationProduct.Price = Math.Round(quotationProduct.PriceUSD * currconver, 0);
                    quotationProduct.EstimatedPrice = Math.Round(quotationProduct.EstimatedPriceUSD * currconver, 0);
                }

                output.quotationProducts = quotationProduct.MapTo<QuotationProductList>();

                return output;
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit_AddQuotationProduct)]
        public async Task CreateOrUpdateQuotationProduct(CreateQuotationProductInput input)
        {
            if (input.Id != 0)
            {
                await UpdateQuotationProduct(input);
            }
            else
            {
                await CreateQuotationProduct(input);
            }
        }
        public async Task CreateQuotationProduct(CreateQuotationProductInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                //decimal currconver = 1;
                //var Quotation = _QuotationRepository.GetAll().Where(p => p.Id == input.QuotationId).FirstOrDefault();
                //currconver = Quotation.ExchangeRate;
             
                //input.PriceUSD = Math.Round(input.Price / currconver, 2);
                //input.EstimatedPriceUSD = Math.Round(input.EstimatedPrice / currconver, 2);

                var quotationProduct = input.MapTo<QuotationProduct>();
                var val = _QuotationProductRepository
                 .GetAll().Where(p => p.QuotationId == input.QuotationId && p.ProductId == input.ProductId).FirstOrDefault();

                if (val == null)
                {
                    await _QuotationProductRepository.InsertAsync(quotationProduct);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in QuotationProduct...");
                }
            }
        }
        public async Task UpdateQuotationProduct(CreateQuotationProductInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                  var quotationProduct = input.MapTo<QuotationProduct>();

                var val = _QuotationProductRepository
                .GetAll().Where(p => p.QuotationId == input.QuotationId && p.ProductId == input.ProductId && p.Id != input.Id).FirstOrDefault();

                if (val == null)
                {
                    await _QuotationProductRepository.UpdateAsync(quotationProduct);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in QuotationProduct...");
                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit_DeleteQuotationProduct)]
        public async Task DeleteQuotationProduct(EntityDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                await _QuotationProductRepository.DeleteAsync(input.Id);
            }
        }
        public ListResultDto<QuotationServiceList> GetQuotationService(GetQuotationsInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _QuotationServiceRepository.GetAll().Where(p => p.QuotationId == input.QuotationId);
                var qut = _QuotationRepository.GetAll().Where(p => p.Id == input.QuotationId).FirstOrDefault();
                var quotationService = (from a in query
                                        select new QuotationServiceList
                                        {
                                            Id = a.Id,
                                            QuotationId = a.QuotationId,
                                            QuotationName = a.QuotationId != null ? a.Quotations.SubjectName : "",
                                            ServiceId = a.ServiceId,
                                            ServiceCode = a.ServiceId != null ? a.Services.ServiceCode : "",
                                            ServiceName = a.ServiceId != null ? a.Services.ServiceName: "",
                                            Price = a.Price,
                                            CovertPrice = Math.Round(a.Price * qut.ExchangeRate, 0),
                                            TenantId = a.TenantId

                                        }).ToList();


                return new ListResultDto<QuotationServiceList>(quotationService.MapTo<List<QuotationServiceList>>());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit_EditQuotationService)]
        public async Task<GetQuotationService> GetQuotationServiceForEdit(NullableIdDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var output = new GetQuotationService
                {
                };

                var query = _QuotationServiceRepository
                    .GetAll().Where(p => p.Id == input.Id);

                var quotationService = (from a in query
                                        select new QuotationServiceList
                                        {
                                            Id = a.Id,
                                            QuotationId = a.QuotationId,
                                            QuotationName = a.QuotationId != null ? a.Quotations.SubjectName : "",
                                            ServiceId = a.ServiceId,
                                            ServiceName = a.ServiceId != null ? a.Services.ServiceCode : "",
                                            Price = a.Price,
                                            TenantId = a.TenantId

                                        }).FirstOrDefault();

                if (quotationService != null)
                {
                    var qut = _QuotationRepository.GetAll().Where(p => p.Id == quotationService.QuotationId).FirstOrDefault();
                    quotationService.CovertPrice = Math.Round(quotationService.Price * qut.ExchangeRate, 0);
                }

                output.quotationServices = quotationService.MapTo<QuotationServiceList>();

                return output;
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit_AddQuotationService)]
        public async Task CreateOrUpdateQuotationService(CreateQuotationServiceInput input)
        {
            if (input.Id != 0)
            {
                await UpdateQuotationService(input);
            }
            else
            {
                await CreateQuotationService(input);
            }
        }
        public async Task CreateQuotationService(CreateQuotationServiceInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var quotationService = input.MapTo<QuotationService>();
                decimal currconver = 1;
                var Quotation = _QuotationRepository.GetAll().Where(p => p.Id == input.QuotationId).FirstOrDefault();
                currconver = Quotation.ExchangeRate;
                quotationService.Price = Math.Round(input.CovertPrice / currconver, 2);
                var val = _QuotationServiceRepository
                 .GetAll().Where(p => p.QuotationId == input.QuotationId && p.ServiceId == input.ServiceId).FirstOrDefault();

                if (val == null)
                {
                    await _QuotationServiceRepository.InsertAsync(quotationService);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in QuotationService...");
                }
            }
        }
        public async Task UpdateQuotationService(CreateQuotationServiceInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;
                var quotationService = input.MapTo<QuotationService>();
                decimal currconver = 1;
                var Quotation = _QuotationRepository.GetAll().Where(p => p.Id == input.QuotationId).FirstOrDefault();
                currconver = Quotation.ExchangeRate;
                quotationService.Price = Math.Round(input.CovertPrice / currconver, 2);
                var val = _QuotationServiceRepository
                .GetAll().Where(p => (p.QuotationId == input.QuotationId && p.ServiceId == input.ServiceId) && p.Id != input.Id).FirstOrDefault();

                if (val == null)
                {
                    await _QuotationServiceRepository.UpdateAsync(quotationService);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in QuotationService...");
                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_Quotations_Edit_DeleteQuotationService)]
        public async Task DeleteQuotationService(EntityDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                await _QuotationServiceRepository.DeleteAsync(input.Id);
            }
        }
        public async Task<GetQuotationPreview> GetQuotationPreviewForEdit(NullableIdDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var output = new GetQuotationPreview
                { };
                var query = _QuotationRepository
                   .GetAll().Where(p => p.Id == input.Id);
                var quotation = (from a in query
                                 select new GetPreview
                                 {
                                     QuotationId = a.Id,
                                     CompanyName = a.CompanyId != null ? a.Companys.Name : "",
                                     ContactName = a.ContactId != null ? a.Contacts.Name + a.Contacts.LastName : "",
                                     CurrencyName = a.CurrencyId != null ? a.Currencys.Name : "",
                                     CurrencyCode = a.CurrencyId != null ? a.Currencys.Code : "",
                                     CreationTime = a.CreationTime,
                                     Total = a.Total,


                                 }).FirstOrDefault();

                output.preview = quotation.MapTo<GetPreview>();

                return output;
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Quotation_SalesOrder)]
        public async Task<PagedResultDto<QuotationList>> GetSalesOrder(GetQuotationInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var WonStatusId = (from p in _QuotationStatusRepository.GetAll() where p.Won == true select p.Id).FirstOrDefault();
                var query = _QuotationRepository.GetAll().Where(p => p.StatusId == WonStatusId && p.Won == true)
                  .WhereIf(
                  !input.Filter.IsNullOrEmpty(),
                  p => p.Id.ToString().Contains(input.Filter) ||
                       p.SubjectName.Contains(input.Filter) ||
                       p.Enquirys.Title.Contains(input.Filter) ||
                       p.QuotationTitle.Name.Contains(input.Filter) ||
                       p.Companys.Name.Contains(input.Filter) ||
                       p.Status.QuotationStatusName.Contains(input.Filter) ||
                       p.SalesmanId.ToString().Contains(input.Filter)
                  );

                var quotation = (from a in query
                                 select new QuotationList
                                 {
                                     Id = a.Id,
                                     SubjectName = a.SubjectName,
                                     ProposalNumber = a.ProposalNumber,
                                     ProjectRef = a.ProjectRef,
                                     Date = a.Date,
                                     DateString = a.CreationTime.ToString(),
                                     ClosureDate = a.ClosureDate,
                                     Revised = a.Revised,
                                     Archived = a.Archived,
                                     Total = a.Vat == true ? Math.Round((a.ExchangeRate * (a.VatAmount + a.Total - a.OverallDiscountinUSD)), 2) : Math.Round((a.ExchangeRate * (a.Total - a.OverallDiscountinUSD)), 2),
                                     SalesOrderNumber = a.SalesOrderNumber,
                                     LostDate = a.LostDate,
                                     OverallDiscount = a.OverallDiscount,
                                     CustomerPONumber = a.CustomerPONumber,
                                     ExchangeRate = a.ExchangeRate,
                                     EnquiryId = a.EnquiryId,
                                     EnquiryTitle = a.EnquiryId != null ? a.Enquirys.Title : "",
                                     QuotationTitleId = a.QuotationTitleId,
                                     QuotationTitleName = a.QuotationTitleId != null ? a.QuotationTitle.Code : "",
                                     CompanyId = a.CompanyId,
                                     CompanyName = a.CompanyId != null ? a.Companys.Name : "",
                                     StatusId = a.StatusId,
                                     StatusName = a.StatusId != null ? a.Status.QuotationStatusName : "",
                                     FreightId = a.FreightId,
                                     FreightName = a.FreightId != null ? a.Freights.FreightName : "",
                                     PaymentId = a.PaymentId,
                                     PaymentName = a.PackingId != null ? a.Payment.PaymentName : "",
                                     PackingId = a.PackingId,
                                     PackingName = a.PackingId != null ? a.Packings.PackingName : "",
                                     WarrantyId = a.WarrantyId,
                                     WarrantyName = a.WarrantyId != null ? a.Warrantys.WarrantyName : "",
                                     ValidityId = a.ValidityId,
                                     ValidityName = a.ValidityId != null ? a.Validitys.ValidityName : "",
                                     DeliveryId = a.DeliveryId,
                                     DeliveryName = a.DeliveryId != null ? a.Deliverys.DeliveryName : "",
                                     CurrencyId = a.CurrencyId,
                                     CurrencyName = a.CurrencyId != null ? a.Currencys.Name : "",
                                     SalesmanId = a.SalesmanId,
                                     ReasonId = a.ReasonId,
                                     Submitted = a.Submitted,
                                     Won = a.Won,
                                     Lost = a.Lost,
                                     WonDate = a.WonDate,
                                     SubmittedDate = a.SubmittedDate,
                                     ReasonName = a.ReasonId != null ? a.Reasons.Name : "",
                                     ContactId = a.ContactId,
                                     ContactName = a.ContactId != null ? a.Contacts.Name + a.Contacts.LastName : "",

                                 });




                var quotationCount = await quotation.CountAsync();
                var quotationlist = await quotation
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();

                var quotationlistoutput = quotationlist.MapTo<List<QuotationList>>();


                return new PagedResultDto<QuotationList>(quotationCount, quotationlistoutput);
            }
        }
        public ListResultDto<PaymentScheduleLists> GetPaymentSchedule(GetQuotationsInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _PaymentScheduleRepository.GetAll().Where(p => p.QuotationId == input.QuotationId);
                var Paymentschedules = (from a in query
                                        select new PaymentScheduleLists
                                        {
                                            Id = a.Id,
                                            QuotationId = a.QuotationId,
                                            TenantId = a.TenantId,
                                            ScheduledDate = a.ScheduledDate,
                                            Total = a.Total,
                                            UserId = a.Quotations.SalesmanId ?? 0,
                                            UserName = a.Quotations.SalesmanId != null ? a.Quotations.Salesman.UserName: ""
                                        }).ToList();


                return new ListResultDto<PaymentScheduleLists>(Paymentschedules.MapTo<List<PaymentScheduleLists>>());
            }
        }
        public async Task<PaymentScheduleLists> GetPaymentScheduleForEdit(NullableIdDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _PaymentScheduleRepository.GetAll().Where(p => p.Id == input.Id);

                var output = (from a in query
                              select new PaymentScheduleLists
                              {
                                  Id = a.Id,
                                  QuotationId = a.QuotationId,
                                  TenantId = a.TenantId,
                                  ScheduledDate = a.ScheduledDate,
                                  Total = a.Total,
                                  UserId = a.Quotations.SalesmanId ?? 0,
                                  UserName = a.Quotations.SalesmanId != null ? a.Quotations.Salesman.UserName : ""
                              }).FirstOrDefault();

                return output;
            }

        }
        public async Task CreateOrUpdatePaymentSchedule(CreatePaymentScheduleInput input)
        {
            if (input.Id != 0)
            {
                await UpdatePaymentSchedule(input);
            }
            else
            {
                await CreatePaymentSchedule(input);
            }
        }
        public async Task CreatePaymentSchedule(CreatePaymentScheduleInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                input.TenantId = (int)_session.TenantId;

                var Paymentschedule = input.MapTo<PaymentSchedule>();

                decimal? ScheduledPay = (from q in _PaymentScheduleRepository.GetAll() where q.QuotationId == input.QuotationId select q.Total).Sum();
                if(ScheduledPay > 0)
                {
                    ScheduledPay = ScheduledPay + (decimal)input.Total;
                }
                else
                {
                    ScheduledPay = 0;
                }

                var Quotation = _QuotationRepository.GetAll().Where(p => p.Id == input.QuotationId).FirstOrDefault();

                if (Quotation.Total > 0)
                {
                    decimal QuotationTotal = Quotation.Vat == true ? Math.Round((Quotation.ExchangeRate * (Quotation.Total - Quotation.OverallDiscountinUSD + Quotation.VatAmount)), 2) : Math.Round((Quotation.ExchangeRate * (Quotation.Total - Quotation.OverallDiscountinUSD)), 2);

                    if (ScheduledPay <= QuotationTotal)
                    {
                        await _PaymentScheduleRepository.InsertAsync(Paymentschedule);
                    }
                    else
                    {
                        throw new UserFriendlyException("Ooops!", "Total Exceeds");
                    }
                }
            }
            
        }
        public async Task UpdatePaymentSchedule(CreatePaymentScheduleInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var Paymentschedule = await _PaymentScheduleRepository.GetAsync(input.Id);
                ObjectMapper.Map(input, Paymentschedule);

                decimal? ScheduledPay = (from q in _PaymentScheduleRepository.GetAll() where q.QuotationId == input.QuotationId && q.Id != input.Id select q.Total).Sum();
                if(ScheduledPay > 0)
                {
                    ScheduledPay = ScheduledPay + (decimal)input.Total;
                }

                var Quotation = _QuotationRepository.GetAll().Where(p => p.Id == input.QuotationId).FirstOrDefault();
                decimal QuotationTotal = Quotation.Vat == true ? Math.Round((Quotation.ExchangeRate * (Quotation.Total - Quotation.OverallDiscountinUSD + Quotation.VatAmount)), 2) : Math.Round((Quotation.ExchangeRate * (Quotation.Total - Quotation.OverallDiscountinUSD)), 2);

                if (ScheduledPay <= QuotationTotal)
                {
                    await _PaymentScheduleRepository.UpdateAsync(Paymentschedule);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Total Exceeds");
                }
            }
        }
        public async Task DeletePaymentSchedule(EntityDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                await _PaymentScheduleRepository.DeleteAsync(input.Id);
            }
        }
        public ListResultDto<PaymentCollectionLists> GetPaymentCollection(GetQuotationsInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var query = _PaymentCollectionRepository.GetAll().Where(p => p.QuotationId == input.QuotationId);
                var PayCollection = (from a in query
                                     select new PaymentCollectionLists
                                     {
                                         Id = a.Id,
                                         QuotationId = a.QuotationId,
                                         TenantId = a.TenantId,
                                         SalesInvoiceNUmber = a.SalesInvoiceNUmber,
                                         Amount = a.Amount,
                                         DueAmount = a.DueAmount,
                                         ChequeNo = a.ChequeNo,
                                         VoucherNo = a.VoucherNo,
                                         ChequeDate = a.ChequeDate,
                                         BankDate = a.BankDate,
                                         Remarks  = a.Remarks,
                                         Received = a.Received,
                                         CurrencyId = a.CurrencyId,
                                         PaymentId = a.PaymentId,
                                         CurrencyName = a.CurrencyId != null? a.Currencys.Name:"",
                                         PaymentName = a.PaymentId != null? a.Payments.Name: ""
                                     }).ToList();
                return new ListResultDto<PaymentCollectionLists>(PayCollection.MapTo<List<PaymentCollectionLists>>());
            }
        }
        public async Task<PaymentCollectionLists> GetPaymentCollectionForEdit(NullableIdDto input)
        {
            var query = _PaymentCollectionRepository.GetAll().Where(p => p.Id == input.Id);
            var output = (from a in query
                          select new PaymentCollectionLists
                          {
                              Id = a.Id,
                              QuotationId = a.QuotationId,
                              TenantId = a.TenantId,
                              SalesInvoiceNUmber = a.SalesInvoiceNUmber,
                              Amount = a.Amount,
                              DueAmount = a.DueAmount,
                              ChequeNo = a.ChequeNo,
                              VoucherNo = a.VoucherNo,
                              ChequeDate = a.ChequeDate,
                              BankDate = a.BankDate,
                              Remarks = a.Remarks,
                              Received = a.Received,
                              CurrencyId = a.CurrencyId,
                              PaymentId = a.PaymentId,
                              CurrencyName = a.CurrencyId != null ? a.Currencys.Name : "",
                              PaymentName = a.PaymentId != null ? a.Payments.Name : ""
                          }).FirstOrDefault();

            return output;

        }
        public async Task CreateOrUpdatePaymentCollection(CreatePaymentCollectionInput input)
        {
            if (input.Id != 0)
            {
                await UpdatePaymentCollection(input);
            }
            else
            {
                await CreatePaymentCollection(input);
            }
        }
        public async Task CreatePaymentCollection(CreatePaymentCollectionInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId((int)_session.TenantId))
            {
                try
                {
                    input.TenantId = (int)_session.TenantId;
                    var PayCollection = input.MapTo<PaymentCollection>();

                    var Quotation = _QuotationRepository.GetAll().Where(p => p.Id == input.QuotationId).FirstOrDefault();
                    decimal QuotationTotal = Quotation.Vat == true ? Math.Round((Quotation.ExchangeRate * (Quotation.Total - Quotation.OverallDiscountinUSD + Quotation.VatAmount)), 2) : Math.Round((Quotation.ExchangeRate * (Quotation.Total - Quotation.OverallDiscountinUSD)), 2);

                    decimal DueAmount = (from p in _PaymentCollectionRepository.GetAll() where p.QuotationId == input.QuotationId && p.Received == true select p.Amount).Sum();

                    decimal BalanceAmount = QuotationTotal - DueAmount;

                    if (BalanceAmount > 0)
                    {
                        if (PayCollection.Received == true)
                        {
                            if (BalanceAmount - PayCollection.Amount >= 0)
                            {
                                PayCollection.DueAmount = BalanceAmount - PayCollection.Amount;
                            }
                            else
                            {
                                throw new UserFriendlyException("Ooops!", "PaymentCollection Amount Exceeds DueAmount");
                            }
                        }
                        else
                        {
                            PayCollection.DueAmount = BalanceAmount;
                        }
                        await _PaymentCollectionRepository.InsertAsync(PayCollection);
                    }
                    else
                    {
                        throw new UserFriendlyException("Ooops!", "PaymentCollection is completed");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }
        public async Task UpdatePaymentCollection(CreatePaymentCollectionInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId((int)_session.TenantId))
            {
                try
                {
                    var UpdatePayCollection = await _PaymentCollectionRepository.GetAsync(input.Id);
                    ObjectMapper.Map(input, UpdatePayCollection);

                    var Quotation = _QuotationRepository.GetAll().Where(p => p.Id == input.QuotationId).FirstOrDefault();
                    decimal QuotationTotal = Quotation.Vat == true ? Math.Round((Quotation.ExchangeRate * (Quotation.Total - Quotation.OverallDiscountinUSD + Quotation.VatAmount)), 2) : Math.Round((Quotation.ExchangeRate * (Quotation.Total - Quotation.OverallDiscountinUSD)), 2);

                    decimal DueAmount = (from p in _PaymentCollectionRepository.GetAll() where p.QuotationId == input.QuotationId && p.Received == true && p.Id != input.Id select p.Amount).Sum();

                    decimal BalanceAmount = QuotationTotal - DueAmount;

                    if (BalanceAmount > 0)
                    {
                        if (UpdatePayCollection.Received == true)
                        {
                            if (BalanceAmount - UpdatePayCollection.Amount >= 0)
                            {
                                UpdatePayCollection.DueAmount = BalanceAmount - UpdatePayCollection.Amount;
                            }
                            else
                            {
                                throw new UserFriendlyException("Ooops!", "PaymentCollection Amount Exceeds DueAmount");
                            }
                        }
                        else
                        {
                            UpdatePayCollection.DueAmount = BalanceAmount;
                        }
                        await _PaymentCollectionRepository.UpdateAsync(UpdatePayCollection);
                    }
                    else
                    {
                        throw new UserFriendlyException("Ooops!", "PaymentCollection is completed");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public async Task DeletePaymentCollection(EntityDto input)
        {
            await _PaymentCollectionRepository.DeleteAsync(input.Id);
        }
        public async Task<decimal> ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
        {
            decimal data = 0;
            try
            {
                WebClient client = new WebClient();
                Stream response = client.OpenRead(string.Format("http://finance.yahoo.com/d/quotes.csv?e=.csv&f=sl1d1t1&s={0}{1}=X", fromCurrency.ToUpper(), toCurrency.ToUpper()));
                StreamReader reader = new StreamReader(response);
                string yahooResponse = reader.ReadLine();
                response.Close();
                if (!string.IsNullOrWhiteSpace(yahooResponse))
                {
                    string[] values = Regex.Split(yahooResponse, ",");
                    if (values.Length > 0)
                    {
                        decimal rate = System.Convert.ToDecimal(values[1]);
                        data =  rate * amount;
                    }
                }
            } catch (Exception ex)
            {
                data = 0;
            }

            return data;
        }
        public async Task<int> UpdateQuotation(CreateQuotationInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                decimal exchangerate = 1;

                int tenantid = (int)_session.TenantId;

                if (input.CurrencyId != null)
                {
                    var currency = _CustomCurrencyRepository.GetAll().Where(p => p.CurrencyId == input.CurrencyId && p.TenantId == tenantid).FirstOrDefault();
                    if (currency == null)
                    {
                        exchangerate = _CurrencyRepository.GetAll().Where(p => p.Id == input.CurrencyId).Select(p => p.ConversionRatio).FirstOrDefault();
                    }
                    else
                    {
                        exchangerate = currency.ConversionRatio;
                    }
                }

                if (input.EnquiryId > 0 && (input.MileStoneId == 0 || input.MileStoneId == null))
                {
                    var enquiryMile = (from r in _EnquiryRepository.GetAll().Where(p => p.Id == input.EnquiryId) select r.MileStoneId).FirstOrDefault();
                    input.MileStoneId = enquiryMile;
                }

                input.ExchangeRate = exchangerate;
                var val = _QuotationRepository
                 .GetAll().Where(p => (p.SubjectName == input.SubjectName && p.Revised != true) && p.Id != input.Id).FirstOrDefault();

                var oldquotation = _QuotationRepository.GetAll().AsNoTracking().Where(u => u.Id == input.Id).FirstOrDefault();

                if (oldquotation.Vat == true && input.Vat == false)
                {
                    input.VatAmount = 0;
                }
                else if (oldquotation.Vat == false && input.Vat == true)
                {
                    if (input.OverallDiscount > 0)
                    {
                        input.VatAmount = ((input.Total - input.OverallDiscountinUSD) * input.VatPercentage) / 100;
                    }
                    else
                    {
                        input.VatAmount = (input.Total * input.VatPercentage) / 100;
                    }
                }
                if (oldquotation.StatusId != input.StatusId)
                {
                    var CQuotationStatus = _QuotationStatusRepository.GetAll().Where(p => p.Id == input.StatusId).FirstOrDefault();

                    if (CQuotationStatus.Submitted == true)
                    {
                        input.Submitted = true;
                        input.Revised = false;
                        input.MileStoneId = CQuotationStatus.MileStoneId != null ? CQuotationStatus.MileStoneId : input.MileStoneId;
                        input.SubmittedDate = DateTime.Now;
                    }
                    else if (CQuotationStatus.Won == true)
                    {
                        input.Won = true;
                        input.MileStoneId = CQuotationStatus.MileStoneId != null ? CQuotationStatus.MileStoneId : input.MileStoneId;
                        input.WonDate = DateTime.Now;
                    }
                    else if (CQuotationStatus.Lost == true)
                    {
                        input.Lost = true;
                        input.MileStoneId = CQuotationStatus.MileStoneId != null ? CQuotationStatus.MileStoneId : input.MileStoneId;
                        input.LostDate = DateTime.Now;
                    }
                    else if (CQuotationStatus.Revised == true)
                    {
                        input.Revised = true;
                        input.Submitted = false;
                        input.MileStoneId = CQuotationStatus.MileStoneId != null ? CQuotationStatus.MileStoneId : input.MileStoneId;
                    }
                }

                if (input.OverallDiscount > 0)
                {
                    input.OverallDiscountinUSD = Math.Round((input.OverallDiscount / input.ExchangeRate), 3);
                    input.OverallDiscount = Math.Round((input.OverallDiscountinUSD * input.ExchangeRate), 2);
                }
                else
                {
                    input.OverallDiscountinUSD = 0;
                }

                var quotation = input.MapTo<Quotation>();

                if (val == null)
                {

                    int Sid = _QuotationStatusRepository.GetAll().Where(o => o.New == true).Select(o => o.Id).DefaultIfEmpty().First();
                    if (input.StatusId != Sid && input.Total < 1)
                    {
                        throw new UserFriendlyException("Ooops!", "Invalid Update...");
                    }
                    else
                    {
                        if (input.Revised == true && oldquotation.Revised != true)
                        {
                            input.Id = await QuotationRevision(input.Id);
                        }
                        else
                        {
                            quotation.CreatorUserId = oldquotation.CreatorUserId;
                            quotation.CreationTime = oldquotation.CreationTime;
                            quotation.Date = oldquotation.Date;

                            await _QuotationRepository.UpdateAsync(quotation);
                        }
                    }

                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured...");
                }
                return input.Id;
            }
        }
        public void QuotationRevoke(EntityDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                ConnectionAppService db = new ConnectionAppService();
                DataTable ds = new DataTable();
                using (SqlConnection con = new SqlConnection(db.ConnectionString()))
                {
                    SqlCommand sqlComm = new SqlCommand("Sp_RevokeQuotation", con);
                    sqlComm.Parameters.AddWithValue("@QuotationId", input.Id);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    sqlComm.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public async Task<PagedResultDto<QuotationList>> GetRevisionQuotation(GetRevisionQuotationInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var QuotationDate = (from q in _QuotationRepository.GetAll() where q.Id == input.QuotationId && q.Revised != true select q.Date).FirstOrDefault();
                var query = _QuotationRepository.GetAll().Where(r => r.Id == 0);
                if (QuotationDate != null)
                {
                    query = _QuotationRepository.GetAll().Where(r => r.Revised == true && r.Date == QuotationDate)
                    .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.SubjectName.Contains(input.Filter) ||
                         p.Enquirys.Title.Contains(input.Filter) ||
                         p.QuotationTitle.Name.Contains(input.Filter) ||
                         p.Companys.Name.Contains(input.Filter) ||
                         p.Status.QuotationStatusName.Contains(input.Filter) ||
                         p.ProposalNumber.Contains(input.Filter)
                    );

                }
               
                var RevQuotation = (from a in query
                                    select new QuotationList
                                    {
                                        Id = a.Id,
                                        SubjectName = a.SubjectName,
                                        ProposalNumber = a.ProposalNumber,
                                        ProjectRef = a.ProjectRef,
                                        Date = a.Date,
                                        DateString = a.CreationTime.ToString(),
                                        ClosureDate = a.ClosureDate,
                                        Revised = a.Revised,
                                        Archived = a.Archived,
                                        Total = a.Vat == true ? Math.Round((a.ExchangeRate * (a.VatAmount + a.Total - a.OverallDiscountinUSD)), 2) : Math.Round((a.ExchangeRate * (a.Total - a.OverallDiscountinUSD)), 2),
                                        SalesOrderNumber = a.SalesOrderNumber,
                                        LostDate = a.LostDate,
                                        OverallDiscount = a.OverallDiscount,
                                        CustomerPONumber = a.CustomerPONumber,
                                        ExchangeRate = a.ExchangeRate,
                                        EnquiryId = a.EnquiryId,
                                        EnquiryTitle = a.EnquiryId != null ? a.Enquirys.Title : "",
                                        QuotationTitleId = a.QuotationTitleId,
                                        QuotationTitleName = a.QuotationTitleId != null ? a.QuotationTitle.Code : "",
                                        CompanyId = a.CompanyId,
                                        CompanyName = a.CompanyId != null ? a.Companys.Name : "",
                                        StatusId = a.StatusId,
                                        StatusName = a.StatusId != null ? a.Status.QuotationStatusName : "",
                                        FreightId = a.FreightId,
                                        FreightName = a.FreightId != null ? a.Freights.FreightName : "",
                                        PaymentId = a.PaymentId,
                                        PaymentName = a.PackingId != null ? a.Payment.PaymentName : "",
                                        PackingId = a.PackingId,
                                        PackingName = a.PackingId != null ? a.Packings.PackingName : "",
                                        WarrantyId = a.WarrantyId,
                                        WarrantyName = a.WarrantyId != null ? a.Warrantys.WarrantyName : "",
                                        ValidityId = a.ValidityId,
                                        ValidityName = a.ValidityId != null ? a.Validitys.ValidityName : "",
                                        DeliveryId = a.DeliveryId,
                                        DeliveryName = a.DeliveryId != null ? a.Deliverys.DeliveryName : "",
                                        CurrencyId = a.CurrencyId,
                                        CurrencyName = a.CurrencyId != null ? a.Currencys.Name : "",
                                        SalesmanId = a.SalesmanId,
                                        ReasonId = a.ReasonId,
                                        Submitted = a.Submitted,
                                        Won = a.Won,
                                        Lost = a.Lost,
                                        WonDate = a.WonDate,
                                        SubmittedDate = a.SubmittedDate,
                                        ReasonName = a.ReasonId != null ? a.Reasons.Name : "",
                                        ContactId = a.ContactId,
                                        ContactName = a.ContactId != null ? a.Contacts.Name + a.Contacts.LastName : "",
                                        Vat = a.Vat,
                                        VatPercentage = a.VatPercentage,
                                        VatAmount = a.VatAmount
                                    });
                var revQuotationCount = await RevQuotation.CountAsync();
                var revQuotationlist = await RevQuotation
                    .OrderByDescending(p => p.DateString)
                    .PageBy(input)
                    .ToListAsync();

                var revQuotationlistoutput = revQuotationlist.MapTo<List<QuotationList>>();

                return new PagedResultDto<QuotationList>(revQuotationCount, revQuotationlistoutput);
            }
        }


    }
    public class UpdateQuotationTotal
    {
        public int QuotationId { get; set; }
    }
}
