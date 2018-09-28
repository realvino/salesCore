using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tibs.stem.ActivityCommands;
using tibs.stem.Activitys;
using tibs.stem.ActivityTypess;
using tibs.stem.Authorization.Roles;
using tibs.stem.Authorization.Users;
using tibs.stem.Chat;
using tibs.stem.Countrys;
using tibs.stem.Currencies;
using tibs.stem.CustomerCompanys;
using tibs.stem.Deliverys;
using tibs.stem.Editions;
using tibs.stem.Enquirys;
using tibs.stem.Freights;
using tibs.stem.Friendships;
using tibs.stem.MileStones;
using tibs.stem.MileStoneStatusDetails;
using tibs.stem.MileStoneStatuss;
using tibs.stem.MultiTenancy;
using tibs.stem.MultiTenancy.Payments;
using tibs.stem.Packings;
using tibs.stem.PaymentCollections;
using tibs.stem.Payments;
using tibs.stem.PaymentSchedules;
using tibs.stem.PriceLevelProducts;
using tibs.stem.PriceLevels;
using tibs.stem.ProductGroups;
using tibs.stem.Products;
using tibs.stem.ProductSubGroups;
using tibs.stem.QPayments;
using tibs.stem.QuotationProducts;
using tibs.stem.Quotations;
using tibs.stem.QuotationServices;
using tibs.stem.QuotationStatuss;
using tibs.stem.Reasonss;
using tibs.stem.Services;
using tibs.stem.Storage;
using tibs.stem.Targetss;
using tibs.stem.TargetTypess;
using tibs.stem.TenantTargetss;
using tibs.stem.TenantTypes;
using tibs.stem.TitleOfCourtes;
using tibs.stem.TitleOfQuotations;
using tibs.stem.Validitys;
using tibs.stem.VatAmountSettings;
using tibs.stem.Warrantys;

namespace tibs.stem.EntityFrameworkCore
{
    public class stemDbContext : AbpZeroDbContext<Tenant, Role, User, stemDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public DbSet<PersistedGrantEntity> PersistedGrants { get; set; }
        //public virtual DbSet<Freight> Freights { get; set; }
        //17.10.2017
        public virtual DbSet<TitleOfCourtesy> TitleOfCourtes { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Country> Countrys { get; set; }
       

        /// <summary>
        /// /
        /// </summary>
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }

        public virtual DbSet<InfoType> InfoTypes { get; set; }

        public virtual DbSet<Company> Companys { get; set; }

        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<AddressInfo> AddressInfos { get; set; }

        public virtual DbSet<ContactInfo> ContactInfos { get; set; }

        public virtual DbSet<MileStone> MileStones { get; set; }

        public virtual DbSet<MileStoneStatus> MileStoneStatuss { get; set; }

        public virtual DbSet<MileStoneStatusDetail> MileStoneStatusDetails { get; set; }

        public virtual DbSet<ActivityType> ActivityTypes { get; set; }

        public virtual DbSet<CustomCurrency> CustomCurrencies { get; set; }

        public virtual DbSet<Enquiry> Enquiry { get; set; }

        public virtual DbSet<Activity> Activitys { get; set; }

        public virtual DbSet<ProductGroup> ProductGroups { get; set; }

        public virtual DbSet<ProductSubGroup> ProductSubGroups { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ActivityCommand> ActivityCommands { get; set; }

        public virtual DbSet<TenantType> TenantTypes { get; set; }

        public virtual DbSet<Validity> Validitys { get; set; }

        public virtual DbSet<Delivery> Deliverys { get; set; }

        public virtual DbSet<QuotationStatus> QuotationStatuss { get; set; }

        public virtual DbSet<Packing> Packings { get; set; }

        public virtual DbSet<Freight> Freights { get; set; }

        public virtual DbSet<Warranty> Warrantys { get; set; }

        public virtual DbSet<TitleOfQuotation> TitleOfQuotations { get; set; }

        public virtual DbSet<QPayment> QPayments { get; set; }
        public virtual DbSet<Quotation> Quotations { get; set; }
        public virtual DbSet<Reason> Reasonss { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<QuotationProduct> QuotationProducts { get; set; }
        public virtual DbSet<QuotationService> QuotationServices { get; set; }
        public virtual DbSet<PriceLevel> PriceLevelss { get; set; }
        public virtual DbSet<PriceLevelProduct> PriceLevelProductss { get; set; }
        public virtual DbSet<TargetTypes> TargetTypess { get; set; }
        public virtual DbSet<Targets> Targetss { get; set; }
        public virtual DbSet<TenantTargets> TenantTarget { get; set; }
        public virtual DbSet<Payment> Paymentss { get; set; }
        public virtual DbSet<PaymentSchedule> PaymentScheduless { get; set; }
        public virtual DbSet<PaymentCollection> PaymentCollectionss { get; set; }
        public virtual DbSet<TenantVatAmountSetting> TenantVatAmountSettingss { get; set; }

        public stemDbContext(DbContextOptions<stemDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { e.PaymentId, e.Gateway });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
