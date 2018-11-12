using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.Extensions;
using System.Text;
using Abp.UI;
using Abp.AutoMapper;
using System.Threading.Tasks;
using Abp.Authorization;
using tibs.stem;
using tibs.stem.MileStones.Dto;
using tibs.stem.MileStoneStatusDetails;
using tibs.stem.MileStoneStatuss;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using tibs.stem.Milestones.Dto;
using tibs.stem.Dto;
using tibs.stem.Milestones.Exporting;
using tibs.stem.Authorization;

namespace tibs.stem.MileStones
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Master_MileStone)]
    public class MilestoneAppService : stemAppServiceBase, IMilestoneAppService
    {
        public readonly IRepository<MileStone> _MilestoneRepository;
        public readonly IRepository<MileStoneStatusDetail> _MilestoneStatusListRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IMilestoneExcelExporter _milestoneExcelExporter;

        public MilestoneAppService(
            IMilestoneExcelExporter milestoneExcelExporter,
            IRepository<MileStone> MilestoneRepository,
            IRepository<MileStoneStatusDetail> MilestoneStatusListRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IAbpSession session
           )
        {
            _milestoneExcelExporter = milestoneExcelExporter;
            _MilestoneRepository = MilestoneRepository;
            _MilestoneStatusListRepository = MilestoneStatusListRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
        }
        public async Task<FileDto> GetMilestoneToExcel()
        {

            var query = _MilestoneRepository.GetAll();
            var data = (from r in query
                        select new MileStoneListDto
                        {

                            Id = r.Id,
                            Code = r.Code,
                            Name = r.Name


                        });


            var MileStoneListDtos = data.MapTo<List<MileStoneListDto>>();

            return _milestoneExcelExporter.ExportToFile(MileStoneListDtos);
        }

        public ListResultDto<MileStoneListDto> GetMilestones(GetMileStoneInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var supp = _MilestoneRepository
                .GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.Name.Contains(input.Filter) ||
                         p.Code.Contains(input.Filter)
                )
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Code)
                .ToList();

                return new ListResultDto<MileStoneListDto>(supp.MapTo<List<MileStoneListDto>>());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_MileStone_Edit)]
        public async Task<GetMileStone> GetMilestoneForEdit(EntityDto input)
        {
            var output = new GetMileStone
            {
            };

            var supp = _MilestoneRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            var mileStone = supp.MapTo<CreateMileInputDto>();

            if (supp != null)
            {
                var mileStoneStatus = _MilestoneStatusListRepository
                    .GetAll().Where(p => p.MileStoneId == input.Id);

                var query = from s in mileStoneStatus
                            select new MileStoneDetailListDto
                            {
                                StatusName = s.EnquiryStatuss.Name,
                                MilestoneStatusId = s.EnquiryStatuss.Id,
                                Id = s.Id,
                                MilestoneId = s.MileStoneId
                            };
                var data = query.ToArray();
                output = new GetMileStone
                {
                    mileStoneStatus = data,
                    mileStones = mileStone

                };

            }
            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_MileStone_Create)]
        public async Task CreateOrUpdateMilestone(CreateMileInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateMilestone(input);
            }
            else
            {
                await CreateMilestone(input);
            }

        }

        public virtual async Task CreateMilestone(CreateMileInputDto input)
        {
                input.TenantId = (int)_session.TenantId;
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {

                var milestone = input.MapTo<MileStone>();

                var val = _MilestoneRepository
                    .GetAll().Where(p => p.Name == input.Name || p.Code == input.Code).FirstOrDefault();

                if (input.EndOfQuotation == true && input.IsQuotation == true)
                {
                    val = _MilestoneRepository.GetAll().Where(p => (p.Name == input.Name || p.Code == input.Code || p.EndOfQuotation == true) && p.IsQuotation == true).FirstOrDefault();
                }
                else if (input.EndOfQuotation == true && input.IsQuotation == false)
                {
                    throw new UserFriendlyException("Ooops!", "Invalid Milestone");
                }

                if (val == null)
                {
                    await _MilestoneRepository.InsertAsync(milestone);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Support Milestone '" + input.Name + "'...");
                }
            }
        }
        public virtual async Task UpdateMilestone(CreateMileInputDto input)
        {
            input.TenantId = (int)_session.TenantId;
            using (_unitOfWorkManager.Current.SetTenantId(_session.TenantId))
            {
                var mileStone = input.MapTo<MileStone>();
                var val = _MilestoneRepository
                  .GetAll().Where(p => (p.Name == input.Name || p.Code == input.Code) && p.Id != input.Id).FirstOrDefault();

                if (input.EndOfQuotation == true && input.IsQuotation == true)
                {
                    val = _MilestoneRepository.GetAll().Where(p => (p.Name == input.Name || p.Code == input.Code || p.EndOfQuotation == true) && p.IsQuotation == true && p.Id != input.Id).FirstOrDefault();
                }
                else if (input.EndOfQuotation == true && input.IsQuotation == false)
                {
                    throw new UserFriendlyException("Ooops!", "Invalid Update");
                }

                if (val == null)
                {
                    await _MilestoneRepository.UpdateAsync(mileStone);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Support Milestone '" + input.Name + "'...");
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_MileStone_Delete)]
        public async Task GetDeleteMilestone(EntityDto input)
        {
            await _MilestoneRepository.DeleteAsync(input.Id);
        }


        // Milestone Details

        //public ListResultDto<MileStoneDetailListDto> GetMileStoneDetailList(EntityDto input)
        //{
        //    try
        //    {
        //        var mileStoneStatus = _MilestoneStatusListRepository
        //        .GetAll().Where(p => p.MileStoneId == input.Id);

        //        var query = from s in mileStoneStatus
        //                    select new MileStoneDetailListDto
        //                    {
        //                        StatusName = s.EnquiryStatuss.Name,
        //                        MilestoneStatusId = s.EnquiryStatuss.Id,
        //                        Id = s.Id,
        //                        MilestoneId = s.MileStoneId
        //                    };
        //        var Senioritylist = query.ToList();
        //        return new ListResultDto<MileStoneDetailListDto>(Senioritylist.MapTo<List<MileStoneDetailListDto>>());
        //    }
        //    catch (Exception obj)
        //    {
        //        string ff = obj.Message.ToString();
        //        return null;
        //    }
        //}

        public async Task<GetMilestoneDetail> GetMileStoneDetailForEdit(EntityDto input)
        {
            var output = new GetMilestoneDetail();
            try
            {
                if (input.Id > 0)
                {
                    var supp = _MilestoneStatusListRepository
                                   .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

                    output.MileStoneStatus = supp.MapTo<CreateMileStoneDetailInput>();

                }
            }
            catch (Exception obj)
            {
                string ff = obj.Message.ToString();
            }
            return output;

        }

        public async Task CreateOrUpdateMileStoneDetail(CreateMileStoneDetailInput input)

        {
            if (input.Id == 0)
            {
                await CreateStatusDetail(input);
            }
            else
            {
                await UpdateStatusDetail(input);
            }
        }

        public virtual async Task CreateStatusDetail(CreateMileStoneDetailInput input)
        {
            var mileStatus = input.MapTo<MileStoneStatusDetail>();

            var query = _MilestoneStatusListRepository.GetAll().Where(p => p.MileStoneId == input.MileStoneId && p.MileStoneStatusId == input.MileStoneStatusId).FirstOrDefault();
            if (query == null)
            {
                await _MilestoneStatusListRepository.InsertAsync(mileStatus);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Support Milestone Status...");

            }

        }

        public virtual async Task UpdateStatusDetail(CreateMileStoneDetailInput input)
        {

            var mileStatus = input.MapTo<MileStoneStatusDetail>();

            var query = _MilestoneStatusListRepository.GetAll().Where(p => p.MileStoneId == input.MileStoneId && p.MileStoneStatusId == input.MileStoneStatusId).FirstOrDefault();
            if (query == null)
            {
                await _MilestoneStatusListRepository.UpdateAsync(mileStatus);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Support Milestone Status...");

            }

        }

        public async Task GetDeleteMileStoneDetail(EntityDto input)
        {
            await _MilestoneStatusListRepository.DeleteAsync(input.Id);
        }

        
    }
}

