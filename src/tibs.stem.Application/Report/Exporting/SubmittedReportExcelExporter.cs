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
    public class SubmittedReportExcelExporter : EpPlusExcelExporterBase, ISubmittedReportExcelExporter
    {
        public FileDto ExportToFile(List<ReportList> SubmittedReportLists)
        {
            return CreateExcelPackage(
                "SubmittedReport.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("SubmittedReport"));
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
                        L("SubmittedDate"),
                        L("Total"),
                        L("CreationTime")
                        );

                    AddObjects(
                        sheet, 2, SubmittedReportLists,
                        _ => _.QuotationTitleName,
                        _ => _.SubjectName,
                        _ => _.ProposalNumber,
                        _ => _.Salesman,
                        _ => _.EnquiryName,
                        _ => _.EnquiryNumber,
                        _ => _.CompanyName,
                        _ => _.ContactName,
                        _ => _.StatusName,
                        _ => _.SubmittedDate,
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
