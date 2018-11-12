using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Quotations;
using tibs.stem.Report.Dto;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using tibs.stem.QuotationStatuss;
using System.Data;
using System.Data.SqlClient;
using tibs.stem.Dto;
using tibs.stem.Report.Exporting;
using tibs.stem.Reports;
using Abp.UI;

namespace tibs.stem.Report
{
    public class ReportAppService : stemAppServiceBase, IReportAppService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<Quotation> _QuotationRepository;
        private readonly IRepository<QuotationStatus> _QuotationStatusRepository;
        private readonly ISubmittedReportExcelExporter _SubmittedReportExcelExporter;
        private readonly IWonReportExcelExporter _WonReportExcelExporter;
        private readonly ILostReportExcelExporter _LostReportExcelExporter;
        private readonly IRepository<ReportGenerator> _ReportGeneratorRepository;
        public ReportAppService(
            IUnitOfWorkManager unitOfWorkManager, 
            IAbpSession session, IRepository<ReportGenerator> ReportGeneratorRepository,
            ISubmittedReportExcelExporter SubmittedReportExcelExporter,
            IWonReportExcelExporter WonReportExcelExporter,
            ILostReportExcelExporter LostReportExcelExporter,
            IRepository<Quotation> QuotationRepository,
            IRepository<QuotationStatus> QuotationStatusRepository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _ReportGeneratorRepository = ReportGeneratorRepository;
            _session = session;
            _QuotationRepository = QuotationRepository;
            _QuotationStatusRepository = QuotationStatusRepository;
            _SubmittedReportExcelExporter = SubmittedReportExcelExporter;
            _WonReportExcelExporter = WonReportExcelExporter;
            _LostReportExcelExporter = LostReportExcelExporter;
        }
        public async Task<PagedResultDto<ReportList>> GetQuotationSubmittedReport(GetReportInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                int Submitted = (from r in _QuotationStatusRepository.GetAll().Where(p => p.Submitted == true) select r.Id).FirstOrDefault();
                var query = _QuotationRepository.GetAll().Where(p=> p.StatusId == Submitted && p.Submitted == true && p.Revised != true)
                  .WhereIf(
                  !input.Filter.IsNullOrEmpty(),
                  p => p.SubjectName.Contains(input.Filter) ||
                       p.Enquirys.Title.Contains(input.Filter) ||
                       p.QuotationTitle.Name.Contains(input.Filter) ||
                       p.Companys.Name.Contains(input.Filter) ||
                       p.Status.QuotationStatusName.Contains(input.Filter)
                  );
                var quotation = (from a in query
                                 select new ReportList
                                 {
                                     Id = a.Id,
                                     SubjectName = a.SubjectName,
                                     ProposalNumber = a.ProposalNumber,
                                     ProjectRef = a.ProjectRef,
                                     CreationTime = a.CreationTime.ToString(),
                                     Total = a.Vat == true ? Math.Round((a.ExchangeRate * (a.VatAmount + a.Total - a.OverallDiscountinUSD)), 2) : Math.Round((a.ExchangeRate * (a.Total - a.OverallDiscountinUSD)), 2),
                                     SalesOrderNumber = a.SalesOrderNumber,
                                     LostDate = a.LostDate,
                                     CustomerPONumber = a.CustomerPONumber,
                                     ExchangeRate = a.ExchangeRate,
                                     EnquiryId = a.EnquiryId,
                                     EnquiryName = a.EnquiryId != null ? a.Enquirys.Title : "",
                                     EnquiryNumber =  a.EnquiryId != null ? a.Enquirys.EnquiryNo : "",
                                     EnquiryClosureDate = a.Enquirys.CloseDate != null ? a.Enquirys.CloseDate.ToString() : "",
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
                                     Salesman = a.SalesmanId != null ? a.Salesman.Name : "",
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
                                     VatAmount = a.VatAmount,
                                     MileStoneId = a.MileStoneId,
                                     MileStones = a.MileStoneId != null ? a.MileStones.Name : ""
                                 });

                var quotationSubmittedCount = await quotation.CountAsync();

                var quotationSubmittedlist = await quotation
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();

                var quotationSubmitted = quotationSubmittedlist.MapTo<List<ReportList>>();

                return new PagedResultDto<ReportList>(quotationSubmittedCount, quotationSubmitted);
            }
        }
        public async Task<PagedResultDto<ReportList>> GetQuotationWonReport(GetReportInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                int Won = (from r in _QuotationStatusRepository.GetAll().Where(p => p.Won == true) select r.Id).FirstOrDefault();
                var query = _QuotationRepository.GetAll().Where(p => p.StatusId == Won && p.Won == true)
                  .WhereIf(
                  !input.Filter.IsNullOrEmpty(),
                  p => p.SubjectName.Contains(input.Filter) ||
                       p.Enquirys.Title.Contains(input.Filter) ||
                       p.QuotationTitle.Name.Contains(input.Filter) ||
                       p.Companys.Name.Contains(input.Filter) ||
                       p.Status.QuotationStatusName.Contains(input.Filter)
                  );
                var quotation = (from a in query
                                 select new ReportList
                                 {
                                     Id = a.Id,
                                     SubjectName = a.SubjectName,
                                     ProposalNumber = a.ProposalNumber,
                                     ProjectRef = a.ProjectRef,
                                     CreationTime = a.CreationTime.ToString(),
                                     Total = a.Vat == true ? Math.Round((a.ExchangeRate * (a.VatAmount + a.Total - a.OverallDiscountinUSD)), 2) : Math.Round((a.ExchangeRate * (a.Total - a.OverallDiscountinUSD)), 2),
                                     SalesOrderNumber = a.SalesOrderNumber,
                                     LostDate = a.LostDate,
                                     CustomerPONumber = a.CustomerPONumber,
                                     ExchangeRate = a.ExchangeRate,
                                     EnquiryId = a.EnquiryId,
                                     EnquiryName = a.EnquiryId != null ? a.Enquirys.Title : "",
                                     EnquiryNumber = a.EnquiryId != null ? a.Enquirys.EnquiryNo : "",
                                     EnquiryClosureDate = a.Enquirys.CloseDate != null ? a.Enquirys.CloseDate.ToString() : "",
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
                                     Salesman = a.SalesmanId != null ? a.Salesman.Name : "",
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
                                     VatAmount = a.VatAmount,
                                     MileStoneId = a.MileStoneId,
                                     MileStones = a.MileStones.Name
                                 });

                var quotationWonCount = await quotation.CountAsync();

                var quotationWonlist = await quotation
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();

                var quotationWon = quotationWonlist.MapTo<List<ReportList>>();

                return new PagedResultDto<ReportList>(quotationWonCount, quotationWon);
            }

        }
        public async Task<PagedResultDto<ReportList>> GetQuotationLostReport(GetReportInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                int Lost = (from r in _QuotationStatusRepository.GetAll().Where(p => p.Lost == true) select r.Id).FirstOrDefault();
                var query = _QuotationRepository.GetAll().Where(p => p.StatusId == Lost && p.Lost == true)
                  .WhereIf(
                  !input.Filter.IsNullOrEmpty(),
                  p => p.SubjectName.Contains(input.Filter) ||
                       p.Enquirys.Title.Contains(input.Filter) ||
                       p.QuotationTitle.Name.Contains(input.Filter) ||
                       p.Companys.Name.Contains(input.Filter) ||
                       p.Status.QuotationStatusName.Contains(input.Filter)
                  );
                var quotation = (from a in query
                                 select new ReportList
                                 {
                                     Id = a.Id,
                                     SubjectName = a.SubjectName,
                                     ProposalNumber = a.ProposalNumber,
                                     ProjectRef = a.ProjectRef,
                                     CreationTime = a.CreationTime.ToString(),
                                     Total = a.Vat == true ? Math.Round((a.ExchangeRate * (a.VatAmount + a.Total - a.OverallDiscountinUSD)), 2) : Math.Round((a.ExchangeRate * (a.Total - a.OverallDiscountinUSD)), 2),
                                     SalesOrderNumber = a.SalesOrderNumber,
                                     LostDate = a.LostDate,
                                     CustomerPONumber = a.CustomerPONumber,
                                     ExchangeRate = a.ExchangeRate,
                                     EnquiryId = a.EnquiryId,
                                     EnquiryName = a.EnquiryId != null ? a.Enquirys.Title : "",
                                     EnquiryNumber = a.EnquiryId != null ? a.Enquirys.EnquiryNo : "",
                                     EnquiryClosureDate = a.Enquirys.CloseDate != null ? a.Enquirys.CloseDate.ToString() : "",
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
                                     Salesman = a.SalesmanId != null ? a.Salesman.Name : "",
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
                                     VatAmount = a.VatAmount,
                                     MileStoneId = a.MileStoneId,
                                     MileStones = a.MileStones.Name
                                 });

                var quotationLostCount = await quotation.CountAsync();

                var quotationLostlist = await quotation
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();

                var quotationLost = quotationLostlist.MapTo<List<ReportList>>();

                return new PagedResultDto<ReportList>(quotationLostCount, quotationLost);
            }

        }
        public async Task<PagedResultDto<QuotationReportDtoList>> GetQuotationReport(GetReportInput input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                ConnectionAppService db = new ConnectionAppService();
                DataTable dt = new DataTable();
                SqlConnection con3 = new SqlConnection(db.ConnectionString());
                SqlCommand sqlComm = new SqlCommand("Sp_QuotationFilter", con3);
                sqlComm.Parameters.AddWithValue("@TenantId", TenantId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlComm))
                {
                    sda.Fill(dt);
                }

                var QuotationReport = (from DataRow dr in dt.Rows
                                       select new QuotationReportDtoList
                                       {
                                           QuotationId = Convert.ToInt32(dr["QuotationId"]),
                                           EnquiryId = Convert.ToInt32(dr["Id"]),
                                           EnquiryTitle = Convert.ToString(dr["Title"]),
                                           EnquiryNo = Convert.ToString(dr["EnquiryNo"]),
                                           MileStones = Convert.ToString(dr["MileStoneName"]),
                                           CompanyName = Convert.ToString(dr["CompanyName"]),
                                           ContactName = Convert.ToString(dr["ContactName"]),
                                           MileStoneStatus = Convert.ToString(dr["MileStoneStatusName"]),
                                           Creator = Convert.ToString(dr["Creator"]),
                                           CreationTime = Convert.ToDateTime(dr["CreationTime"]),
                                           Salesperson = Convert.ToString(dr["Salesperson"]),
                                           EnquiryClosureDate = Convert.ToDateTime(dr["CloseDate"]),
                                           SubjectName = Convert.ToString(dr["SubjectName"]),
                                           QRefno = Convert.ToString(dr["QRefno"]),
                                           StatusName = Convert.ToString(dr["QuotationStatusName"]),
                                           Total = Convert.ToDecimal(dr["Total"]),
                                           SubmittedDate = Convert.ToDateTime(dr["SubmittedDate"]),
                                           WonDate = Convert.ToDateTime(dr["WonDate"]),
                                           LostDate = Convert.ToDateTime(dr["LostDate"])
                                       });

                QuotationReport = QuotationReport.WhereIf(
                      !input.Filter.IsNullOrEmpty(),
                      p =>
                           p.EnquiryTitle.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.EnquiryNo.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.CompanyName.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.SubjectName.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.QRefno.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty))
                      );

                var QuotationReportCount = QuotationReport.Count();

                var QuotationReportList = QuotationReport.OrderByDescending(p => p.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

                var QuotationReportdtos = QuotationReportList.MapTo<List<QuotationReportDtoList>>();

                return new PagedResultDto<QuotationReportDtoList>(QuotationReportCount, QuotationReportdtos);
            }
        }
        public async Task<PagedResultDto<QuotationReportDtoList>> GetInquiryReport(GetReportInput input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                ConnectionAppService db = new ConnectionAppService();
                DataTable dt = new DataTable();
                SqlConnection con3 = new SqlConnection(db.ConnectionString());
                SqlCommand sqlComm = new SqlCommand("Sp_EnquiryFilter", con3);
                sqlComm.Parameters.AddWithValue("@TenantId", TenantId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlComm))
                {
                    sda.Fill(dt);
                }

                var NormalTicket = (from DataRow dr in dt.Rows
                                    select new QuotationReportDtoList
                                    {
                                        EnquiryId = Convert.ToInt32(dr["Id"]),
                                        EnquiryTitle = Convert.ToString(dr["Title"]),
                                        EnquiryNo = Convert.ToString(dr["EnquiryNo"]),
                                        MileStones = Convert.ToString(dr["MileStoneName"]),
                                        CompanyName = Convert.ToString(dr["CompanyName"]),
                                        ContactName = Convert.ToString(dr["ContactName"]),
                                        MileStoneStatus = Convert.ToString(dr["MileStoneStatusName"]),
                                        Remarks = Convert.ToString(dr["Remarks"]),
                                        Creator = Convert.ToString(dr["Creator"]),
                                        CreationTime = Convert.ToDateTime(dr["CreationTime"]),
                                        Salesperson = Convert.ToString(dr["Salesperson"]),
                                        EnquiryClosureDate = Convert.ToDateTime(dr["CloseDate"]),
                                        QuotationId = Convert.ToInt32(dr["QuotationId"]),
                                        QRefno = Convert.ToString(dr["QRefno"]),
                                        Total = Convert.ToDecimal(dr["Total"])
                                    });

                NormalTicket = NormalTicket.WhereIf(
                      !input.Filter.IsNullOrEmpty(),
                      p =>
                           p.EnquiryTitle.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.EnquiryNo.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.CompanyName.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty))
                      );

                var NewStatussCount = NormalTicket.Count();

                var NewStatussList = NormalTicket.OrderByDescending(p => p.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

                var NewStatusdtos = NewStatussList.MapTo<List<QuotationReportDtoList>>();

                return new PagedResultDto<QuotationReportDtoList>(NewStatussCount, NewStatusdtos);
            }
        }
        public async Task<PagedResultDto<CompanyReportList>> GetCompanyReport(GetReportInput input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                ConnectionAppService db = new ConnectionAppService();
                DataTable dt = new DataTable();
                SqlConnection con3 = new SqlConnection(db.ConnectionString());
                SqlCommand sqlComm = new SqlCommand("Sp_CompanyFilter", con3);
                sqlComm.Parameters.AddWithValue("@TenantId", TenantId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlComm))
                {
                    sda.Fill(dt);
                }

                var CompanyReport = (from DataRow dr in dt.Rows
                                     select new CompanyReportList
                                     {
                                         Id = Convert.ToInt32(dr["Id"]),
                                         Name = Convert.ToString(dr["Name"]),
                                         Salesperson = Convert.ToString(dr["Salesperson"]),
                                         Creator = Convert.ToString(dr["Creator"]),
                                         CreationTime = Convert.ToDateTime(dr["CreationTime"]),
                                         CountryOrCompanyName = Convert.ToString(dr["CountryName"]),
                                         CurrencyOrTitleName = Convert.ToString(dr["CurrencyName"]),
                                         CustomerType = Convert.ToString(dr["CustomerType"]),
                                         EnquiryCount = Convert.ToInt32(dr["EnquiryCount"]),
                                         QuotationCount = Convert.ToInt32(dr["QuotationCount"])
                                     });

                CompanyReport = CompanyReport.WhereIf(
                      !input.Filter.IsNullOrEmpty(),
                      p =>
                           p.Name.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.Salesperson.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.CountryOrCompanyName.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty))
                      );

                var CompanyReportCount = CompanyReport.Count();

                var CompanyReportLists = CompanyReport.OrderByDescending(p => p.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

                var CompanyReportdtos = CompanyReportLists.MapTo<List<CompanyReportList>>();

                return new PagedResultDto<CompanyReportList>(CompanyReportCount, CompanyReportdtos);
            }
        }
        public async Task<PagedResultDto<CompanyReportList>> GetContactReport(GetReportInput input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                ConnectionAppService db = new ConnectionAppService();
                DataTable dt = new DataTable();
                SqlConnection con3 = new SqlConnection(db.ConnectionString());
                SqlCommand sqlComm = new SqlCommand("Sp_ContactFilter", con3);
                sqlComm.Parameters.AddWithValue("@TenantId", TenantId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlComm))
                {
                    sda.Fill(dt);
                }

                var ContactReport = (from DataRow dr in dt.Rows
                                     select new CompanyReportList
                                     {
                                         Id = Convert.ToInt32(dr["Id"]),
                                         Name = Convert.ToString(dr["Name"]),
                                         Salesperson = Convert.ToString(dr["Salesperson"]),
                                         Creator = Convert.ToString(dr["Creator"]),
                                         CreationTime = Convert.ToDateTime(dr["CreationTime"]),
                                         CountryOrCompanyName = Convert.ToString(dr["CompanyName"]),
                                         CurrencyOrTitleName = Convert.ToString(dr["TitleName"]),
                                         CustomerType = Convert.ToString(dr["CustomerType"])
                                     });

                ContactReport = ContactReport.WhereIf(
                      !input.Filter.IsNullOrEmpty(),
                      p =>
                           p.Name.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.Salesperson.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.CountryOrCompanyName.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty))
                      );

                var ContactReportCount = ContactReport.Count();

                var ContactReportLists = ContactReport.OrderByDescending(p => p.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

                var ContactReportdtos = ContactReportLists.MapTo<List<CompanyReportList>>();

                return new PagedResultDto<CompanyReportList>(ContactReportCount, ContactReportdtos);
            }
        }
        public async Task<Array> GetGlobalReportColumn()
        {
            List<ReportColumn> GlobalReportColumn = new List<ReportColumn>()
            {
                new ReportColumn(){ ColumnId=1, ColumnName="Enquiry Title", Active=true},
                new ReportColumn(){ ColumnId=2, ColumnName="Enquiry No", Active=true},
                new ReportColumn(){ ColumnId=3, ColumnName="Quotation Ref No", Active=true},
                new ReportColumn(){ ColumnId=4, ColumnName="MileStone Status", Active=true},
                new ReportColumn(){ ColumnId=5, ColumnName="Salesperson", Active=true},
                new ReportColumn(){ ColumnId=6, ColumnName="Company", Active=true},
                new ReportColumn(){ ColumnId=7, ColumnName="Contact", Active=true},
                new ReportColumn(){ ColumnId=8, ColumnName="Closure Date", Active=true},
                new ReportColumn(){ ColumnId=9, ColumnName="Total", Active=true},
                new ReportColumn(){ ColumnId=10, ColumnName="Remarks", Active=true},
                new ReportColumn(){ ColumnId=11, ColumnName="Created By", Active=true},
                new ReportColumn(){ ColumnId=12, ColumnName="Creation Time", Active=true}
            };

            return GlobalReportColumn.ToArray();
        }
        public async Task<FileDto> GetSubmittedReportToExcel()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                int Submitted = (from r in _QuotationStatusRepository.GetAll().Where(p => p.Submitted == true) select r.Id).FirstOrDefault();
                var query = _QuotationRepository.GetAll().Where(p => p.StatusId == Submitted && p.Submitted == true);

                var quotation = (from a in query
                                 select new ReportList
                                 {
                                     Id = a.Id,
                                     SubjectName = a.SubjectName,
                                     ProposalNumber = a.ProposalNumber,
                                     ProjectRef = a.ProjectRef,
                                     CreationTime = a.CreationTime.ToString(),
                                     Total = a.Vat == true ? (a.VatAmount + Math.Round(((a.Total * a.ExchangeRate) - a.OverallDiscount), 2)) : Math.Round(((a.Total * a.ExchangeRate) - a.OverallDiscount), 2),
                                     SalesOrderNumber = a.SalesOrderNumber,
                                     LostDate = a.LostDate,
                                     CustomerPONumber = a.CustomerPONumber,
                                     ExchangeRate = a.ExchangeRate,
                                     EnquiryId = a.EnquiryId,
                                     EnquiryName = a.EnquiryId != null ? a.Enquirys.Title : "",
                                     EnquiryNumber = a.EnquiryId != null ? a.Enquirys.EnquiryNo : "",
                                     EnquiryClosureDate = a.Enquirys.CloseDate != null ? a.Enquirys.CloseDate.ToString() : "",
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
                                     Salesman = a.SalesmanId != null ? a.Salesman.Name : "",
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
                                     VatAmount = a.VatAmount,
                                     MileStoneId = a.MileStoneId,
                                     MileStones = a.MileStoneId != null ? a.MileStones.Name : ""
                                 });

                var quotationSubmittedCount = await quotation.CountAsync();

                var quotationSubmittedlist = await quotation.OrderByDescending(p => p.CreationTime).ToListAsync();

                var quotationSubmitted = quotationSubmittedlist.MapTo<List<ReportList>>();

                return _SubmittedReportExcelExporter.ExportToFile(quotationSubmitted);
            }
        }
        public async Task<FileDto> GetWonReportToExcel()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                int Won = (from r in _QuotationStatusRepository.GetAll().Where(p => p.Won == true) select r.Id).FirstOrDefault();
                var query = _QuotationRepository.GetAll().Where(p => p.StatusId == Won && p.Won == true);
                var quotation = (from a in query
                                 select new ReportList
                                 {
                                     Id = a.Id,
                                     SubjectName = a.SubjectName,
                                     ProposalNumber = a.ProposalNumber,
                                     ProjectRef = a.ProjectRef,
                                     CreationTime = a.CreationTime.ToString(),
                                     Total = a.Vat == true ? (a.VatAmount + Math.Round(((a.Total * a.ExchangeRate) - a.OverallDiscount), 2)) : Math.Round(((a.Total * a.ExchangeRate) - a.OverallDiscount), 2),
                                     SalesOrderNumber = a.SalesOrderNumber,
                                     LostDate = a.LostDate,
                                     CustomerPONumber = a.CustomerPONumber,
                                     ExchangeRate = a.ExchangeRate,
                                     EnquiryId = a.EnquiryId,
                                     EnquiryName = a.EnquiryId != null ? a.Enquirys.Title : "",
                                     EnquiryNumber = a.EnquiryId != null ? a.Enquirys.EnquiryNo : "",
                                     EnquiryClosureDate = a.Enquirys.CloseDate != null ? a.Enquirys.CloseDate.ToString() : "",
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
                                     Salesman = a.SalesmanId != null ? a.Salesman.Name : "",
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
                                     VatAmount = a.VatAmount,
                                     MileStoneId = a.MileStoneId,
                                     MileStones = a.MileStones.Name
                                 });

                var quotationWonCount = await quotation.CountAsync();

                var quotationWonlist = await quotation.OrderByDescending(p => p.CreationTime).ToListAsync();

                var quotationWon = quotationWonlist.MapTo<List<ReportList>>();

                return _WonReportExcelExporter.ExportToFile(quotationWon);
            }
        }
        public async Task<FileDto> GetLostReportToExcel()
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                int Lost = (from r in _QuotationStatusRepository.GetAll().Where(p => p.Lost == true) select r.Id).FirstOrDefault();
                var query = _QuotationRepository.GetAll().Where(p => p.StatusId == Lost && p.Lost == true);
                var quotation = (from a in query
                                 select new ReportList
                                 {
                                     Id = a.Id,
                                     SubjectName = a.SubjectName,
                                     ProposalNumber = a.ProposalNumber,
                                     ProjectRef = a.ProjectRef,
                                     CreationTime = a.CreationTime.ToString(),
                                     Total = a.Vat == true ? (a.VatAmount + Math.Round(((a.Total * a.ExchangeRate) - a.OverallDiscount), 2)) : Math.Round(((a.Total * a.ExchangeRate) - a.OverallDiscount), 2),
                                     SalesOrderNumber = a.SalesOrderNumber,
                                     LostDate = a.LostDate,
                                     CustomerPONumber = a.CustomerPONumber,
                                     ExchangeRate = a.ExchangeRate,
                                     EnquiryId = a.EnquiryId,
                                     EnquiryName = a.EnquiryId != null ? a.Enquirys.Title : "",
                                     EnquiryNumber = a.EnquiryId != null ? a.Enquirys.EnquiryNo : "",
                                     EnquiryClosureDate = a.Enquirys.CloseDate != null ? a.Enquirys.CloseDate.ToString() : "",
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
                                     Salesman = a.SalesmanId != null ? a.Salesman.Name : "",
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
                                     VatAmount = a.VatAmount,
                                     MileStoneId = a.MileStoneId,
                                     MileStones = a.MileStones.Name
                                 });

                var quotationLostCount = await quotation.CountAsync();

                var quotationLostlist = await quotation.OrderByDescending(p => p.CreationTime).ToListAsync();

                var quotationLost = quotationLostlist.MapTo<List<ReportList>>();

                return _LostReportExcelExporter.ExportToFile(quotationLost);
            }

        }
        public virtual async Task CreateReport(ReportGeneratorInput input)
        {
            input.TenantId = (int)(AbpSession.TenantId);
            var NewReport = input.MapTo<ReportGenerator>();
            var query = _ReportGeneratorRepository.GetAll().Where(p => p.Name == input.Name && p.TenantId == input.TenantId).FirstOrDefault();
            if (query == null)
            {
                await _ReportGeneratorRepository.InsertAsync(NewReport);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Report Name...");
            }

        }
        public async Task<PagedResultDto<FilterListOutput>> GetFilterReport(GetFilterReportInput input)
        {
            int TenantId = (int)(_session.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                try
                {
                    ConnectionAppService db = new ConnectionAppService();
                    DataTable dt = new DataTable();
                    SqlConnection con3 = new SqlConnection(db.ConnectionString());
                    SqlCommand sqlComm = new SqlCommand("Sp_ReportFilterView", con3);
                    sqlComm.Parameters.AddWithValue("@TenantId", TenantId);
                    sqlComm.Parameters.AddWithValue("@ReportTypeId", input.ReportTypeId);
                    sqlComm.Parameters.AddWithValue("@ReportViewId", input.ReportViewId);
                    sqlComm.Parameters.AddWithValue("@Salesperson", input.Salesperson);
                    sqlComm.Parameters.AddWithValue("@Creator", input.Creator);
                    sqlComm.Parameters.AddWithValue("@Country", input.Country);
                    sqlComm.Parameters.AddWithValue("@Currency", input.Currency);
                    sqlComm.Parameters.AddWithValue("@CustomerType", input.CustomerType);
                    sqlComm.Parameters.AddWithValue("@MileStone", input.MileStone);
                    sqlComm.Parameters.AddWithValue("@MileStoneStatus", input.MileStoneStatus);
                    sqlComm.Parameters.AddWithValue("@QuotationStatus", input.QuotationStatus);
                    sqlComm.Parameters.AddWithValue("@EnquiryCreationTime", input.EnquiryCreationTime);
                    sqlComm.Parameters.AddWithValue("@EnquiryCreationTimeId", input.EnquiryCreationTimeId);
                    sqlComm.Parameters.AddWithValue("@QuotationCreationTime", input.QuotationCreationTime);
                    sqlComm.Parameters.AddWithValue("@QuotationCreationTimeId", input.QuotationCreationTimeId);
                    sqlComm.Parameters.AddWithValue("@SubmittedDate", input.SubmittedDate);
                    sqlComm.Parameters.AddWithValue("@SubmittedDateId", input.SubmittedDateId);
                    sqlComm.Parameters.AddWithValue("@WonDate", input.WonDate);
                    sqlComm.Parameters.AddWithValue("@WonDateId", input.WonDateId);
                    sqlComm.Parameters.AddWithValue("@LostDate", input.LostDate);
                    sqlComm.Parameters.AddWithValue("@LostDateId", input.LostDateId);

                    sqlComm.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter(sqlComm))
                    {
                        sda.Fill(dt);
                    }

                    var FilterReport = (from DataRow dr in dt.Rows
                                        select new FilterListOutput
                                        {
                                            CompanyId = Convert.ToInt32(dr["CompanyId"]),
                                            Company = Convert.ToString(dr["CompanyName"]),
                                            Salesperson = Convert.ToString(dr["Salesperson"]),
                                            Creator = Convert.ToString(dr["Creator"]),
                                            CreationTime = Convert.ToDateTime(dr["CreationTime"]),
                                            Country = Convert.ToString(dr["CountryName"]),
                                            Currency = Convert.ToString(dr["CurrencyName"]),
                                            CustomerType = Convert.ToString(dr["CustomerType"]),
                                            EnquiryCount = Convert.ToInt32(dr["EnquiryCount"]),
                                            QuotationCount = Convert.ToInt32(dr["QuotationCount"]),
                                            ContactId = Convert.ToInt32(dr["ContactId"]),
                                            Contact = Convert.ToString(dr["ContactName"]),
                                            TitleName = Convert.ToString(dr["TitleName"]),
                                            QuotationId = Convert.ToInt32(dr["QuotationId"]),
                                            EnquiryId = Convert.ToInt32(dr["Id"]),
                                            EnquiryTitle = Convert.ToString(dr["EnquiryTitle"]),
                                            EnquiryNo = Convert.ToString(dr["EnquiryNo"]),
                                            MileStones = Convert.ToString(dr["MileStoneName"]),
                                            MileStoneStatus = Convert.ToString(dr["MileStoneStatusName"]),
                                            EnquiryClosureDate = Convert.ToDateTime(dr["CloseDate"]),
                                            SubjectName = Convert.ToString(dr["SubjectName"]),
                                            QRefno = Convert.ToString(dr["QRefno"]),
                                            StatusName = Convert.ToString(dr["QuotationStatusName"]),
                                            Total = Convert.ToDecimal(dr["Total"]),
                                            SubmittedDate = Convert.ToDateTime(dr["SubmittedDate"]),
                                            WonDate = Convert.ToDateTime(dr["WonDate"]),
                                            LostDate = Convert.ToDateTime(dr["LostDate"]),
                                            Remarks = Convert.ToString(dr["Remarks"])
                                        });

                    var FilterReportCount = FilterReport.Count();

                    var FilterReportLists = FilterReport.OrderByDescending(p => p.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

                    var ReportOutput = FilterReportLists.MapTo<List<FilterListOutput>>();

                    return new PagedResultDto<FilterListOutput>(FilterReportCount, ReportOutput);

                }
                catch (Exception EX)
                {
                    throw EX;
                }

            }
        }
        public async Task<Array> GetEnquiryReportColumn()
        {
            List<ReportColumn> EnquiryReportColumn = new List<ReportColumn>()
            {
                new ReportColumn(){ ColumnId=1, ColumnName="Enquiry Title", Active=true},
                new ReportColumn(){ ColumnId=2, ColumnName="Enquiry No", Active=true},
                new ReportColumn(){ ColumnId=3, ColumnName="MileStone", Active=true},
                new ReportColumn(){ ColumnId=4, ColumnName="MileStone Status", Active=true},
                new ReportColumn(){ ColumnId=5, ColumnName="Salesperson", Active=true},
                new ReportColumn(){ ColumnId=6, ColumnName="Company", Active=true},
                new ReportColumn(){ ColumnId=7, ColumnName="Contact", Active=true},
                new ReportColumn(){ ColumnId=8, ColumnName="Closure Date", Active=true},
                new ReportColumn(){ ColumnId=9, ColumnName="Total", Active=true},
                new ReportColumn(){ ColumnId=10, ColumnName="Remarks", Active=true},
                new ReportColumn(){ ColumnId=11, ColumnName="Created By", Active=true},
                new ReportColumn(){ ColumnId=12, ColumnName="Creation Time", Active=true}
            };

            return EnquiryReportColumn.ToArray();
        }
        public async Task<Array> GetQuotationReportColumn()
        {
            List<ReportColumn> EnquiryReportColumn = new List<ReportColumn>()
            {
                new ReportColumn(){ ColumnId=1, ColumnName="Subject", Active=true},
                new ReportColumn(){ ColumnId=2, ColumnName="Ref No", Active=true},
                new ReportColumn(){ ColumnId=3, ColumnName="Enquiry Title", Active=true},
                new ReportColumn(){ ColumnId=4, ColumnName="Enquiry No", Active=true},
                new ReportColumn(){ ColumnId=5, ColumnName="MileStone", Active=true},
                new ReportColumn(){ ColumnId=6, ColumnName="MileStone Status", Active=true},
                new ReportColumn(){ ColumnId=7, ColumnName="Salesperson", Active=true},
                new ReportColumn(){ ColumnId=8, ColumnName="Company", Active=true},
                new ReportColumn(){ ColumnId=9, ColumnName="Contact", Active=true},
                new ReportColumn(){ ColumnId=10, ColumnName="Quotation Status", Active=true},
                new ReportColumn(){ ColumnId=11, ColumnName="Submitted Date", Active=true},
                new ReportColumn(){ ColumnId=12, ColumnName="Won Date", Active=true},
                new ReportColumn(){ ColumnId=13, ColumnName="Lost Date", Active=true},
                new ReportColumn(){ ColumnId=14, ColumnName="Closure Date", Active=true},
                new ReportColumn(){ ColumnId=15, ColumnName="Total", Active=true},
                new ReportColumn(){ ColumnId=16, ColumnName="Created By", Active=true},
                new ReportColumn(){ ColumnId=17, ColumnName="Creation Time", Active=true}
            };

            return EnquiryReportColumn.ToArray();
        }
        public async Task<Array> GetCompanyReportColumn()
        {
            List<ReportColumn> CompanyReportColumn = new List<ReportColumn>()
            {
                new ReportColumn(){ ColumnId=1, ColumnName="Company", Active=true},
                new ReportColumn(){ ColumnId=2, ColumnName="Salesperson", Active=true},
                new ReportColumn(){ ColumnId=3, ColumnName="Country", Active=true},
                new ReportColumn(){ ColumnId=4, ColumnName="Currency", Active=true},
                new ReportColumn(){ ColumnId=5, ColumnName="Customer Type", Active=true},
                new ReportColumn(){ ColumnId=6, ColumnName="Enquiry Count", Active=true},
                new ReportColumn(){ ColumnId=7, ColumnName="Quotation Count", Active=true},
                new ReportColumn(){ ColumnId=8, ColumnName="Created By", Active=true},
                new ReportColumn(){ ColumnId=9, ColumnName="Creation Time", Active=true}
            };

            return CompanyReportColumn.ToArray();
        }
        public async Task<Array> GetContactReportColumn()
        {
            List<ReportColumn> ContactReportColumn = new List<ReportColumn>()
            {
                new ReportColumn(){ ColumnId=1, ColumnName="Title", Active=true},
                new ReportColumn(){ ColumnId=2, ColumnName="Name", Active=true},
                new ReportColumn(){ ColumnId=3, ColumnName="Company", Active=true},
                new ReportColumn(){ ColumnId=4, ColumnName="Salesperson", Active=true},
                new ReportColumn(){ ColumnId=5, ColumnName="Customer Type", Active=true},
                new ReportColumn(){ ColumnId=6, ColumnName="Created By", Active=true},
                new ReportColumn(){ ColumnId=7, ColumnName="Creation Time", Active=true}
            };

            return ContactReportColumn.ToArray();
        }

        public class ReportColumn
        {
            public int ColumnId { get; set; }
            public string ColumnName { get; set; }
            public bool Active { get; set; }

        }
    }
}
