using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tibs.stem.Currencies;
using tibs.stem.EntityFrameworkCore;

namespace tibs.stem.Migrations.Seed.Host
{
    public class InitialCurrencyCreater
    {
        private readonly stemDbContext _context;

        public InitialCurrencyCreater(stemDbContext context)
        {
            _context = context;
        }
       public void Create()
        {
           
             var v1 = _context.Currencies.FirstOrDefault(p => p.Code == "USD");
            if (v1 == null)
            {
                _context.Currencies.Add(
                    new Currency
                    {
                        Code = "USD",
                        Name = "United States Dollar",
                        ConversionRatio = 1
                    });
            }
            var v2 = _context.Currencies.FirstOrDefault(p => p.Code == "AED");
            if (v2 == null)
            {
                _context.Currencies.Add(
                    new Currency
                    {
                        Code = "AED",
                        Name = "Arab Emirates Dirham",
                        ConversionRatio = (decimal)3.67
                    });
            }
            var v3 = _context.Currencies.FirstOrDefault(p => p.Code == "SAR");
            if (v3 == null)
            {
                _context.Currencies.Add(
                    new Currency
                    {
                        Code = "SAR",
                        Name = "Saudi Arabia Riyal",
                        ConversionRatio = (decimal)3.75
                    });
           }
        }
  }
}
