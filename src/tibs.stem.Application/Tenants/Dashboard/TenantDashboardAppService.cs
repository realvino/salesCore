using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Authorization;
using tibs.stem.Authorization.Roles;
using tibs.stem.QuotationProducts;
using tibs.stem.Quotations;
using tibs.stem.Targetss;
using tibs.stem.Tenants.Dashboard.Dto;
using tibs.stem.TenantTargetss;
using Abp.UI;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using tibs.stem.Url;
using tibs.stem.VatAmountSettings;

namespace tibs.stem.Tenants.Dashboard
{
    [DisableAuditing]
    [AbpAuthorize(AppPermissions.Pages_Tenant_Dashboard_Dashboard)]
    public class TenantDashboardAppService : stemAppServiceBase, ITenantDashboardAppService
    {

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public readonly IRepository<TenantTargets> _TenantTargetRepository;
        public readonly IRepository<Targets> _TargetRepository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<Role> _RoleRepository;
        //scDbContext _scDbContext; scDbContext scDbContext,
        public readonly IRepository<Quotation> _QuotationRepository;
        public readonly IRepository<QuotationProduct> _QuotationProductRepository;
        private readonly IWebUrlService _webUrlService;
        public readonly IRepository<TenantVatAmountSetting> _TenantVatAmountSettingRepository;

        public TenantDashboardAppService(IRepository<TenantTargets> TenantTargetRepository, IRepository<TenantVatAmountSetting> TenantVatAmountSettingRepository, IUnitOfWorkManager unitOfWorkManager,
            IRepository<Targets> TargetRepository, IWebUrlService webUrlService, RoleManager roleManager, IRepository<UserRole, long> userRoleRepository, IRepository<Role> RoleRepository,
            IRepository<Quotation> QuotationRepository, IRepository<QuotationProduct> QuotationProductRepository)
        {
            _TenantTargetRepository = TenantTargetRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _TargetRepository = TargetRepository;
            _roleManager = roleManager;
            _userRoleRepository = userRoleRepository;
            _RoleRepository = RoleRepository;
            _TenantVatAmountSettingRepository = TenantVatAmountSettingRepository;
            _webUrlService = webUrlService;
            _QuotationRepository = QuotationRepository;
            _QuotationProductRepository = QuotationProductRepository;

        }

        public GetMemberActivityOutput GetMemberActivity()
        {
            return new GetMemberActivityOutput
            (
                DashboardRandomDataGenerator.GenerateMemberActivities()
            );
        }

        public GetDashboardDataOutput GetDashboardData(GetDashboardDataInput input)
        {
            var output = new GetDashboardDataOutput
            {
                TotalProfit = DashboardRandomDataGenerator.GetRandomInt(5000, 9000),
                NewFeedbacks = DashboardRandomDataGenerator.GetRandomInt(1000, 5000),
                NewOrders = DashboardRandomDataGenerator.GetRandomInt(100, 900),
                NewUsers = DashboardRandomDataGenerator.GetRandomInt(50, 500),
                SalesSummary = DashboardRandomDataGenerator.GenerateSalesSummaryData(input.SalesSummaryDatePeriod),
                Expenses = DashboardRandomDataGenerator.GetRandomInt(5000, 10000),
                Growth = DashboardRandomDataGenerator.GetRandomInt(5000, 10000),
                Revenue = DashboardRandomDataGenerator.GetRandomInt(1000, 9000),
                TotalSales = DashboardRandomDataGenerator.GetRandomInt(10000, 90000),
                TransactionPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                NewVisitPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                BouncePercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                NetworkLoad = DashboardRandomDataGenerator.GetRandomArray(20, 8, 15),
                CpuLoad = DashboardRandomDataGenerator.GetRandomArray(20, 8, 15),
                LoadRate = DashboardRandomDataGenerator.GetRandomArray(20, 8, 15),
                TimeLineItems = DashboardRandomDataGenerator.GenerateTimeLineItems()
            };

            return output;
        }

        public GetSalesSummaryOutput GetSalesSummary(GetSalesSummaryInput input)
        {
            return new GetSalesSummaryOutput(DashboardRandomDataGenerator.GenerateSalesSummaryData(input.SalesSummaryDatePeriod));
        }

