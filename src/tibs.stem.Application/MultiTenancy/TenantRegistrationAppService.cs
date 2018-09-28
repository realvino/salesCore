using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using Abp.Zero.Configuration;
using tibs.stem.Configuration;
using tibs.stem.Debugging;
using tibs.stem.Editions;
using tibs.stem.Editions.Dto;
using tibs.stem.Features;
using tibs.stem.MultiTenancy.Dto;
using tibs.stem.MultiTenancy.Payments;
using tibs.stem.MultiTenancy.Payments.Cache;
using tibs.stem.Notifications;
using tibs.stem.Security.Recaptcha;
using tibs.stem.Url;
using Abp.Extensions;
using tibs.stem.MultiTenancy.Payments.Dto;
using Abp.Domain.Repositories;
using tibs.stem.MileStones;
using tibs.stem.TenantTypes;
using tibs.stem.MileStones.Dto;
using Abp.Domain.Uow;
using tibs.stem.Authorization.Users;
using Abp.AutoMapper;
using tibs.stem.Select2;
using tibs.stem.QuotationStatusss.Dto;
using tibs.stem.QuotationStatuss;

namespace tibs.stem.MultiTenancy
{
    public class TenantRegistrationAppService : stemAppServiceBase, ITenantRegistrationAppService
    {
        public IAppUrlService AppUrlService { get; set; }

        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IRecaptchaValidator _recaptchaValidator;
        private readonly EditionManager _editionManager;
        private readonly IAppNotifier _appNotifier;
        private readonly ILocalizationContext _localizationContext;
        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly ISubscriptionPaymentRepository _subscriptionPaymentRepository;
        private readonly IPaymentGatewayManagerFactory _paymentGatewayManagerFactory;
        private readonly IPaymentCache _paymentCache;
        public readonly IRepository<MileStone> _MilestoneRepository;
        private readonly IRepository<TenantType> _TenantTypeRepositary;
        public readonly IRepository<QuotationStatus> _QuotationStatusRepository;

        public TenantRegistrationAppService(
            IMultiTenancyConfig multiTenancyConfig,
            IRecaptchaValidator recaptchaValidator,
            EditionManager editionManager,
            IAppNotifier appNotifier,
            ILocalizationContext localizationContext,
            TenantManager tenantManager,
            UserManager userManager,
            ISubscriptionPaymentRepository subscriptionPaymentRepository,
            IPaymentGatewayManagerFactory paymentGatewayManagerFactory,
            IRepository<MileStone> MilestoneRepository,
            IRepository<TenantType> TenantTypeRepositary,
            IRepository<QuotationStatus> QuotationStatusRepository,
            IPaymentCache paymentCache)
        {
            _QuotationStatusRepository = QuotationStatusRepository;
            _multiTenancyConfig = multiTenancyConfig;
            _recaptchaValidator = recaptchaValidator;
            _editionManager = editionManager;
            _appNotifier = appNotifier;
            _localizationContext = localizationContext;
            _tenantManager = tenantManager;
            _subscriptionPaymentRepository = subscriptionPaymentRepository;
            _paymentGatewayManagerFactory = paymentGatewayManagerFactory;
            _paymentCache = paymentCache;
            _userManager = userManager;
            _TenantTypeRepositary = TenantTypeRepositary;
            _MilestoneRepository = MilestoneRepository;
            AppUrlService = NullAppUrlService.Instance;
        }

