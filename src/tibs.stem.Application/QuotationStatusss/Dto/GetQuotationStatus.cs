using System;
using System.Collections.Generic;
using System.Text;

namespace tibs.stem.QuotationStatusss.Dto
{
    public class GetQuotationStatus
    {
        public CreateQuotationStatusInput quotationStatus { get; set; }
        public MilestoneData Milestone { get; set; }

    }
    public class MilestoneData
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