        public GetWorldMapOutput GetWorldMap(GetWorldMapInput input)
        {
            return new GetWorldMapOutput(DashboardRandomDataGenerator.GenerateWorldMapCountries());
        }

        public GetServerStatsOutput GetServerStats(GetServerStatsInput input)
        {
            return new GetServerStatsOutput
            {
                NetworkLoad = DashboardRandomDataGenerator.GetRandomArray(20, 8, 15),
                CpuLoad = DashboardRandomDataGenerator.GetRandomArray(20, 8, 15),
                LoadRate = DashboardRandomDataGenerator.GetRandomArray(20, 8, 15)
            };
        }

        public GetGeneralStatsOutput GetGeneralStats(GetGeneralStatsInput input)
        {
            return new GetGeneralStatsOutput
            {
                TransactionPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                NewVisitPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                BouncePercent = DashboardRandomDataGenerator.GetRandomInt(10, 100)
            };
        }

        public ListResultDto<TenantTargetListDto> GetTenantTarget(GetTenantTargetInput input)
        {
            long userid = (long)AbpSession.UserId;
            int TenantId = (int)(AbpSession.TenantId);

            List<string> UserRoleList = new List<string>();
            ConnectionAppService db = new ConnectionAppService();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("sp_GetUserRoles", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@TenantId", TenantId);
                sqlComm.Parameters.AddWithValue("@UserId", userid);
                conn.Open();
                using (var reader = sqlComm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserRoleList.Add((string)reader["DisplayName"]);
                    }
                }
                conn.Close();
            }

            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                var TenantTarget = _TenantTargetRepository.GetAll().Where(p => p.Id == 0);
                if (UserRoleList.Contains("Admin"))
                {
                    TenantTarget = _TenantTargetRepository.GetAll()
                        .WhereIf(!input.Filter.IsNullOrEmpty(),
                                    p => p.Value.ToString().Contains(input.Filter) ||
                                    p.TenantId.ToString().Contains(input.Filter));
                }
                else if (UserRoleList.Contains("Sales Manager"))
                {
                    TenantTarget = _TenantTargetRepository.GetAll().Where(p => p.CreatorUserId == userid)
                        .WhereIf(!input.Filter.IsNullOrEmpty(),
                                    p => p.Value.ToString().Contains(input.Filter) ||
                                    p.TenantId.ToString().Contains(input.Filter));
                }

