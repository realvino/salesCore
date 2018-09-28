using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;
using tibs.stem.Enquiryss.Dto;

namespace tibs.stem.Enquiryss.Exporting
{
    public interface IEnquiryExcelExporter
    {
        FileDto ExportToFile(List<EnquiryList> EnquiryLists);
    }
}
