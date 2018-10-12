using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using tibs.stem.EntityFrameworkCore;
using Abp.Authorization;
using Abp.BackgroundJobs;
using Abp.Notifications;
using tibs.stem.Chat;
using tibs.stem.Friendships;
using tibs.stem.MultiTenancy.Payments;

namespace tibs.stem.Migrations
{
    [DbContext(typeof(stemDbContext))]
    [Migration("20180928112236_Add-ReportFilter-Table-2")]
    partial class AddReportFilterTable2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Abp.Application.Editions.Edition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("AbpEditions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Edition");
                });

            modelBuilder.Entity("Abp.Application.Features.FeatureSetting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.ToTable("AbpFeatures");

                    b.HasDiscriminator<string>("Discriminator").HasValue("FeatureSetting");
                });

            modelBuilder.Entity("Abp.Auditing.AuditLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrowserInfo")
                        .HasMaxLength(256);

                    b.Property<string>("ClientIpAddress")
                        .HasMaxLength(64);

                    b.Property<string>("ClientName")
                        .HasMaxLength(128);

                    b.Property<string>("CustomData")
                        .HasMaxLength(2000);

                    b.Property<string>("Exception")
                        .HasMaxLength(2000);

                    b.Property<int>("ExecutionDuration");

                    b.Property<DateTime>("ExecutionTime");

                    b.Property<int?>("ImpersonatorTenantId");

                    b.Property<long?>("ImpersonatorUserId");

                    b.Property<string>("MethodName")
                        .HasMaxLength(256);

                    b.Property<string>("Parameters")
                        .HasMaxLength(1024);

                    b.Property<string>("ServiceName")
                        .HasMaxLength(256);

                    b.Property<int?>("TenantId");

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "ExecutionDuration");

                    b.HasIndex("TenantId", "ExecutionTime");

                    b.HasIndex("TenantId", "UserId");

                    b.ToTable("AbpAuditLogs");
                });

            modelBuilder.Entity("Abp.Authorization.PermissionSetting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsGranted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "Name");

                    b.ToTable("AbpPermissions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PermissionSetting");
                });

            modelBuilder.Entity("Abp.Authorization.Roles.RoleClaim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("RoleId");

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("TenantId", "ClaimType");

                    b.ToTable("AbpRoleClaims");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("EmailAddress");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastLoginTime");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.Property<long?>("UserLinkId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress");

                    b.HasIndex("UserName");

                    b.HasIndex("TenantId", "EmailAddress");

                    b.HasIndex("TenantId", "UserId");

                    b.HasIndex("TenantId", "UserName");

                    b.ToTable("AbpUserAccounts");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserClaim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TenantId", "ClaimType");

                    b.ToTable("AbpUserClaims");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserLogin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TenantId", "UserId");

                    b.HasIndex("TenantId", "LoginProvider", "ProviderKey");

                    b.ToTable("AbpUserLogins");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserLoginAttempt", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrowserInfo")
                        .HasMaxLength(256);

                    b.Property<string>("ClientIpAddress")
                        .HasMaxLength(64);

                    b.Property<string>("ClientName")
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreationTime");

                    b.Property<byte>("Result");

                    b.Property<string>("TenancyName")
                        .HasMaxLength(64);

                    b.Property<int?>("TenantId");

                    b.Property<long?>("UserId");

                    b.Property<string>("UserNameOrEmailAddress")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("UserId", "TenantId");

                    b.HasIndex("TenancyName", "UserNameOrEmailAddress", "Result");

                    b.ToTable("AbpUserLoginAttempts");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserOrganizationUnit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("OrganizationUnitId");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "OrganizationUnitId");

                    b.HasIndex("TenantId", "UserId");

                    b.ToTable("AbpUserOrganizationUnits");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("RoleId");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TenantId", "RoleId");

                    b.HasIndex("TenantId", "UserId");

                    b.ToTable("AbpUserRoles");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TenantId", "UserId");

                    b.ToTable("AbpUserTokens");
                });

            modelBuilder.Entity("Abp.BackgroundJobs.BackgroundJobInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<bool>("IsAbandoned");

                    b.Property<string>("JobArgs")
                        .IsRequired()
                        .HasMaxLength(1048576);

                    b.Property<string>("JobType")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<DateTime?>("LastTryTime");

                    b.Property<DateTime>("NextTryTime");

                    b.Property<byte>("Priority");

                    b.Property<short>("TryCount");

                    b.HasKey("Id");

                    b.HasIndex("IsAbandoned", "NextTryTime");

                    b.ToTable("AbpBackgroundJobs");
                });

            modelBuilder.Entity("Abp.Configuration.Setting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int?>("TenantId");

                    b.Property<long?>("UserId");

                    b.Property<string>("Value")
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TenantId", "Name");

                    b.ToTable("AbpSettings");
                });

            modelBuilder.Entity("Abp.IdentityServer4.PersistedGrantEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(200);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000);

                    b.Property<DateTime?>("Expiration");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.ToTable("AbpPersistedGrants");
                });

            modelBuilder.Entity("Abp.Localization.ApplicationLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Icon")
                        .HasMaxLength(128);

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsDisabled");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "Name");

                    b.ToTable("AbpLanguages");
                });

            modelBuilder.Entity("Abp.Localization.ApplicationLanguageText", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int?>("TenantId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(67108864);

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "Source", "LanguageName", "Key");

                    b.ToTable("AbpLanguageTexts");
                });

            modelBuilder.Entity("Abp.Notifications.NotificationInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Data")
                        .HasMaxLength(1048576);

                    b.Property<string>("DataTypeName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityId")
                        .HasMaxLength(96);

                    b.Property<string>("EntityTypeAssemblyQualifiedName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityTypeName")
                        .HasMaxLength(250);

                    b.Property<string>("ExcludedUserIds")
                        .HasMaxLength(131072);

                    b.Property<string>("NotificationName")
                        .IsRequired()
                        .HasMaxLength(96);

                    b.Property<byte>("Severity");

                    b.Property<string>("TenantIds")
                        .HasMaxLength(131072);

                    b.Property<string>("UserIds")
                        .HasMaxLength(131072);

                    b.HasKey("Id");

                    b.ToTable("AbpNotifications");
                });

            modelBuilder.Entity("Abp.Notifications.NotificationSubscriptionInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("EntityId")
                        .HasMaxLength(96);

                    b.Property<string>("EntityTypeAssemblyQualifiedName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityTypeName")
                        .HasMaxLength(250);

                    b.Property<string>("NotificationName")
                        .HasMaxLength(96);

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("NotificationName", "EntityTypeName", "EntityId", "UserId");

                    b.HasIndex("TenantId", "NotificationName", "EntityTypeName", "EntityId", "UserId");

                    b.ToTable("AbpNotificationSubscriptions");
                });

            modelBuilder.Entity("Abp.Notifications.TenantNotificationInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Data")
                        .HasMaxLength(1048576);

                    b.Property<string>("DataTypeName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityId")
                        .HasMaxLength(96);

                    b.Property<string>("EntityTypeAssemblyQualifiedName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityTypeName")
                        .HasMaxLength(250);

                    b.Property<string>("NotificationName")
                        .IsRequired()
                        .HasMaxLength(96);

                    b.Property<byte>("Severity");

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("AbpTenantNotifications");
                });

            modelBuilder.Entity("Abp.Notifications.UserNotificationInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("State");

                    b.Property<int?>("TenantId");

                    b.Property<Guid>("TenantNotificationId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "State", "CreationTime");

                    b.ToTable("AbpUserNotifications");
                });

            modelBuilder.Entity("Abp.Organizations.OrganizationUnit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(95);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<long?>("ParentId");

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("TenantId", "Code");

                    b.ToTable("AbpOrganizationUnits");
                });

            modelBuilder.Entity("tibs.stem.ActivityCommands.ActivityCommand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityId");

                    b.Property<string>("Commands");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("ActivityCommand");
                });

            modelBuilder.Entity("tibs.stem.Activitys.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityTypeId");

                    b.Property<int?>("ContactId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Description");

                    b.Property<int?>("EnquiryId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<DateTime?>("ScheduleTime");

                    b.Property<bool>("Status");

                    b.Property<int>("TenantId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTypeId");

                    b.HasIndex("ContactId");

                    b.HasIndex("EnquiryId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("tibs.stem.ActivityTypess.ActivityType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("ActivityType");
                });

            modelBuilder.Entity("tibs.stem.Authorization.Roles.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<bool>("IsDefault");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsStatic");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("DeleterUserId");

                    b.HasIndex("LastModifierUserId");

                    b.HasIndex("TenantId", "NormalizedName");

                    b.ToTable("AbpRoles");
                });

            modelBuilder.Entity("tibs.stem.Authorization.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AuthenticationSource")
                        .HasMaxLength(64);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("EmailConfirmationCode")
                        .HasMaxLength(328);

                    b.Property<string>("GoogleAuthenticatorKey");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsEmailConfirmed");

                    b.Property<bool>("IsLockoutEnabled");

                    b.Property<bool>("IsPhoneNumberConfirmed");

                    b.Property<bool>("IsTwoFactorEnabled");

                    b.Property<DateTime?>("LastLoginTime");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<DateTime?>("LockoutEndDateUtc");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("NormalizedEmailAddress")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("PasswordResetCode")
                        .HasMaxLength(328);

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("ProfileImage");

                    b.Property<Guid?>("ProfilePictureId");

                    b.Property<int>("RootId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("ShouldChangePasswordOnNextLogin");

                    b.Property<string>("SignInToken");

                    b.Property<DateTime?>("SignInTokenExpireTimeUtc");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int?>("TenantId");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("DeleterUserId");

                    b.HasIndex("LastModifierUserId");

                    b.HasIndex("TenantId", "NormalizedEmailAddress");

                    b.HasIndex("TenantId", "NormalizedUserName");

                    b.ToTable("AbpUsers");
                });

            modelBuilder.Entity("tibs.stem.Chat.ChatMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(4096);

                    b.Property<int>("ReadState");

                    b.Property<int>("Side");

                    b.Property<int?>("TargetTenantId");

                    b.Property<long>("TargetUserId");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                    b.HasIndex("TargetTenantId", "UserId", "ReadState");

                    b.HasIndex("TenantId", "TargetUserId", "ReadState");

                    b.HasIndex("TenantId", "UserId", "ReadState");

                    b.ToTable("AppChatMessages");
                });

            modelBuilder.Entity("tibs.stem.Countrys.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryCode");

                    b.Property<string>("CountryName");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("ISDCode");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("tibs.stem.Currencies.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<decimal>("ConversionRatio");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("tibs.stem.Currencies.CustomCurrency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<decimal>("ConversionRatio");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("CurrencyId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<bool>("Online");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("CustomCurrency");
                });

            modelBuilder.Entity("tibs.stem.CustomerCompanys.AddressInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<int?>("CompanyId");

                    b.Property<int?>("ContacId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int?>("InfoTypeId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ContacId");

                    b.HasIndex("InfoTypeId");

                    b.ToTable("AddressInformation");
                });

            modelBuilder.Entity("tibs.stem.CustomerCompanys.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AccountManagerId");

                    b.Property<int?>("CountryId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int?>("CurrencyId");

                    b.Property<int?>("CustomerTypeId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("AccountManagerId");

                    b.HasIndex("CountryId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("CustomerTypeId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("tibs.stem.CustomerCompanys.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CompanyId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int?>("CustomerTypeId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<int>("TenantId");

                    b.Property<int?>("TitleId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CustomerTypeId");

                    b.HasIndex("TitleId");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("tibs.stem.CustomerCompanys.ContactInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CompanyId");

                    b.Property<int?>("ContacId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("InfoData");

                    b.Property<int?>("InfoTypeId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ContacId");

                    b.HasIndex("InfoTypeId");

                    b.ToTable("ContactInformation");
                });

            modelBuilder.Entity("tibs.stem.CustomerCompanys.CustomerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Company");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("TenantId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("CustomerType");
                });

            modelBuilder.Entity("tibs.stem.CustomerCompanys.InfoType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactName");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool?>("Info");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("InfoType");
                });

            modelBuilder.Entity("tibs.stem.Deliverys.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DeliveryCode");

                    b.Property<string>("DeliveryName");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("Delivery");
                });

            modelBuilder.Entity("tibs.stem.Enquirys.Enquiry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CloseDate");

                    b.Property<int>("CompanyId");

                    b.Property<int?>("ContactId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("EnquiryNo");

                    b.Property<decimal>("EstimationValue");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("MileStoneId");

                    b.Property<int?>("MileStoneStatusId");

                    b.Property<string>("Remarks");

                    b.Property<int>("TenantId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ContactId");

                    b.HasIndex("MileStoneId");

                    b.HasIndex("MileStoneStatusId");

                    b.ToTable("Enquiry");
                });

            modelBuilder.Entity("tibs.stem.Freights.Freight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("FreightCode");

                    b.Property<string>("FreightName");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("Freight");
                });

            modelBuilder.Entity("tibs.stem.Friendships.Friendship", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<Guid?>("FriendProfilePictureId");

                    b.Property<string>("FriendTenancyName");

                    b.Property<int?>("FriendTenantId");

                    b.Property<long>("FriendUserId");

                    b.Property<string>("FriendUserName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("State");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FriendTenantId", "FriendUserId");

                    b.HasIndex("FriendTenantId", "UserId");

                    b.HasIndex("TenantId", "FriendUserId");

                    b.HasIndex("TenantId", "UserId");

                    b.ToTable("AppFriendships");
                });

            modelBuilder.Entity("tibs.stem.MileStones.MileStone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsQuotation");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("MileStone");
                });

            modelBuilder.Entity("tibs.stem.MileStoneStatusDetails.MileStoneStatusDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("MileStoneId");

                    b.Property<int>("MileStoneStatusId");

                    b.HasKey("Id");

                    b.HasIndex("MileStoneId");

                    b.HasIndex("MileStoneStatusId");

                    b.ToTable("MileStoneStatusDetail");
                });

            modelBuilder.Entity("tibs.stem.MileStoneStatuss.MileStoneStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Color");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("MileStoneStatus");
                });

            modelBuilder.Entity("tibs.stem.MultiTenancy.Payments.SubscriptionPayment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("DayCount");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int>("EditionId");

                    b.Property<int>("Gateway");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("PaymentId");

                    b.Property<int?>("PaymentPeriodType");

                    b.Property<int>("Status");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("EditionId");

                    b.HasIndex("PaymentId", "Gateway");

                    b.HasIndex("Status", "CreationTime");

                    b.ToTable("AppSubscriptionPayments");
                });

            modelBuilder.Entity("tibs.stem.MultiTenancy.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConnectionString")
                        .HasMaxLength(1024);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<Guid?>("CustomCssId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int?>("EditionId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsInTrialPeriod");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LogoFileType")
                        .HasMaxLength(64);

                    b.Property<Guid?>("LogoId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<DateTime?>("SubscriptionEndDateUtc");

                    b.Property<string>("TenancyName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int?>("TenantTypeId");

                    b.HasKey("Id");

                    b.HasIndex("CreationTime");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("DeleterUserId");

                    b.HasIndex("EditionId");

                    b.HasIndex("LastModifierUserId");

                    b.HasIndex("SubscriptionEndDateUtc");

                    b.HasIndex("TenancyName");

                    b.HasIndex("TenantTypeId");

                    b.ToTable("AbpTenants");
                });

            modelBuilder.Entity("tibs.stem.Packings.Packing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("PackingCode");

                    b.Property<string>("PackingName");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("Packing");
                });

            modelBuilder.Entity("tibs.stem.PaymentCollections.PaymentCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime?>("BankDate");

                    b.Property<DateTime?>("ChequeDate");

                    b.Property<string>("ChequeNo");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int?>("CurrencyId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<decimal>("DueAmount");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int?>("PaymentId");

                    b.Property<int>("QuotationId");

                    b.Property<bool?>("Received");

                    b.Property<string>("Remarks");

                    b.Property<string>("SalesInvoiceNUmber");

                    b.Property<int>("TenantId");

                    b.Property<string>("VoucherNo");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("QuotationId");

                    b.ToTable("PaymentCollection");
                });

            modelBuilder.Entity("tibs.stem.Payments.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("tibs.stem.PaymentSchedules.PaymentSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("QuotationId");

                    b.Property<DateTime?>("ScheduledDate");

                    b.Property<int>("TenantId");

                    b.Property<decimal?>("Total");

                    b.HasKey("Id");

                    b.HasIndex("QuotationId");

                    b.ToTable("PaymentSchedule");
                });

            modelBuilder.Entity("tibs.stem.PriceLevelProducts.PriceLevelProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<double>("Price");

                    b.Property<int>("PriceLevelId");

                    b.Property<int>("ProductId");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("PriceLevelId");

                    b.HasIndex("ProductId");

                    b.ToTable("PriceLevelProduct");
                });

            modelBuilder.Entity("tibs.stem.PriceLevels.PriceLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("PriceLevelCode");

                    b.Property<string>("PriceLevelName");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("PriceLevel");
                });

            modelBuilder.Entity("tibs.stem.ProductGroups.ProductGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Path");

                    b.Property<string>("ProductGroupCode");

                    b.Property<string>("ProductGroupName");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("ProductGroup");
                });

            modelBuilder.Entity("tibs.stem.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Description");

                    b.Property<bool?>("Discontinued");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Path");

                    b.Property<string>("ProductCode");

                    b.Property<string>("ProductName");

                    b.Property<int>("ProductSubGroupId");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("ProductSubGroupId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("tibs.stem.ProductSubGroups.ProductSubGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Path");

                    b.Property<int>("ProductGroupId");

                    b.Property<string>("ProductSubGroupCode");

                    b.Property<string>("ProductSubGroupName");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("ProductGroupId");

                    b.ToTable("ProductSubGroup");
                });

            modelBuilder.Entity("tibs.stem.QPayments.QPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("PaymentCode");

                    b.Property<string>("PaymentName");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("QPayment");
                });

            modelBuilder.Entity("tibs.stem.QuotationProducts.QuotationProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool?>("Discount");

                    b.Property<decimal>("EstimatedPrice");

                    b.Property<decimal>("EstimatedPriceUSD");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<bool?>("Optional");

                    b.Property<decimal>("Price");

                    b.Property<int?>("PriceLevelProductId");

                    b.Property<decimal>("PriceUSD");

                    b.Property<int?>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<int?>("QuotationId");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("PriceLevelProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("QuotationId");

                    b.ToTable("QuotationProduct");
                });

            modelBuilder.Entity("tibs.stem.Quotations.Quotation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Archived");

                    b.Property<DateTime?>("ClosureDate");

                    b.Property<int?>("CompanyId");

                    b.Property<int?>("CompetitorId");

                    b.Property<int?>("ContactId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int?>("CurrencyId");

                    b.Property<string>("CustomerPONumber");

                    b.Property<DateTime?>("Date");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int?>("DeliveryId");

                    b.Property<int?>("EnquiryId");

                    b.Property<decimal>("ExchangeRate");

                    b.Property<int?>("FreightId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<bool>("Lost");

                    b.Property<DateTime?>("LostDate");

                    b.Property<int?>("MileStoneId");

                    b.Property<decimal>("OverallDiscount");

                    b.Property<decimal>("OverallDiscountinUSD");

                    b.Property<int?>("PackingId");

                    b.Property<int?>("PaymentId");

                    b.Property<string>("ProjectRef");

                    b.Property<string>("ProposalNumber");

                    b.Property<int?>("QuotationTitleId");

                    b.Property<int?>("ReasonId");

                    b.Property<bool?>("Revised");

                    b.Property<string>("SalesOrderNumber");

                    b.Property<long?>("SalesmanId");

                    b.Property<int?>("StatusId");

                    b.Property<string>("SubjectName");

                    b.Property<bool>("Submitted");

                    b.Property<DateTime?>("SubmittedDate");

                    b.Property<int>("TenantId");

                    b.Property<decimal>("Total");

                    b.Property<int?>("ValidityId");

                    b.Property<bool>("Vat");

                    b.Property<decimal>("VatAmount");

                    b.Property<decimal>("VatPercentage");

                    b.Property<int?>("WarrantyId");

                    b.Property<bool>("Won");

                    b.Property<DateTime?>("WonDate");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CompetitorId");

                    b.HasIndex("ContactId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("EnquiryId");

                    b.HasIndex("FreightId");

                    b.HasIndex("MileStoneId");

                    b.HasIndex("PackingId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("QuotationTitleId");

                    b.HasIndex("ReasonId");

                    b.HasIndex("SalesmanId");

                    b.HasIndex("StatusId");

                    b.HasIndex("ValidityId");

                    b.HasIndex("WarrantyId");

                    b.ToTable("Quotation");
                });

            modelBuilder.Entity("tibs.stem.QuotationServices.QuotationService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("CovertPrice");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<decimal>("Price");

                    b.Property<int?>("QuotationId");

                    b.Property<int?>("ServiceId");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("QuotationId");

                    b.HasIndex("ServiceId");

                    b.ToTable("QuotationService");
                });

            modelBuilder.Entity("tibs.stem.QuotationStatuss.QuotationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<bool>("Lost");

                    b.Property<int?>("MileStoneId");

                    b.Property<bool>("New");

                    b.Property<string>("QuotationStatusCode");

                    b.Property<string>("QuotationStatusName");

                    b.Property<bool>("Revised");

                    b.Property<bool>("Submitted");

                    b.Property<int>("TenantId");

                    b.Property<bool>("Won");

                    b.HasKey("Id");

                    b.HasIndex("MileStoneId");

                    b.ToTable("QuotationStatus");
                });

            modelBuilder.Entity("tibs.stem.Reasonss.Reason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("Reason");
                });

            modelBuilder.Entity("tibs.stem.Reports.ReportGenerator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Creator");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Currency");

                    b.Property<string>("CustomerType");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<DateTime>("EnquiryCreationTime");

                    b.Property<int>("EnquiryCreationTypeId");

                    b.Property<string>("HiddenColumns");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<DateTime>("LostDate");

                    b.Property<int>("LostDateId");

                    b.Property<string>("MileStone");

                    b.Property<string>("MileStoneStatus");

                    b.Property<string>("Name");

                    b.Property<DateTime>("QuotationCreationTime");

                    b.Property<int>("QuotationCreationTypeId");

                    b.Property<string>("QuotationStatus");

                    b.Property<int>("ReportTypeId");

                    b.Property<string>("Salesperson");

                    b.Property<DateTime>("SubmitttedDate");

                    b.Property<int>("SubmitttedDateId");

                    b.Property<int>("TenantId");

                    b.Property<DateTime>("WonDate");

                    b.Property<int>("WonDateId");

                    b.HasKey("Id");

                    b.ToTable("ReportGenerator");
                });

            modelBuilder.Entity("tibs.stem.Services.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("ServiceCode");

                    b.Property<string>("ServiceName");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("tibs.stem.Storage.BinaryObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Bytes")
                        .IsRequired();

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("AppBinaryObjects");
                });

            modelBuilder.Entity("tibs.stem.Targetss.Targets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("TargetAmount");

                    b.Property<int>("TargetTypeId");

                    b.Property<int>("TenantId");

                    b.Property<decimal>("Total");

                    b.Property<long>("UserId");

                    b.Property<DateTime>("ValidityDate");

                    b.HasKey("Id");

                    b.HasIndex("TargetTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("tibs.stem.TargetTypess.TargetTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TargetTypes");
                });

            modelBuilder.Entity("tibs.stem.TenantTargetss.TenantTargets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<DateTime>("TargetDate");

                    b.Property<int>("TenantId");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.ToTable("TenantTargets");
                });

            modelBuilder.Entity("tibs.stem.TenantTypes.TenantType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TenantType");
                });

            modelBuilder.Entity("tibs.stem.TitleOfCourtes.TitleOfCourtesy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TitleOfCourtesy");
                });

            modelBuilder.Entity("tibs.stem.TitleOfQuotations.TitleOfQuotation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("TitleOfQuotation");
                });

            modelBuilder.Entity("tibs.stem.Validitys.Validity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("TenantId");

                    b.Property<string>("ValidityCode");

                    b.Property<string>("ValidityName");

                    b.HasKey("Id");

                    b.ToTable("Validity");
                });

            modelBuilder.Entity("tibs.stem.VatAmountSettings.TenantVatAmountSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("TenantId");

                    b.Property<decimal>("VatAmount");

                    b.HasKey("Id");

                    b.ToTable("SettingVatAmount");
                });

            modelBuilder.Entity("tibs.stem.Warrantys.Warranty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("TenantId");

                    b.Property<string>("WarrantyCode");

                    b.Property<string>("WarrantyName");

                    b.HasKey("Id");

                    b.ToTable("Warranty");
                });

            modelBuilder.Entity("tibs.stem.Editions.SubscribableEdition", b =>
                {
                    b.HasBaseType("Abp.Application.Editions.Edition");

                    b.Property<decimal?>("AnnualPrice");

                    b.Property<int?>("ExpiringEditionId");

                    b.Property<decimal?>("MonthlyPrice");

                    b.Property<int?>("TrialDayCount");

                    b.Property<int?>("WaitingDayAfterExpire");

                    b.ToTable("AbpEditions");

                    b.HasDiscriminator().HasValue("SubscribableEdition");
                });

            modelBuilder.Entity("Abp.Application.Features.EditionFeatureSetting", b =>
                {
                    b.HasBaseType("Abp.Application.Features.FeatureSetting");

                    b.Property<int>("EditionId");

                    b.HasIndex("EditionId", "Name");

                    b.ToTable("AbpFeatures");

                    b.HasDiscriminator().HasValue("EditionFeatureSetting");
                });

            modelBuilder.Entity("Abp.MultiTenancy.TenantFeatureSetting", b =>
                {
                    b.HasBaseType("Abp.Application.Features.FeatureSetting");

                    b.Property<int>("TenantId");

                    b.HasIndex("TenantId", "Name");

                    b.ToTable("AbpFeatures");

                    b.HasDiscriminator().HasValue("TenantFeatureSetting");
                });

            modelBuilder.Entity("Abp.Authorization.Roles.RolePermissionSetting", b =>
                {
                    b.HasBaseType("Abp.Authorization.PermissionSetting");

                    b.Property<int>("RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AbpPermissions");

                    b.HasDiscriminator().HasValue("RolePermissionSetting");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserPermissionSetting", b =>
                {
                    b.HasBaseType("Abp.Authorization.PermissionSetting");

                    b.Property<long>("UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AbpPermissions");

                    b.HasDiscriminator().HasValue("UserPermissionSetting");
                });

            modelBuilder.Entity("Abp.Authorization.Roles.RoleClaim", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Roles.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserClaim", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserLogin", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserRole", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserToken", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Configuration.Setting", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User")
                        .WithMany("Settings")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Abp.Organizations.OrganizationUnit", b =>
                {
                    b.HasOne("Abp.Organizations.OrganizationUnit", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("tibs.stem.ActivityCommands.ActivityCommand", b =>
                {
                    b.HasOne("tibs.stem.Activitys.Activity", "Activitys")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.Activitys.Activity", b =>
                {
                    b.HasOne("tibs.stem.ActivityTypess.ActivityType", "ActivityTypes")
                        .WithMany()
                        .HasForeignKey("ActivityTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.CustomerCompanys.Contact", "Contacts")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.HasOne("tibs.stem.Enquirys.Enquiry", "Enquirys")
                        .WithMany()
                        .HasForeignKey("EnquiryId");
                });

            modelBuilder.Entity("tibs.stem.Authorization.Roles.Role", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "DeleterUser")
                        .WithMany()
                        .HasForeignKey("DeleterUserId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "LastModifierUser")
                        .WithMany()
                        .HasForeignKey("LastModifierUserId");
                });

            modelBuilder.Entity("tibs.stem.Authorization.Users.User", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "DeleterUser")
                        .WithMany()
                        .HasForeignKey("DeleterUserId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "LastModifierUser")
                        .WithMany()
                        .HasForeignKey("LastModifierUserId");
                });

            modelBuilder.Entity("tibs.stem.Currencies.CustomCurrency", b =>
                {
                    b.HasOne("tibs.stem.Currencies.Currency", "Currencys")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.CustomerCompanys.AddressInfo", b =>
                {
                    b.HasOne("tibs.stem.CustomerCompanys.Company", "Companys")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("tibs.stem.CustomerCompanys.Contact", "Contacts")
                        .WithMany()
                        .HasForeignKey("ContacId");

                    b.HasOne("tibs.stem.CustomerCompanys.InfoType", "InfoTypes")
                        .WithMany()
                        .HasForeignKey("InfoTypeId");
                });

            modelBuilder.Entity("tibs.stem.CustomerCompanys.Company", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "AbpAccountManager")
                        .WithMany()
                        .HasForeignKey("AccountManagerId");

                    b.HasOne("tibs.stem.Countrys.Country", "Countrys")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("tibs.stem.Currencies.Currency", "Currencys")
                        .WithMany()
                        .HasForeignKey("CurrencyId");

                    b.HasOne("tibs.stem.CustomerCompanys.CustomerType", "CustomerTypes")
                        .WithMany()
                        .HasForeignKey("CustomerTypeId");
                });

            modelBuilder.Entity("tibs.stem.CustomerCompanys.Contact", b =>
                {
                    b.HasOne("tibs.stem.CustomerCompanys.Company", "Companys")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("tibs.stem.CustomerCompanys.CustomerType", "CustomerTypes")
                        .WithMany()
                        .HasForeignKey("CustomerTypeId");

                    b.HasOne("tibs.stem.TitleOfCourtes.TitleOfCourtesy", "TitleOfCourtesies")
                        .WithMany()
                        .HasForeignKey("TitleId");
                });

            modelBuilder.Entity("tibs.stem.CustomerCompanys.ContactInfo", b =>
                {
                    b.HasOne("tibs.stem.CustomerCompanys.Company", "Companys")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("tibs.stem.CustomerCompanys.Contact", "Contacts")
                        .WithMany()
                        .HasForeignKey("ContacId");

                    b.HasOne("tibs.stem.CustomerCompanys.InfoType", "InfoTypes")
                        .WithMany()
                        .HasForeignKey("InfoTypeId");
                });

            modelBuilder.Entity("tibs.stem.Enquirys.Enquiry", b =>
                {
                    b.HasOne("tibs.stem.CustomerCompanys.Company", "Companys")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.CustomerCompanys.Contact", "Contacts")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.HasOne("tibs.stem.MileStones.MileStone", "MileStones")
                        .WithMany()
                        .HasForeignKey("MileStoneId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.MileStoneStatuss.MileStoneStatus", "MileStoneStatuss")
                        .WithMany()
                        .HasForeignKey("MileStoneStatusId");
                });

            modelBuilder.Entity("tibs.stem.MileStoneStatusDetails.MileStoneStatusDetail", b =>
                {
                    b.HasOne("tibs.stem.MileStones.MileStone", "MileStones")
                        .WithMany()
                        .HasForeignKey("MileStoneId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.MileStoneStatuss.MileStoneStatus", "EnquiryStatuss")
                        .WithMany()
                        .HasForeignKey("MileStoneStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.MultiTenancy.Payments.SubscriptionPayment", b =>
                {
                    b.HasOne("Abp.Application.Editions.Edition", "Edition")
                        .WithMany()
                        .HasForeignKey("EditionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.MultiTenancy.Tenant", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "DeleterUser")
                        .WithMany()
                        .HasForeignKey("DeleterUserId");

                    b.HasOne("Abp.Application.Editions.Edition", "Edition")
                        .WithMany()
                        .HasForeignKey("EditionId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "LastModifierUser")
                        .WithMany()
                        .HasForeignKey("LastModifierUserId");

                    b.HasOne("tibs.stem.TenantTypes.TenantType", "TenantTypes")
                        .WithMany()
                        .HasForeignKey("TenantTypeId");
                });

            modelBuilder.Entity("tibs.stem.PaymentCollections.PaymentCollection", b =>
                {
                    b.HasOne("tibs.stem.Currencies.Currency", "Currencys")
                        .WithMany()
                        .HasForeignKey("CurrencyId");

                    b.HasOne("tibs.stem.Payments.Payment", "Payments")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.HasOne("tibs.stem.Quotations.Quotation", "Quotations")
                        .WithMany()
                        .HasForeignKey("QuotationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.PaymentSchedules.PaymentSchedule", b =>
                {
                    b.HasOne("tibs.stem.Quotations.Quotation", "Quotations")
                        .WithMany()
                        .HasForeignKey("QuotationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.PriceLevelProducts.PriceLevelProduct", b =>
                {
                    b.HasOne("tibs.stem.PriceLevels.PriceLevel", "PriceLevels")
                        .WithMany()
                        .HasForeignKey("PriceLevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.Products.Product", "Products")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.Products.Product", b =>
                {
                    b.HasOne("tibs.stem.ProductSubGroups.ProductSubGroup", "ProductSubGroup")
                        .WithMany()
                        .HasForeignKey("ProductSubGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.ProductSubGroups.ProductSubGroup", b =>
                {
                    b.HasOne("tibs.stem.ProductGroups.ProductGroup", "productGroups")
                        .WithMany()
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.QuotationProducts.QuotationProduct", b =>
                {
                    b.HasOne("tibs.stem.PriceLevelProducts.PriceLevelProduct", "PriceLevelProducts")
                        .WithMany()
                        .HasForeignKey("PriceLevelProductId");

                    b.HasOne("tibs.stem.Products.Product", "Products")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("tibs.stem.Quotations.Quotation", "Quotations")
                        .WithMany()
                        .HasForeignKey("QuotationId");
                });

            modelBuilder.Entity("tibs.stem.Quotations.Quotation", b =>
                {
                    b.HasOne("tibs.stem.CustomerCompanys.Company", "Companys")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("tibs.stem.CustomerCompanys.Company", "Competitor")
                        .WithMany()
                        .HasForeignKey("CompetitorId");

                    b.HasOne("tibs.stem.CustomerCompanys.Contact", "Contacts")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.HasOne("tibs.stem.Currencies.Currency", "Currencys")
                        .WithMany()
                        .HasForeignKey("CurrencyId");

                    b.HasOne("tibs.stem.Deliverys.Delivery", "Deliverys")
                        .WithMany()
                        .HasForeignKey("DeliveryId");

                    b.HasOne("tibs.stem.Enquirys.Enquiry", "Enquirys")
                        .WithMany()
                        .HasForeignKey("EnquiryId");

                    b.HasOne("tibs.stem.Freights.Freight", "Freights")
                        .WithMany()
                        .HasForeignKey("FreightId");

                    b.HasOne("tibs.stem.MileStones.MileStone", "MileStones")
                        .WithMany()
                        .HasForeignKey("MileStoneId");

                    b.HasOne("tibs.stem.Packings.Packing", "Packings")
                        .WithMany()
                        .HasForeignKey("PackingId");

                    b.HasOne("tibs.stem.QPayments.QPayment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.HasOne("tibs.stem.TitleOfQuotations.TitleOfQuotation", "QuotationTitle")
                        .WithMany()
                        .HasForeignKey("QuotationTitleId");

                    b.HasOne("tibs.stem.Reasonss.Reason", "Reasons")
                        .WithMany()
                        .HasForeignKey("ReasonId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "Salesman")
                        .WithMany()
                        .HasForeignKey("SalesmanId");

                    b.HasOne("tibs.stem.QuotationStatuss.QuotationStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("tibs.stem.Validitys.Validity", "Validitys")
                        .WithMany()
                        .HasForeignKey("ValidityId");

                    b.HasOne("tibs.stem.Warrantys.Warranty", "Warrantys")
                        .WithMany()
                        .HasForeignKey("WarrantyId");
                });

            modelBuilder.Entity("tibs.stem.QuotationServices.QuotationService", b =>
                {
                    b.HasOne("tibs.stem.Quotations.Quotation", "Quotations")
                        .WithMany()
                        .HasForeignKey("QuotationId");

                    b.HasOne("tibs.stem.Services.Service", "Services")
                        .WithMany()
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("tibs.stem.QuotationStatuss.QuotationStatus", b =>
                {
                    b.HasOne("tibs.stem.MileStones.MileStone", "MileStones")
                        .WithMany()
                        .HasForeignKey("MileStoneId");
                });

            modelBuilder.Entity("tibs.stem.Targetss.Targets", b =>
                {
                    b.HasOne("tibs.stem.TargetTypess.TargetTypes", "TargetType")
                        .WithMany()
                        .HasForeignKey("TargetTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.Authorization.Users.User", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Application.Features.EditionFeatureSetting", b =>
                {
                    b.HasOne("Abp.Application.Editions.Edition", "Edition")
                        .WithMany()
                        .HasForeignKey("EditionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Authorization.Roles.RolePermissionSetting", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Roles.Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserPermissionSetting", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User")
                        .WithMany("Permissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
