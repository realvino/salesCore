using tibs.stem.CustomerCompanys;

namespace tibs.stem.Migrations.Seed.Tenants
{
    internal class NewCustomerTypeInputDto : CustomerType
    {
        public virtual string Title { get; set; }
        public virtual bool Company { get; set; }
        public virtual int TenantId { get; set; }
    }
}