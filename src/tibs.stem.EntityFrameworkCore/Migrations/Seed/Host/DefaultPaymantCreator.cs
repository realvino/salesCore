using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.EntityFrameworkCore;
using tibs.stem.Payments;

namespace tibs.stem.Migrations.Seed.Host
{
    public class DefaultPaymantCreator
    {
        private readonly stemDbContext _context;

        public DefaultPaymantCreator(stemDbContext context)
        {
            _context = context;
        }
        public void Create()
        {
            var v1 = _context.Paymentss.FirstOrDefault(p => p.Name == "Bank");
            if (v1 == null)
            {
                _context.Paymentss.Add(
                    new Payment
                    {
                        Name = "Bank",
                    });
            }
            var v2 = _context.Paymentss.FirstOrDefault(p => p.Name == "Cash");
            if (v2 == null)
            {
                _context.Paymentss.Add(
                    new Payment
                    {
                        Name = "Cash",
                    });
            }
        }

    }
}
