using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.EntityFrameworkCore;
using tibs.stem.TitleOfCourtes;

namespace tibs.stem.Migrations.Seed.Host
{
    public class InitialTitleCreator
    {
        private readonly stemDbContext _context;

        public InitialTitleCreator(stemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var v1 = _context.TitleOfCourtes.FirstOrDefault(p => p.Name == "Mr");
            if (v1 == null)
            {
                _context.TitleOfCourtes.Add(
                    new TitleOfCourtesy
                    {
                        Name = "Mr"
                    });
            }

            var v2 = _context.TitleOfCourtes.FirstOrDefault(p => p.Name == "Mrs");
            if (v2 == null)
            {
                _context.TitleOfCourtes.Add(
                    new TitleOfCourtesy
                    {
                        Name = "Mrs"
                    });
            }
            var v3 = _context.TitleOfCourtes.FirstOrDefault(p => p.Name == "Ms");
            if (v3 == null)
            {
                _context.TitleOfCourtes.Add(
                    new TitleOfCourtesy
                    {
                        Name = "Ms"
                    });
            }
            var v4 = _context.TitleOfCourtes.FirstOrDefault(p => p.Name == "Dr");
            if (v4 == null)
            {
                _context.TitleOfCourtes.Add(
                    new TitleOfCourtesy
                    {
                        Name = "Dr"
                    });
            }
            var v5 = _context.TitleOfCourtes.FirstOrDefault(p => p.Name == "Eng");
            if (v5 == null)
            {
                _context.TitleOfCourtes.Add(
                    new TitleOfCourtesy
                    {
                        Name = "Eng"
                    });
            }
        }

    }
}
