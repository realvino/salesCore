using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Report.Dto
{
    public class CompanyReportList
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Salesperson { get; set; }
        public virtual string Creator { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual string CountryOrCompanyName { get; set; }
        public virtual string CustomerType { get; set; }
        public virtual string CurrencyOrTitleName { get; set; }
        public virtual int EnquiryCount { get; set; }
        public virtual int QuotationCount { get; set; }
    }
}
