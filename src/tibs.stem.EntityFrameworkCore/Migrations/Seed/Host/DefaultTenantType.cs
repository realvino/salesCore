using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tibs.stem.EntityFrameworkCore;
using tibs.stem.TenantTypes;

namespace tibs.stem.Migrations.Seed.Host
{
   public class DefaultTenantType
    {
        private readonly stemDbContext _context;

        public DefaultTenantType(stemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            
            var v1 = _context.TenantTypes.FirstOrDefault(p => p.Name == "Product");
            if (v1 == null)
            {
                _context.TenantTypes.Add(
                    new TenantType
                    {
                        Code="PRD",
                        Name = "Product"
                    });
            }

            var v2 = _context.TenantTypes.FirstOrDefault(p => p.Name == "Service");
            if (v2 == null)
            {
                _context.TenantTypes.Add(
                    new TenantType
                    {
                        Code = "SER",
                        Name = "Service"
                    });
            }
           
        }
    }
}
