using System;
using System.Collections.Generic;
using System.Text;

namespace tibs.stem.Enquiryss.Dto
{
    public class EnquiryKanbanUpdateInput
    {
        public int EnquiryId { get; set; }
        public string UpdateMilestone { get; set; }
        public string CurrentMilestone { get; set; }
    }
    public class EnquiryQuotationKanbanUpdateInput
    {
        public int QuotationId { get; set; }
        public string UpdateMilestone { get; set; }
        public string CurrentMilestone { get; set; }
    }
}
