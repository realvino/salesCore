using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Enquiryss.Dto;

namespace tibs.stem.Enquiryss.Exporting
{
    public class GlobalReportExcelExporter : EpPlusExcelExporterBase, IGlobalReportExcelExporter
    {
        public FileDto ExportToFile(List<EnquiryQuotationKanbanList> EnquiryReportLists)
        {
            return CreateExcelPackage(
                "GlobalReport.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("GlobalReport"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("EnquiryTitle"),
                        L("EnquiryNo"),
                        L("QuotationRefNo"),
                        L("MileStoneStatus"),
                        L("Salesperson"),
                        L("Company"),
                        L("Contact"),
                        L("ClosureDate"),
                        L("Total"),
                        L("Remarks"),
                        L("CreatedBy"),
                        L("CreationTime"));

                    AddObjects(
                        sheet, 2, EnquiryReportLists,
                        _ => _.Title,
                        _ => _.EnquiryNo,
                        _ => _.QRefno,
                        _ => _.MileStoneName,
                        _ => _.Salesperson,
                        _ => _.CompanyName,
                        _ => _.ContactName,
                        _ => _.CloseDate,
                        _ => _.Total,
                        _ => _.Remarks,
                        _ => _.Creator,
                        _ => _.CreationTime);

                    for (var i = 1; i <= 12; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
    
}