                var query = from s in TenantTarget
                            select new TenantTargetListDto
                            {
                                TenantId = s.TenantId,
                                Value = s.Value,
                                TargetDate = s.TargetDate,
                                Id = s.Id
                            };
                var list = query.ToList();
                return new ListResultDto<TenantTargetListDto>(list.MapTo<List<TenantTargetListDto>>());
            }
        }

        public async Task<PagedResultDto<TenantTargetListDto>> GetTenantTargetPagedResult(GetTenantTargetInput1 input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                var TenantTarget = _TenantTargetRepository.GetAll()
             .WhereIf(
             !input.Filter.IsNullOrEmpty(),
             p => p.Value.ToString().Contains(input.Filter) ||
                 p.TenantId.ToString().Contains(input.Filter)
             );

                var query = from s in TenantTarget
                            select new TenantTargetListDto
                            {
                                TenantId = s.TenantId,
                                Value = s.Value,
                                TargetDate = s.TargetDate,
                                Id = s.Id
                            };
                var TargetCount = await query.CountAsync();
                var Targetlist = await query
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();

                var Targetlistoutput = Targetlist.MapTo<List<TenantTargetListDto>>();

                return new PagedResultDto<TenantTargetListDto>(TargetCount, Targetlistoutput);
            }
        }
        public async Task<GetTenantTarget> GetTenantTargetForEdit(EntityDto input)
        {
            var output = new GetTenantTarget();
            if (input.Id > 0)
            {
                var query = _TenantTargetRepository
                               .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

                output.tenantTarget = query.MapTo<CreateTenantTargetInput>();

            }
            return output;

        }

        public async Task CreateOrUpdateTenantTarget(CreateTenantTargetInput input)
        {
            if (input.Id == 0)
            {
                await CreateTenantTarget(input);
            }
            else
            {
                await UpdateTenantTarget(input);
            }
        }


        public virtual async Task CreateTenantTarget(CreateTenantTargetInput input)
        {
            input.TenantId = (int)(AbpSession.TenantId);
            var Target = input.MapTo<TenantTargets>();
            int InputMonth = Convert.ToDateTime(input.TargetDate).Month;
            int InputYear = Convert.ToDateTime(input.TargetDate).Year;
            var query = _TenantTargetRepository.GetAll().Where(p => p.TenantId == input.TenantId && Convert.ToDateTime(p.TargetDate).Year == InputYear && Convert.ToDateTime(p.TargetDate).Month == InputMonth).FirstOrDefault();

            if (query == null)
            {
                await _TenantTargetRepository.InsertAsync(Target);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Tenant Target ...");
            }
        }

        public virtual async Task UpdateTenantTarget(CreateTenantTargetInput input)
        {
            var Target = input.MapTo<TenantTargets>();
            int InputMonth = Convert.ToDateTime(input.TargetDate).Month;
            int InputYear = Convert.ToDateTime(input.TargetDate).Year;
            var query = _TenantTargetRepository.GetAll().Where(p => p.TenantId == input.TenantId && Convert.ToDateTime(p.TargetDate).Year == InputYear && Convert.ToDateTime(p.TargetDate).Month == InputMonth && p.Id != input.Id).FirstOrDefault();
            if (query == null)
            {
                await _TenantTargetRepository.UpdateAsync(Target);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Tenant Target ...");
            }
        }

        public async Task GetDeleteTarget(EntityDto input)
        {
            await _TenantTargetRepository.DeleteAsync(input.Id);
        }
        public async Task<Array> GetActivepotential()   // public async Task<PagedResultDto<SpResultActivepotential>> GetActivepotential()
        {
            long userid = (long)AbpSession.UserId;
            int TenantId = (int)(AbpSession.TenantId);
            List<SpResultActivepotential> Activepotentiallist = new List<SpResultActivepotential>();
            ConnectionAppService db = new ConnectionAppService();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("sp_activepotential", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@TenantId", TenantId);
                sqlComm.Parameters.AddWithValue("@UserId", userid);
                conn.Open();
                using (var reader = sqlComm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SpResultActivepotential actpotential = new SpResultActivepotential();
                        actpotential.createmonth = Convert.ToString(reader["create_month"]);
                        actpotential.totalcreatedvalue = Convert.ToDecimal(reader["total_created_value"]);
                        actpotential.totalwonvalue = Convert.ToDecimal(reader["total_won_value"]);
                        actpotential.optimalactive = Convert.ToDecimal(reader["optimal_active"]);
                        Activepotentiallist.Add(actpotential);
                    }
                }
                conn.Close();
            }
            return (Activepotentiallist.ToArray());
            //var ActivepotentialCount = Activepotentiallist.Count;
            // return new PagedResultDto<SpResultActivepotential>(ActivepotentialCount, Activepotentiallist);

        }
        public async Task<Array> GetUserTarget(EntityDto input)  //public async Task<PagedResultDto<SpResultUserTarget>> GetUserTarget()
        {
            int TenantId = (int)(AbpSession.TenantId);

            List<SpResultUserTarget> UserTargetlist = new List<SpResultUserTarget>();
            ConnectionAppService db = new ConnectionAppService();             //var db = _scDbContext.Database.GetDbConnection().ConnectionString;
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("sp_GetUserTarget_Updated", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@TenantId", TenantId);
                sqlComm.Parameters.AddWithValue("@UserId", input.Id);
                conn.Open();
                using (var reader = sqlComm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SpResultUserTarget userTarget = new SpResultUserTarget();
                        userTarget.Yeartarget = Convert.ToDecimal(reader["TargetYear"]);
                        userTarget.YearAchived = Convert.ToDecimal(reader["AchivedYear"]);
                        userTarget.MonthTarget = Convert.ToDecimal(reader["Targetmonth"]);
                        userTarget.MonthAchived = Convert.ToDecimal(reader["AchivedMonth"]);
                        userTarget._Month = Convert.ToDecimal(reader["_Month"]);
                        userTarget._Year = Convert.ToDecimal(reader["_Year"]);

                        UserTargetlist.Add(userTarget);
                    }
                }
                conn.Close();
            }
            var datalist = (from r in UserTargetlist select r).ToArray();
            return datalist.ToArray();
            //var UserTargetCount = UserTargetlist.Count;
            //return new PagedResultDto<SpResultUserTarget>(UserTargetCount, UserTargetlist);

        }
        public async Task<List<SliderDataList>> GetSalesExecutive()
        {
            long userid = (long)AbpSession.UserId;
            var user = await UserManager.GetUserByIdAsync(userid);
            var grantedPermissions = await UserManager.GetGrantedPermissionsAsync(user);
            var permissionresult = (from r in grantedPermissions where r.Name == "Pages.Tenant.Managemant.Leads" select r).FirstOrDefault();

            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                string SliderQuery = "SELECT * FROM [dbo].[View_SalesPersonSlider] where TenantId = " + AbpSession.TenantId;

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
                var data = (from DataRow dr in viewtable.Rows
                            select new SliderDataList
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Name = Convert.ToString(dr["Name"]),
                                ProfilePicture = _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + Convert.ToString(dr["ProfileImage"]),
                                Email = Convert.ToString(dr["EmailAddress"])
                            }).ToList();

                //if(userrole.DisplayName != "Admin")
                //{
                //    data = (from r in data where r.Id == userid select r).ToList();
                //}

                if (permissionresult != null && userrole.DisplayName != "Admin")
                {
                    data = (from r in data where r.Id == userid select r).ToList();
                }
                else if (permissionresult == null && userrole.DisplayName != "Admin")
                {
                    var qchk = _QuotationRepository.GetAll().Where(p => p.SalesmanId == userid).FirstOrDefault();
                    if (qchk != null)
                    {
                        data = (from r in data where r.Id == userid select r).ToList();
                    }
                    else
                    {
                        data = (from r in data where r.Id < 1 select r).ToList();
                    }
                }

                if (data.Count > 1)
                {
                    data.Add(new SliderDataList { Id = 0, Name = "All", ProfilePicture = _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + "Common/Profile/default-profile-picture.png", Email = "test@gmail.com" });
                }
                var SliderData = data.OrderBy(p => p.Id).MapTo<List<SliderDataList>>();
                return SliderData;
            }

        }

        public async Task CreateOrUpdateTenantVatAmount(SettingVatAmountInput input)
        {
            if (input.Id != 0)
            {
                await UpdateTenantVatAmount(input);
            }
            else
            {
                await CreateTenantVatAmount(input);
            }
        }
        public async Task CreateTenantVatAmount(SettingVatAmountInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId((int)AbpSession.TenantId))
            {
                input.TenantId = (int)AbpSession.TenantId;
                var TenantVatAmount = input.MapTo<TenantVatAmountSetting>();
                await _TenantVatAmountSettingRepository.InsertAsync(TenantVatAmount);
            }
        }
        public async Task UpdateTenantVatAmount(SettingVatAmountInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId((int)AbpSession.TenantId))
            {
                var TenantVatAmount = await _TenantVatAmountSettingRepository.GetAsync(input.Id);
                ObjectMapper.Map(input, TenantVatAmount);
                TenantVatAmount.TenantId = (int)AbpSession.TenantId;
                await _TenantVatAmountSettingRepository.UpdateAsync(TenantVatAmount);
            }
        }
        public async Task<SettingVatAmountInput> GetTenantVatAmountSettingForEdit(NullableIdDto input)
        {
            using (_unitOfWorkManager.Current.SetTenantId((int)AbpSession.TenantId))
            {
                var output = new SettingVatAmountInput();
                var query = _TenantVatAmountSettingRepository.GetAll().Where(p => p.TenantId == input.Id).FirstOrDefault();
                output = query.MapTo<SettingVatAmountInput>();
                return output;
            }

        }
    }
        public class SpResultActivepotential
    {
        public string createmonth { get; set; }
        public decimal totalcreatedvalue { get; set; }
        public decimal totalwonvalue { get; set; }
        public decimal optimalactive { get; set; }
    }
    public class SpResultUserTarget
    {
        public decimal Yeartarget { get; set; }
        public decimal YearAchived { get; set; }
        public decimal MonthTarget { get; set; }
        public decimal MonthAchived { get; set; }
        public decimal _Month { get; set; }
        public decimal _Year { get; set; }

    }
}