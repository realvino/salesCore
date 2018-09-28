using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Enquiryss.Dto;

namespace tibs.stem.Enquiryss.Exporting
{
    public interface IGlobalReportExcelExporter
    {
        FileDto ExportToFile(List<EnquiryQuotationKanbanList> EnquiryLists);
    }
}
