using tibs.stem.EntityFrameworkCore;

namespace tibs.stem.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly stemDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(stemDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();
            new TestSubscriptionPaymentBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