        public async Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input)
        {

            try
            {
                if (input.EditionId.HasValue)
                {
                    await CheckEditionSubscriptionAsync(input.EditionId.Value, input.SubscriptionStartType, input.Gateway, input.PaymentId);
                }
                else
                {
                    await CheckRegistrationWithoutEdition();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            try
            {

            

            using (CurrentUnitOfWork.SetTenantId(null))
            {
                CheckTenantRegistrationIsEnabled();

                if (UseCaptchaOnRegistration())
                {
                    await _recaptchaValidator.ValidateAsync(input.CaptchaResponse);
                }

                //Getting host-specific settings
                var isNewRegisteredTenantActiveByDefault = await SettingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault);
                var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueForApplicationAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

                DateTime? subscriptionEndDate = null;
                var isInTrialPeriod = false;

                if (input.EditionId.HasValue)
                {
                    isInTrialPeriod = input.SubscriptionStartType == SubscriptionStartType.Trial;

                    if (isInTrialPeriod)
                    {
                        var edition = (SubscribableEdition)await _editionManager.GetByIdAsync(input.EditionId.Value);
                        subscriptionEndDate = Clock.Now.AddDays(edition.TrialDayCount ?? 0);
                    }
                }

                var tenantId = await _tenantManager.CreateWithAdminUserAsync(
                    input.TenancyName,
                    input.Name,
                    input.AdminPassword,
                    input.AdminEmailAddress,
                    null,
                    isNewRegisteredTenantActiveByDefault,
                    input.EditionId,
                    false,
                    true,
                    subscriptionEndDate,
                    isInTrialPeriod,
                    AppUrlService.CreateEmailActivationUrlFormat(input.TenancyName),
                    input.TenantTypeId
                );

                Tenant tenant;

                if (input.SubscriptionStartType == SubscriptionStartType.Paid)
                {
                    if (!input.Gateway.HasValue)
                    {
                        throw new Exception("Gateway is missing!");
                    }

                    var payment = await _subscriptionPaymentRepository.GetByGatewayAndPaymentIdAsync(
                        input.Gateway.Value,
                        input.PaymentId
                    );

                    tenant = await _tenantManager.UpdateTenantAsync(
                        tenantId,
                        true,
                        false,
                        payment.PaymentPeriodType,
                        payment.EditionId,
                        EditionPaymentType.NewRegistration);

                    await _subscriptionPaymentRepository.UpdateByGatewayAndPaymentIdAsync(input.Gateway.Value,
                        input.PaymentId, tenantId, SubscriptionPaymentStatus.Completed);
                }
                else
                {
                    tenant = await TenantManager.GetByIdAsync(tenantId);
                }

                await _appNotifier.NewTenantRegisteredAsync(tenant);

                if (input.EditionId.HasValue && input.Gateway.HasValue && !input.PaymentId.IsNullOrEmpty())
                {
                    _paymentCache.RemoveCacheItem(input.Gateway.Value, input.PaymentId);
                }



                    CreateMileInputDto mob = new CreateMileInputDto();
                    mob.Code = "LD";
                    mob.Name = "Lead";
                    mob.TenantId = tenantId;
                    mob.Id = 0;
                    mob.IsQuotation = false;
                    var milestone2 = mob.MapTo<MileStone>();
                    await _MilestoneRepository.InsertAsync(milestone2);

                    CreateMileInputDto mobj = new CreateMileInputDto();
                    mobj.Code = "QL";
                    mobj.Name = "Qualified";
                    mobj.TenantId = tenantId;
                    mobj.Id = 0;
                    mobj.IsQuotation = false;
                    var milestone = mobj.MapTo<MileStone>();
                    await _MilestoneRepository.InsertAsync(milestone);

                    CreateMileInputDto QT = new CreateMileInputDto();
                    QT.Code = "QT";
                    QT.Name = "Qouted";
                    QT.TenantId = tenantId;
                    QT.Id = 0;
                    QT.IsQuotation = true;
                    var Qouted = QT.MapTo<MileStone>();
                    await _MilestoneRepository.InsertAsync(Qouted);

                    CreateMileInputDto NG = new CreateMileInputDto();
                    NG.Code = "NG";
                    NG.Name = "Negotiation";
                    NG.TenantId = tenantId;
                    NG.Id = 0;
                    NG.IsQuotation = true;
                    var Negotiation = NG.MapTo<MileStone>();
                    await _MilestoneRepository.InsertAsync(Negotiation);

                    CreateMileInputDto CL = new CreateMileInputDto();
                    CL.Code = "CL";
                    CL.Name = "Closed";
                    CL.TenantId = tenantId;
                    CL.Id = 0;
                    CL.IsQuotation = true;
                    var Closed = CL.MapTo<MileStone>();
                    await _MilestoneRepository.InsertAsync(Closed);

                    CreateQuotationStatusInput QS1 = new CreateQuotationStatusInput();
                    QS1.QuotationStatusCode = "NW";
                    QS1.QuotationStatusName = "New";
                    QS1.TenantId = tenantId;
                    QS1.Id = 0;
                    var QS1s = QS1.MapTo<QuotationStatus>();
                    await _QuotationStatusRepository.InsertAsync(QS1s);

                    CreateQuotationStatusInput QS2 = new CreateQuotationStatusInput();
                    QS2.QuotationStatusCode = "SU";
                    QS2.QuotationStatusName = "Submitted";
                    QS2.TenantId = tenantId;
                    QS2.Id = 0;
                    var QS2s = QS2.MapTo<QuotationStatus>();
                    await _QuotationStatusRepository.InsertAsync(QS2s);

                    CreateQuotationStatusInput QS3 = new CreateQuotationStatusInput();
                    QS3.QuotationStatusCode = "WO";
                    QS3.QuotationStatusName = "Won";
                    QS3.TenantId = tenantId;
                    QS3.Id = 0;
                    var QS3s = QS3.MapTo<QuotationStatus>();
                    await _QuotationStatusRepository.InsertAsync(QS3s);

                    CreateQuotationStatusInput QS4 = new CreateQuotationStatusInput();
                    QS4.QuotationStatusCode = "LO";
                    QS4.QuotationStatusName = "Lost";
                    QS4.TenantId = tenantId;
                    QS4.Id = 0;
                    var QS4s = QS4.MapTo<QuotationStatus>();
                    await _QuotationStatusRepository.InsertAsync(QS4s);


                    return new RegisterTenantOutput
                {
                    TenantId = tenant.Id,
                    TenancyName = input.TenancyName,
                    Name = input.Name,
                    UserName = AbpUserBase.AdminUserName,
                    EmailAddress = input.AdminEmailAddress,
                    IsActive = tenant.IsActive,
                    IsEmailConfirmationRequired = isEmailConfirmationRequiredForLogin,
                    IsTenantActive = tenant.IsActive
                };
            }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task CheckRegistrationWithoutEdition()
        {
            var editions = await _editionManager.GetAllAsync();
            if (editions.Any())
            {
                throw new Exception("Tenant registration is not allowed without edition because there are editions defined !");
            }
        }

        public async Task<EditionsSelectOutput> GetEditionsForSelect()
        {
            var features = FeatureManager
                .GetAll()
                .Where(feature => (feature[FeatureMetadata.CustomFeatureKey] as FeatureMetadata)?.IsVisibleOnPricingTable ?? false);

            var flatFeatures = ObjectMapper
                .Map<List<FlatFeatureSelectDto>>(features)
                .OrderBy(f => f.DisplayName)
                .ToList();

            var editions = (await _editionManager.GetAllAsync())
                .Cast<SubscribableEdition>()
                .OrderBy(e => e.MonthlyPrice)
                .ToList();

            var featureDictionary = features.ToDictionary(feature => feature.Name, f => f);

            var editionWithFeatures = new List<EditionWithFeaturesDto>();
            foreach (var edition in editions)
            {
                editionWithFeatures.Add(await CreateEditionWithFeaturesDto(edition, featureDictionary));
            }

            int? tenantEditionId = null;
            if (AbpSession.UserId.HasValue)
            {
                tenantEditionId = (await _tenantManager.GetByIdAsync(AbpSession.GetTenantId()))
                    .EditionId;
            }

            return new EditionsSelectOutput
            {
                AllFeatures = flatFeatures,
                EditionsWithFeatures = editionWithFeatures,
                TenantEditionId = tenantEditionId
            };
        }

        public async Task<EditionSelectDto> GetEdition(int editionId)
        {
            var edition = await _editionManager.GetByIdAsync(editionId);
            var editionDto = ObjectMapper.Map<EditionSelectDto>(edition);
            foreach (var paymentGateway in Enum.GetValues(typeof(SubscriptionPaymentGatewayType)).Cast<SubscriptionPaymentGatewayType>())
            {
                using (var paymentGatewayManager = _paymentGatewayManagerFactory.Create(paymentGateway))
                {
                    var additionalData = await paymentGatewayManager.Object.GetAdditionalPaymentData(ObjectMapper.Map<SubscribableEdition>(edition));
                    editionDto.AdditionalData.Add(paymentGateway, additionalData);
                }
            }

            return editionDto;
        }

        private async Task<EditionWithFeaturesDto> CreateEditionWithFeaturesDto(SubscribableEdition edition, Dictionary<string, Feature> featureDictionary)
        {
            return new EditionWithFeaturesDto
            {
                Edition = ObjectMapper.Map<EditionSelectDto>(edition),
                FeatureValues = (await _editionManager.GetFeatureValuesAsync(edition.Id))
                    .Where(featureValue => featureDictionary.ContainsKey(featureValue.Name))
                    .Select(fv => new NameValueDto(
                        fv.Name,
                        featureDictionary[fv.Name].GetValueText(fv.Value, _localizationContext))
                    )
                    .ToList()
            };
        }

        private void CheckTenantRegistrationIsEnabled()
        {
            if (!IsSelfRegistrationEnabled())
            {
                throw new UserFriendlyException(L("SelfTenantRegistrationIsDisabledMessage_Detail"));
            }

            if (!_multiTenancyConfig.IsEnabled)
            {
                throw new UserFriendlyException(L("MultiTenancyIsNotEnabled"));
            }
        }

        private bool IsSelfRegistrationEnabled()
        {
            return SettingManager.GetSettingValueForApplication<bool>(AppSettings.TenantManagement.AllowSelfRegistration);
        }

        private bool UseCaptchaOnRegistration()
        {
            if (DebugHelper.IsDebug)
            {
                return false;
            }

            return SettingManager.GetSettingValueForApplication<bool>(AppSettings.TenantManagement.UseCaptchaOnRegistration);
        }

        private async Task CheckEditionSubscriptionAsync(int editionId, SubscriptionStartType subscriptionStartType, SubscriptionPaymentGatewayType? gateway, string paymentId)
        {
            var edition = await _editionManager.GetByIdAsync(editionId);
            var subscribableEdition = ObjectMapper.Map<SubscribableEdition>(edition);

            CheckSubscriptionStart(subscribableEdition, subscriptionStartType);
            CheckPaymentCache(subscribableEdition, subscriptionStartType, gateway, paymentId);
        }

        public virtual async Task CreateMilestone(CreateMileInputDto input)
        {
            var milestone = input.MapTo<MileStone>();

            var val = _MilestoneRepository
                .GetAll().Where(p => p.Name == input.Name || p.Code == input.Code).FirstOrDefault();
            if (val == null)
            {
                await _MilestoneRepository.InsertAsync(milestone);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Support Milestone '" + input.Name + "'...");
            }

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
        public async Task<CheckEmailValid> EmailValidation(EmailCheck input)
        {
            var output = new CheckEmailValid();

            var data = "success";
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var user = _userManager.Users.Where(u => u.EmailAddress == input.Email).FirstOrDefault();
                if (user != null)
                {
                    data = "failure";
                }
            }
            output.email = data;
            return output;
        }

        private void CheckPaymentCache(SubscribableEdition edition, SubscriptionStartType subscriptionStartType, SubscriptionPaymentGatewayType? gateway, string paymentId)
        {
            if (edition.IsFree || subscriptionStartType != SubscriptionStartType.Paid)
            {
                return;
            }

            if (!gateway.HasValue)
            {
                throw new Exception("Gateway cannot be empty !");
            }

            if (paymentId.IsNullOrEmpty())
            {
                throw new Exception("PaymentId cannot be empty !");
            }

            var paymentCacheItem = _paymentCache.GetCacheItemOrNull(gateway.Value, paymentId);
            if (paymentCacheItem == null)
            {
                throw new UserFriendlyException(L("PaymentMightBeExpiredWarning"));
            }
        }

        private static void CheckSubscriptionStart(SubscribableEdition edition, SubscriptionStartType subscriptionStartType)
        {
            switch (subscriptionStartType)
            {
                case SubscriptionStartType.Free:
                    if (!edition.IsFree)
                    {
                        throw new Exception("This is not a free edition !");
                    }
                    break;
                case SubscriptionStartType.Trial:
                    if (!edition.HasTrial())
                    {
                        throw new Exception("Trial is not available for this edition !");
                    }
                    break;
                case SubscriptionStartType.Paid:
                    if (edition.IsFree)
                    {
                        throw new Exception("This is a free edition and cannot be subscribed as paid !");
                    }
                    break;
            }
        }
    }
}
