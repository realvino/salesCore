using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Report.Dto;

namespace tibs.stem.Report.Exporting
{
    public class LostReportExcelExporter : EpPlusExcelExporterBase, ILostReportExcelExporter
    {
        public FileDto ExportToFile(List<ReportList> LostReportLists)
        {
            return CreateExcelPackage(
                "LostReport.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("LostReport"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Title"),
                        L("Subject"),
                        L("RefNo"),
                        L("Salesperson"),
                        L("EnquiryTitle"),
                        L("EnquiryNo"),
                        L("Company"),
                        L("Contact"),
                        L("Status"),
                        L("LostDate"),
                        L("Total"),
                        L("CreationTime")
                        );

                    AddObjects(
                        sheet, 2, LostReportLists,
                        _ => _.QuotationTitleName,
                        _ => _.SubjectName,
                        _ => _.ProposalNumber,
                        _ => _.Salesman,
                        _ => _.EnquiryName,
                        _ => _.EnquiryNumber,
                        _ => _.CompanyName,
                        _ => _.ContactName,
                        _ => _.StatusName,
                        _ => _.LostDate,
                        _ => _.Total,
                        _ => _.CreationTime
                        );

                    for (var i = 1; i <= 12; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
