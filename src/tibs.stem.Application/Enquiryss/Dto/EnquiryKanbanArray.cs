using System;
using System.Collections.Generic;
using System.Text;

namespace tibs.stem.Enquiryss.Dto
{
    public class EnquiryKanbanArray
    {
        public string MilestoneName { get; set; }
        public EnquiryList[] EnquiryKanban { get; set; }
    }
}
