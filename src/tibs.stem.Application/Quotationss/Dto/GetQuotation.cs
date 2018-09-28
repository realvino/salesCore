using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Quotationss.Dto;

namespace tibs.stem.Quotationss.Dto
{
    public class GetQuotation
    {
        public QuotationList quotations { get; set; }
    }

    public class GetQuotationProduct
    {
        public QuotationProductList quotationProducts { get; set; }
    }

    public class GetQuotationService
    {
        public QuotationServiceList quotationServices { get; set; }
    }
}
