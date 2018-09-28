using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tibs.stem.Countrys;
using tibs.stem.EntityFrameworkCore;

namespace tibs.stem.Migrations.Seed.Host
{
   public class InitialCountryCreater
    {

        private readonly stemDbContext _context;

        public InitialCountryCreater(stemDbContext context)
        {
            _context = context; 
        }
        public void Create()
        {
            var v1 = _context.Countrys.FirstOrDefault(p => p.CountryCode == "UAE");
            if (v1 == null)
            {
                _context.Countrys.Add(
                    new Country
                    {
                        CountryCode = "UAE",
                        CountryName = "United Arab Emirates",
                        ISDCode = "+971"
                    });
            }
            var v2 = _context.Countrys.FirstOrDefault(p => p.CountryCode == "SA");
            if (v2 == null)
            {
                _context.Countrys.Add(
                    new Country
                    {
                        CountryCode = "SA",
                        CountryName = "Saudi Arabia",
                        ISDCode = "+966"
                    });
            }
            var v3 = _context.Countrys.FirstOrDefault(p => p.CountryCode == "QT");
            if (v3 == null)
            {
                _context.Countrys.Add(
                    new Country
                    {
                        CountryCode = "QT",
                        CountryName = "Qatar",
                        ISDCode = "+974"
                    });
            }
            var v4 = _context.Countrys.FirstOrDefault(p => p.CountryCode == "OM");
            if (v4 == null)
            {
                _context.Countrys.Add(
                    new Country
                    {
                        CountryCode = "OM",
                        CountryName = "Oman",
                        ISDCode = "+968"
                    });
            }
            var v5 = _context.Countrys.FirstOrDefault(p => p.CountryCode == "BAH");
            if (v5 == null)
            {
                _context.Countrys.Add(
                    new Country
                    {
                        CountryCode = "BAH",
                        CountryName = "Bahrain",
                        ISDCode = "+973"
                    });

                var v6 = _context.Countrys.FirstOrDefault(p => p.CountryCode == "EGP");
                if (v6 == null)
                {
                    _context.Countrys.Add(
                        new Country
                        {
                            CountryCode = "EGP",
                            CountryName = "Egypt",
                            ISDCode = "+20"
                        });
                }

            }
        }
   }
}
