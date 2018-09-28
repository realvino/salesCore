using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.NewCompanyContacts.Dto;

namespace tibs.stem.CompanyContacts.Exporting
{
    public class ContactExcelExporter : EpPlusExcelExporterBase, IContactExcelExporter
    {
        public FileDto ExportToFile(List<NewContactListDto> NewContactListDtos)
        {
            return CreateExcelPackage(
                "Contact.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Contact"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Title"),
                        L(" ContactName"),
                        L("LastName"),
                        L("CompanyName"),
                        L("CustomerTypeName")
                        );

                    AddObjects(
                        sheet, 2, NewContactListDtos,
                        _ => _.Title,
                        _ => _.ContactName,
                        _ => _.LastName,
                        _ => _.CompanyName,
                        _ => _.CustomerTypeName,
                        _ => _.ContactTypeName);


                    for (var i = 1; i <= 5; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }


    }
}
