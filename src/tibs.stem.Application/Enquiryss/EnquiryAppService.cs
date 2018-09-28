using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using tibs.stem.CustomerCompanys;
using tibs.stem.Enquirys;
using tibs.stem.Enquiryss.Dto;
using tibs.stem.MileStones;
using tibs.stem.MileStoneStatuss;
using Abp.Domain.Uow;
using tibs.stem;
using tibs.stem.Enquiryss.Exporting;
using tibs.stem.Dto;
using tibs.stem.Quotations;
using tibs.stem.Quotationss.Dto;
using System.Data;
using System.Data.SqlClient;
using Abp.Authorization;
using tibs.stem.Authorization;

namespace tibs.stem.Enquiryss
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Leads_Leads)]
    public class EnquiryAppService : stemAppServiceBase, IEnquiryAppService
    {
        private readonly IEnquiryExcelExporter _enquiryExcelExporter;
        private readonly IRepository<Company> _NewCompanyRepository;
        private readonly IRepository<Contact> _NewContactRepository;
        public readonly IRepository<MileStone> _MilestoneRepository;
        private readonly IRepository<MileStoneStatus> _MileStoneStatusRepository;
        private readonly IRepository<Enquiry> _EnquiryRepository;
        private readonly IRepository<Quotation> _QuotationRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IGlobalReportExcelExporter _GlobalReportExcelExporter;

        public EnquiryAppService(IRepository<Quotation> QuotationRepository, IGlobalReportExcelExporter GlobalReportExcelExporter, IEnquiryExcelExporter enquiryExcelExporter, IRepository<Enquiry> EnquiryRepository, IUnitOfWorkManager unitOfWorkManager,IRepository<MileStoneStatus> MileStoneStatusRepository, IRepository<MileStone> MilestoneRepository, IRepository<Company> NewCompanyRepository, IRepository<Contact> NewContactRepository)
        {
            _enquiryExcelExporter = enquiryExcelExporter;
            _NewCompanyRepository = NewCompanyRepository;
            _NewContactRepository = NewContactRepository;
            _MilestoneRepository = MilestoneRepository;
            _MileStoneStatusRepository = MileStoneStatusRepository;
            _EnquiryRepository = EnquiryRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _QuotationRepository = QuotationRepository;
            _GlobalReportExcelExporter = GlobalReportExcelExporter;
        }
        public async Task<Array> GetEnquiryQuotationKanbanOld(EnquiryKanbanInput input)
        {
            //int TenantId = 2;
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                var milestone = (from r in _MilestoneRepository.GetAll() select r).ToArray();
                var query = _EnquiryRepository.GetAll()
                    .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p =>
                         p.EnquiryNo.Contains(input.Filter) ||
                         p.Title.Contains(input.Filter) ||
                         p.Companys.Name.Contains(input.Filter) ||
                         p.Contacts.Name.Contains(input.Filter) ||
                         p.MileStones.Name.Contains(input.Filter)
                    );
                var enquiry = (from a in query
                               select new EnquiryQuotationKanbanListOld
                               {
                                   Id = a.Id,
                                   EnquiryNo = a.EnquiryNo,
                                   Title = a.Title,
                                   CompanyId = a.CompanyId,
                                   CompanyName = a.Companys.Name,
                                   ContactId = a.ContactId != null ? a.ContactId : 0,
                                   ContactName = a.ContactId != null ? a.Contacts.Name + " " + a.Contacts.LastName : "",
                                   MileStoneId = a.MileStoneId,
                                   MileStoneName = a.MileStones.Name,
                                   MileStoneStatusId = a.MileStoneStatusId != null ? a.MileStoneStatusId : 0,
                                   MileStoneStatusName = a.MileStoneStatusId != null ? a.MileStoneStatuss.Name : "",
                                   CreationTime = a.CreationTime
                               });
                var enquiryquotation = new List<EnquiryQuotationKanbanListOld>();
                var enquiryquotationkanban = new List<EnquiryQuotationKanbanArrayOld>();
                var quotation = (from a in _QuotationRepository.GetAll()
                               select new QuotationKanbanList
                               {
                                   Id = a.Id,
                                   SubjectName = a.SubjectName,
                                   ProposalNumber = a.ProposalNumber,
                                   EnquiryId = a.EnquiryId,
                                   EnquiryTitle = a.EnquiryId != null ? a.Enquirys.Title : "",
                                   QuotationTitleId = a.QuotationTitleId,
                                   QuotationTitleName = a.QuotationTitleId != null ? a.QuotationTitle.Name : "",
                                   MileStoneId = a.MileStoneId,
                                   CompanyId = a.CompanyId,
                                   CompanyName = a.CompanyId != null ? a.Companys.Name : "",
                                   StatusId = a.StatusId,
                                   StatusName = a.StatusId != null ? a.Status.QuotationStatusName : "",
                                   ContactId = a.ContactId,
                                   ContactName = a.ContactId != null ? a.Contacts.Name + a.Contacts.LastName : "",

                               }).ToList();
                foreach (var enq in enquiry)
                {
                    var qutcon = (from r in quotation where r.EnquiryId == enq.Id select r.MileStoneId).Distinct();
                    var qutcount = qutcon.ToList();

                    if(qutcount.Count > 1)
                    {
                        foreach (var item in qutcon)
                        {
                            var mil = (from r in milestone where r.Id == item select r).FirstOrDefault();
                            enquiryquotation.Add(new EnquiryQuotationKanbanListOld
                            {
                                Id = enq.Id,
                                EnquiryNo = enq.EnquiryNo,
                                Title = enq.Title,
                                CompanyId = enq.CompanyId,
                                CompanyName = enq.CompanyName,
                                ContactId = enq.ContactId,
                                ContactName = enq.ContactName,
                                MileStoneId = mil.Id,
                                MileStoneName = mil.Name,
                                MileStoneStatusId = enq.MileStoneStatusId,
                                MileStoneStatusName = enq.MileStoneStatusName,
                                CreationTime = enq.CreationTime,
                                QuotationKanban = (from r in quotation where r.EnquiryId == enq.Id && r.MileStoneId == mil.Id select r).ToArray()
                            });
                        }
                    }
                    else
                    {
                        try
                        {
                            enquiryquotation.Add(new EnquiryQuotationKanbanListOld
                            {
                                Id = enq.Id,
                                EnquiryNo = enq.EnquiryNo,
                                Title = enq.Title,
                                CompanyId = enq.CompanyId,
                                CompanyName = enq.CompanyName,
                                ContactId = enq.ContactId,
                                ContactName = enq.ContactName,
                                MileStoneId = enq.MileStoneId,
                                MileStoneName = enq.MileStoneName,
                                MileStoneStatusId = enq.MileStoneStatusId,
                                MileStoneStatusName = enq.MileStoneStatusName,
                                CreationTime = enq.CreationTime,
                                QuotationKanban = (from r in quotation where r.EnquiryId == enq.Id && r.MileStoneId == enq.MileStoneId select r).ToArray()
                            });
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    } 
                    
                }
                foreach (var mile in milestone)
                {
                    var flag = true;
                    if (mile.Name == "Lead" || mile.Name == "Qualified")
                    {
                        flag = false;
                    }
                    enquiryquotationkanban.Add(new EnquiryQuotationKanbanArrayOld { MilestoneName = mile.Name,Flag = flag, EnquiryQuotationKanban = (from r in enquiryquotation where r.MileStoneId == mile.Id select r).OrderByDescending(p => p.CreationTime).ToArray() });
                }
                return enquiryquotationkanban.ToArray();
            }
        }
        public async Task<FileDto> GetEnquiryToExcel()
        {

            var query = _EnquiryRepository.GetAll();
            var enquiry = (from a in query
                           select new EnquiryList
                           {
                               Id = a.Id,
                               EnquiryNo = a.EnquiryNo,
                               Title = a.Title,
                               CompanyId = a.CompanyId,
                               CompanyName = a.Companys.Name,
                               ContactId = a.ContactId != null ? a.ContactId : 0,
                               ContactName = a.ContactId != null ? a.Contacts.Name + " " + a.Contacts.LastName : "",
                               MileStoneId = a.MileStoneId,
                               MileStoneName = a.MileStones.Name,
                               MileStoneStatusId = a.MileStoneStatusId != null ? a.MileStoneStatusId : 0,
                               MileStoneStatusName = a.MileStoneStatusId != null ? a.MileStoneStatuss.Name : "",
                               Remarks = a.Remarks,
                               CreationTime = a.CreationTime,
                               CloseDate = a.CloseDate
                           });

            var EnquiryLists = enquiry.MapTo<List<EnquiryList>>();

            return _enquiryExcelExporter.ExportToFile(EnquiryLists);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Leads_Leads_Gridview)]
        public async Task<PagedResultDto<EnquiryList>> GetEnquiryGrid(EnquiryListInput input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                var query = _EnquiryRepository.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p =>
                     p.EnquiryNo.Contains(input.Filter) ||
                     p.Title.Contains(input.Filter) ||
                     p.Companys.Name.Contains(input.Filter) ||
                     p.Contacts.Name.Contains(input.Filter) ||
                     p.MileStones.Name.Contains(input.Filter)
                );
                var enquiry = (from a in query
                               select new EnquiryList
                               {
                                   Id = a.Id,
                                   EnquiryNo = a.EnquiryNo,
                                   Title = a.Title,
                                   CompanyId = a.CompanyId,
                                   CompanyName = a.Companys.Name,
                                   ContactId = a.ContactId != null ? a.ContactId : 0,
                                   ContactName = a.ContactId != null ? a.Contacts.Name + " " + a.Contacts.LastName : "",
                                   MileStoneId = a.MileStoneId,
                                   MileStoneName = a.MileStones.Name,
                                   MileStoneStatusId = a.MileStoneStatusId != null ? a.MileStoneStatusId : 0,
                                   MileStoneStatusName = a.MileStoneStatusId != null ? a.MileStoneStatuss.Name : "",
                                   Remarks = a.Remarks,
                                   CreationTime = a.CreationTime,
                                   EstimationValue =a.EstimationValue
                               });

                var datacount = enquiry.Count();
                var data = await enquiry.OrderBy(input.Sorting).PageBy(input).ToListAsync();
                var enquirylistresult = ObjectMapper.Map<List<EnquiryList>>(data);
                return new PagedResultDto<EnquiryList>(datacount, enquirylistresult);
            }
        }
        public ListResultDto<EnquiryList> GetNotificationEnquiry()
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                var today = DateTime.Now;
                var query = _EnquiryRepository.GetAll().Where(p => p.CloseDate < today);
                var Enquiry = (from r in query
                               select new EnquiryList
                               {
                                   Id = r.Id,
                                   EnquiryNo = r.EnquiryNo,
                                   Title = r.Title,
                                   MileStoneId = r.MileStoneId,
                                   MileStoneName = r.MileStoneId > 0 ? r.MileStones.Name : "",
                                   MileStoneStatusId = r.MileStoneStatusId,
                                   MileStoneStatusName = r.MileStoneStatusId > 0 ? r.MileStoneStatuss.Name : "",
                                   CompanyId = r.CompanyId,
                                   CompanyName = r.CompanyId > 0 ? r.Companys.Name : "",
                                   ContactId = r.ContactId,
                                   ContactName = r.ContactId > 0 ? r.Contacts.Name + " " + r.Contacts.LastName : "",
                                   Remarks = r.Remarks,
                                   CreationTime = r.CreationTime,
                                   CloseDate = r.CloseDate
                               }).ToList();

                return new ListResultDto<EnquiryList>(Enquiry.MapTo<List<EnquiryList>>());
            }
        }
        public async Task<Array> GetEnquiryKanban(EnquiryKanbanInput input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                var milestone = (from r in _MilestoneRepository.GetAll() select r).ToArray();
                var query = _EnquiryRepository.GetAll()
                    .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p =>
                         p.EnquiryNo.Contains(input.Filter) ||
                         p.Title.Contains(input.Filter) ||
                         p.Companys.Name.Contains(input.Filter) ||
                         p.Contacts.Name.Contains(input.Filter) ||
                         p.MileStones.Name.Contains(input.Filter)
                    );
                var enquiry = (from a in query
                               select new EnquiryList
                               {
                                   Id = a.Id,
                                   EnquiryNo = a.EnquiryNo,
                                   Title = a.Title,
                                   CompanyId = a.CompanyId,
                                   CompanyName = a.Companys.Name,
                                   ContactId = a.ContactId > 0 ? a.ContactId : 0,
                                   ContactName = a.ContactId > 0 ? a.Contacts.Name + " " + a.Contacts.LastName : "",
                                   MileStoneId = a.MileStoneId,
                                   MileStoneName = a.MileStones.Name,
                                   MileStoneStatusId = a.MileStoneStatusId > 0 ? a.MileStoneStatusId : 0,
                                   MileStoneStatusName = a.MileStoneStatusId > 0 ? a.MileStoneStatuss.Name : "",
                                   Remarks = a.Remarks,
                                   CreationTime = a.CreationTime,
                                   CloseDate = a.CloseDate
                               }).ToList();
                var enquirylist = ObjectMapper.Map<List<EnquiryList>>(enquiry);
                var enquirykanban = new List<EnquiryKanbanArray>();
                foreach (var mile in milestone)
                {
                    enquirykanban.Add(new EnquiryKanbanArray { MilestoneName = mile.Name, EnquiryKanban = (from r in enquirylist where r.MileStoneId == mile.Id select r).OrderByDescending(p => p.CreationTime).ToArray() });
                }
                return enquirykanban.ToArray();
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Leads_Leads_Create)]
        public async Task CreateOrUpdateEnquiry(EnquiryInput input)
        {
            if (input.Id != 0)
            {
                await UpdateEnquiryAsync(input);
            }
            else
            {
                await CreateEnquiryAsync(input);
            }
        }
        public virtual async Task CreateEnquiryAsync(EnquiryInput input)
        {
            try
            {
                input.TenantId = (int)(AbpSession.TenantId);
                var enquiry = _EnquiryRepository.GetAll().ToList();
                if (enquiry != null)
                {
                    if (enquiry.Count > 0)
                    {
                        var max = (enquiry.Select(x => (int?)x.Id).Max() ?? 0) + 1;
                        input.EnquiryNo = "ENQ" + max;
                    }
                    else
                    {
                        input.EnquiryNo = "ENQ" + 1;
                    }
                }

                var query = input.MapTo<Enquiry>();
                await _EnquiryRepository.InsertAsync(query);
            }
            catch(Exception ex)
            {

            }
          
        }
        public virtual async Task UpdateEnquiryAsync(EnquiryInput input)
        {
            input.TenantId = (int)(AbpSession.TenantId);
            var enquiry = await _EnquiryRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, enquiry);
            await _EnquiryRepository.UpdateAsync(enquiry);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Leads_Leads_Edit)] 
        public async Task<GetEnquiry> GetEnquiryForEdit(NullableIdDto input)
        {
            var output = new GetEnquiry();
            var query = _EnquiryRepository.GetAll().Where(p => p.Id == input.Id);
            var enquiry = (from a in query
                           select new EnquiryList
                           {
                               Id = a.Id,
                               EnquiryNo = a.EnquiryNo,
                               Title = a.Title,
                               CompanyId = a.CompanyId,
                               CompanyName = a.Companys.Name,
                               ContactId = a.ContactId != null ? a.ContactId : 0,
                               ContactName = a.ContactId != null ? a.Contacts.Name + " " + a.Contacts.LastName : "",
                               MileStoneId = a.MileStoneId,
                               MileStoneName = a.MileStones.Name,
                               MileStoneStatusId = a.MileStoneStatusId != null ? a.MileStoneStatusId : 0,
                               MileStoneStatusName = a.MileStoneStatusId != null ? a.MileStoneStatuss.Name : "",
                               Remarks = a.Remarks,
                               CloseDate = a.CloseDate,
                               EstimationValue = a.EstimationValue
                           }).FirstOrDefault();
            output = new GetEnquiry
            {
                EnquiryDetail = enquiry,
            };
            return output;
        }
        public async Task EnquiryKanbanUpdateAsync(EnquiryKanbanUpdateInput input)
        {
            var enquiry = _EnquiryRepository.GetAll().Where(p => p.Id == input.EnquiryId).FirstOrDefault();
            var milestone = _MilestoneRepository.GetAll().Where(p => p.Name == input.UpdateMilestone).FirstOrDefault();
            var query = enquiry.MapTo<Enquiry>();
            query.MileStoneId = milestone.Id;
            ObjectMapper.Map(query, enquiry);
            await _EnquiryRepository.UpdateAsync(enquiry);
        }
        public async Task EnquiryQuotationKanbanUpdateAsync(EnquiryQuotationKanbanUpdateInput input)
        {
            var quotation = _QuotationRepository.GetAll().Where(p => p.Id == input.QuotationId).FirstOrDefault();
            var milestone = _MilestoneRepository.GetAll().Where(p => p.Name == input.UpdateMilestone).FirstOrDefault();
            var query = quotation.MapTo<Quotation>();
            query.MileStoneId = milestone.Id;
            ObjectMapper.Map(query, quotation);
            await _QuotationRepository.UpdateAsync(quotation);
        }
        public async Task<PagedResultDto<QuotationList>> GetEnquiryQuotation(GetEnquiryQuotationInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                var query = _QuotationRepository.GetAll().Where(p => p.EnquiryId == input.EnquiryId);

                var quotation = (from a in query
                                 select new QuotationList
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
                                     SalesOrderNumber = a.SalesOrderNumber,
                                     LostDate = a.LostDate,
                                     OverallDiscount = a.OverallDiscount,
                                     CustomerPONumber = a.CustomerPONumber,
                                     ExchangeRate = a.ExchangeRate,
                                     EnquiryId = a.EnquiryId,
                                     EnquiryTitle = a.EnquiryId != null ? a.Enquirys.Title : "",
                                     QuotationTitleId = a.QuotationTitleId,
                                     QuotationTitleName = a.QuotationTitleId != null ? a.QuotationTitle.Name : "",
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
                                     ReasonName = a.ReasonId != null ? a.Reasons.Name : "",
                                     ContactId = a.ContactId,
                                     ContactName = a.ContactId != null ? a.Contacts.Name + a.Contacts.LastName : "",
                                     MileStoneId = a.MileStoneId

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
        public async Task<Array> GetEnquiryQuotationsKanban(EnquiryKanbanInput input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
               
                var query = _EnquiryRepository.GetAll();

                query = query.WhereIf(
                  !input.Filter.IsNullOrEmpty(),
                  p =>
                       p.Companys.Name.Contains(input.Filter) ||
                       p.Title.Contains(input.Filter) ||
                       p.MileStones.Name.Contains(input.Filter) ||
                       p.EnquiryNo.Contains(input.Filter)
                  );

                var SupportMileStones = (from r in _MilestoneRepository.GetAll() where r.IsQuotation == false select r).ToArray();
                var QSupportMileStones = (from r in _MilestoneRepository.GetAll() where r.IsQuotation == true select r).ToArray();

                var NormalTicket = (from a in query
                                    select new EnquiryQuotationKanbanList
                                    {
                                        Id = a.Id,
                                        EnquiryNo = a.EnquiryNo,
                                        Title = a.Title,
                                        CompanyId = a.CompanyId,
                                        CompanyName = a.Companys.Name,
                                        ContactId = a.ContactId != null ? a.ContactId : 0,
                                        ContactName = a.ContactId != null ? a.Contacts.Name + " " + a.Contacts.LastName : "",
                                        MileStoneId = a.MileStoneId,
                                        MileStoneName = a.MileStones.Name,
                                        IsQuotation = a.MileStones.IsQuotation,
                                        MileStoneStatusId = a.MileStoneStatusId != null ? a.MileStoneStatusId : 0,
                                        MileStoneStatusName = a.MileStoneStatusId != null ? a.MileStoneStatuss.Name : "",
                                        CreationTime = a.CreationTime,
                                        QuotationId = 0,
                                        SubjectName = ""
                                       
                                    }).ToList();

                var NewStatuss = (from r in NormalTicket where r.IsQuotation == false select r).OrderByDescending(p => p.CreationTime).ToList();
                var NewStatusdtos = NewStatuss.MapTo<List<EnquiryQuotationKanbanList>>();

                var SubListout = new List<EnquiryQuotationKanbanArray>();


                foreach (var newsts in SupportMileStones)
                {
                   var flag = true;
                   if (newsts.Name == "Lead" || newsts.Name == "Qualified")
                   {
                       flag = false;
                   }

                   SubListout.Add(new EnquiryQuotationKanbanArray { MilestoneName = newsts.Name, Flag = flag, EnquiryQuotationKanban = (from r in NewStatuss where r.MileStoneId == newsts.Id select r).OrderByDescending(p => p.CreationTime).ToArray() });
                }

                var QNewStatuss = (from r in NormalTicket where r.IsQuotation == true select r).OrderByDescending(p => p.CreationTime).ToList();
                var NewQuotationStatus = new List<EnquiryQuotationKanbanList>();
                foreach (var tickt in QNewStatuss)
                {
                    var Quotation = _QuotationRepository.GetAll().Where(a => a.EnquiryId == tickt.Id).ToList();
                    
                    foreach (var a in Quotation)
                    {
                        //try
                        //{
                            NewQuotationStatus.Add(new EnquiryQuotationKanbanList
                            {
                                Id = a.EnquiryId ?? 0,
                                EnquiryNo = "",
                                Title = "",
                                CompanyId = (int)a.CompanyId,
                                CompanyName = a.Companys != null? a.Companys.Name: "",
                                ContactId = a.ContactId != null ? a.ContactId : 0,
                                ContactName = a.Contacts != null? a.Contacts.Name + " " +a.Contacts.LastName : "",
                                MileStoneId = (int)a.MileStoneId,
                                MileStoneName = a.MileStones.Name,
                                IsQuotation = a.MileStones.IsQuotation,
                                MileStoneStatusId = 0,
                                MileStoneStatusName = "",
                                CreationTime = a.CreationTime,
                                QuotationId = a.Id,
                                SubjectName = a.SubjectName

                            });
                        //}
                        //catch (Exception ex)
                        //{
                        //    throw ex;
                        //}
                        
                    }

                }

                foreach (var Qnewsts in QSupportMileStones)
                {
                    var flag = true;
                    if (Qnewsts.Name == "Lead" || Qnewsts.Name == "Qualified")
                    {
                        flag = false;
                    }
                    SubListout.Add(new EnquiryQuotationKanbanArray { MilestoneName = Qnewsts.Name, Flag = flag, EnquiryQuotationKanban = (from r in NewQuotationStatus where r.MileStoneId == Qnewsts.Id select r).OrderByDescending(p => p.CreationTime).ToArray() });
                }

                return SubListout.ToArray();
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Leads_Leads_Gridview_Delete)]
        public async Task GetDeleteEnquiry(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_DeleteAllDetail", conn);
                sqlComm.Parameters.AddWithValue("@Id", input.Id);
                sqlComm.Parameters.AddWithValue("@TableId", 3);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }
        public async Task<Array> GetInquiryKanban(EnquiryKanbanInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId((int)AbpSession.TenantId))
            {
                string Query = "SELECT * FROM [dbo].[View_Kanban] WHERE TenantId = " + AbpSession.TenantId;

                var SupportMileStones = (from r in _MilestoneRepository.GetAll() where r.IsQuotation == false select r).ToArray();
                var QSupportMileStones = (from r in _MilestoneRepository.GetAll() where r.IsQuotation == true select r).ToArray();

                var SubListout = new List<EnquiryQuotationKanbanArray>();

                ConnectionAppService db = new ConnectionAppService();
                DataTable dt = new DataTable();
                SqlConnection con3 = new SqlConnection(db.ConnectionString());
                con3.Open();
                SqlCommand cmd3 = new SqlCommand(Query, con3);
                DataTable dt3 = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd3))
                {
                    sda.Fill(dt);
                }

                var NormalTicket = (from DataRow dr in dt.Rows
                                    select new EnquiryQuotationKanbanList
                                    {
                                        Id = Convert.ToInt32(dr["Id"]),
                                        Title = Convert.ToString(dr["Title"]),
                                        EnquiryNo = Convert.ToString(dr["EnquiryNo"]),
                                        MileStoneId = Convert.ToInt32(dr["MileStoneId"]),
                                        MileStoneStatusName = Convert.ToString(dr["StatusName"]),
                                        CompanyName = Convert.ToString(dr["CompanyName"]),
                                        ContactName = Convert.ToString(dr["ContactName"]),
                                        QuotationId = Convert.ToInt32(dr["QuotationId"]),
                                        QRefno = Convert.ToString(dr["QRefno"]),
                                        CreationTime = Convert.ToDateTime(dr["CreationTime"]),
                                        CloseDate = Convert.ToDateTime(dr["CloseDate"]),
                                        Creator = Convert.ToString(dr["Creator"]),
                                        CreatorImg = Convert.ToString(dr["CreatorImg"]),
                                        Salesperson = Convert.ToString(dr["Salesperson"]),
                                        SalespersonImg = Convert.ToString(dr["SalespersonImg"]),
                                        Remarks = Convert.ToString(dr["Remarks"]),
                                        Total = Convert.ToDecimal(dr["Total"]),
                                        EnqQuotation = Convert.ToString(dr["QuotationInfo"]),
                                    });

                var NewStatuss = NormalTicket;

                NewStatuss = NewStatuss.WhereIf(
                      !input.Filter.IsNullOrEmpty(),
                      p =>
                           p.Title.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.EnquiryNo.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.CompanyName.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) 
                      );

                var NewStatusdtos = NewStatuss.MapTo<List<EnquiryQuotationKanbanList>>();

                foreach (var newsts in SupportMileStones)
                {
                    SubListout.Add(new EnquiryQuotationKanbanArray
                    {
                        Flag = false,
                        MilestoneName = newsts.Name,
                        Total = (from r in NewStatuss where r.MileStoneId == newsts.Id && r.QuotationId > 0 select r.Total).Sum(),
                        EnquiryQuotationKanban = (from r in NewStatuss where r.MileStoneId == newsts.Id && r.QuotationId == 0 select r).OrderByDescending(p => p.CreationTime).ToArray()
                    });
                }

                foreach (var Qnewsts in QSupportMileStones)
                {
                    SubListout.Add(new EnquiryQuotationKanbanArray
                    {
                        Flag = true,
                        MilestoneName = Qnewsts.Name,
                        Total = (from r in NewStatuss where r.MileStoneId == Qnewsts.Id && r.QuotationId > 0 select r.Total).Sum(),
                        EnquiryQuotationKanban = (from r in NewStatuss where r.MileStoneId == Qnewsts.Id && r.QuotationId > 0 select r).OrderByDescending(p => p.CreationTime).ToArray()
                    });
                }
                return SubListout.ToArray();
            }
        }
        public async Task<PagedResultDto<EnquiryQuotationKanbanList>> GetGlobalReport(EnquiryListInput input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                string Query = "SELECT * FROM [dbo].[View_Kanban] WHERE TenantId = " + TenantId;

                ConnectionAppService db = new ConnectionAppService();
                DataTable dt = new DataTable();
                SqlConnection con3 = new SqlConnection(db.ConnectionString());
                con3.Open();
                SqlCommand cmd3 = new SqlCommand(Query, con3);
                DataTable dt3 = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd3))
                {
                    sda.Fill(dt);
                }

                var NormalTicket = (from DataRow dr in dt.Rows
                                    select new EnquiryQuotationKanbanList
                                    {
                                        Id = Convert.ToInt32(dr["Id"]),
                                        Title = Convert.ToString(dr["Title"]),
                                        EnquiryNo = Convert.ToString(dr["EnquiryNo"]),
                                        MileStoneId = Convert.ToInt32(dr["MileStoneId"]),
                                        MileStoneStatusName = Convert.ToString(dr["StatusName"]),
                                        CompanyName = Convert.ToString(dr["CompanyName"]),
                                        ContactName = Convert.ToString(dr["ContactName"]),
                                        QuotationId = Convert.ToInt32(dr["QuotationId"]),
                                        QRefno = Convert.ToString(dr["QRefno"]),
                                        CreationTime = Convert.ToDateTime(dr["CreationTime"]),
                                        CloseDate = Convert.ToDateTime(dr["CloseDate"]),
                                        Creator = Convert.ToString(dr["Creator"]),
                                        CreatorImg = Convert.ToString(dr["CreatorImg"]),
                                        Salesperson = Convert.ToString(dr["Salesperson"]),
                                        SalespersonImg = Convert.ToString(dr["SalespersonImg"]),
                                        Remarks = Convert.ToString(dr["Remarks"]),
                                        Total = Convert.ToDecimal(dr["Total"]),
                                    });

                NormalTicket = NormalTicket.WhereIf(
                      !input.Filter.IsNullOrEmpty(),
                      p =>
                           p.Title.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.EnquiryNo.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.CompanyName.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.MileStoneStatusName.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                           p.QRefno.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty))
                      );

                var NewStatussCount = NormalTicket.Count();

                var NewStatussList = NormalTicket.OrderByDescending(p => p.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

                var NewStatusdtos = NewStatussList.MapTo<List<EnquiryQuotationKanbanList>>();

                return new PagedResultDto<EnquiryQuotationKanbanList>(NewStatussCount, NewStatusdtos);
            }
        }
        public async Task<FileDto> GetGlobalReportToExcel()
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                string Query = "SELECT * FROM [dbo].[View_Kanban] WHERE TenantId = " + TenantId;

                ConnectionAppService db = new ConnectionAppService();
                DataTable dt = new DataTable();
                SqlConnection con3 = new SqlConnection(db.ConnectionString());
                con3.Open();
                SqlCommand cmd3 = new SqlCommand(Query, con3);
                DataTable dt3 = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd3))
                {
                    sda.Fill(dt);
                }

                var NormalTicket = (from DataRow dr in dt.Rows
                                    select new EnquiryQuotationKanbanList
                                    {
                                        Id = Convert.ToInt32(dr["Id"]),
                                        Title = Convert.ToString(dr["Title"]),
                                        EnquiryNo = Convert.ToString(dr["EnquiryNo"]),
                                        MileStoneId = Convert.ToInt32(dr["MileStoneId"]),
                                        MileStoneStatusName = Convert.ToString(dr["StatusName"]),
                                        CompanyName = Convert.ToString(dr["CompanyName"]),
                                        ContactName = Convert.ToString(dr["ContactName"]),
                                        QuotationId = Convert.ToInt32(dr["QuotationId"]),
                                        QRefno = Convert.ToString(dr["QRefno"]),
                                        CreationTime = Convert.ToDateTime(dr["CreationTime"]),
                                        CloseDate = Convert.ToDateTime(dr["CloseDate"]),
                                        Creator = Convert.ToString(dr["Creator"]),
                                        CreatorImg = Convert.ToString(dr["CreatorImg"]),
                                        Salesperson = Convert.ToString(dr["Salesperson"]),
                                        SalespersonImg = Convert.ToString(dr["SalespersonImg"]),
                                        Remarks = Convert.ToString(dr["Remarks"]),
                                        Total = Convert.ToDecimal(dr["Total"]),
                                    });

                var NewStatussCount = NormalTicket.Count();

                var NewStatussList = NormalTicket.OrderByDescending(p => p.CreationTime).ToList();

                var NewStatusdtos = NewStatussList.MapTo<List<EnquiryQuotationKanbanList>>();

                return _GlobalReportExcelExporter.ExportToFile(NewStatusdtos);
            }

        } 

    }
}
