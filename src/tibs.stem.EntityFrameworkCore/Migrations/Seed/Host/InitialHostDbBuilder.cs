using tibs.stem.EntityFrameworkCore;

namespace tibs.stem.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly stemDbContext _context;

        public InitialHostDbBuilder(stemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new InitialCountryCreater(_context).Create();
            new InitialTitleCreator(_context).Create();
            new InitialCurrencyCreater(_context).Create();
            new DefaultTenantType(_context).Create();
            new DefaultPaymantCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
