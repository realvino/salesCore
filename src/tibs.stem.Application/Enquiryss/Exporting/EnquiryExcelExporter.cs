using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Enquiryss.Dto;

namespace tibs.stem.Enquiryss.Exporting
{
    public class EnquiryExcelExporter : EpPlusExcelExporterBase, IEnquiryExcelExporter
    {
        public FileDto ExportToFile(List<EnquiryList> EnquiryLists)
        {
            return CreateExcelPackage(
                "Enquiry.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Enquiry"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("EnquiryNo"),
                        L("Title"),
                        L("MileStone"),
                        L("Company"),
                        L("ClosedDate"));

                    AddObjects(
                        sheet, 2, EnquiryLists,
                        _ => _.EnquiryNo,
                        _ => _.Title,
                        _ => _.MileStoneName,
                        _ => _.CompanyName,
                         _ => _.CloseDate);


                    for (var i = 1; i <= 5; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

    }



}

