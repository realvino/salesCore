using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.NewCompanyContacts.Dto;

namespace tibs.stem.CompanyContacts.Exporting
{
    public class CompanyExcelExporter : EpPlusExcelExporterBase, ICompanyExcelExporter
    {
        public FileDto ExportToFile(List<NewCompanyListDto> NewCompanyListDtos)
        {
            return CreateExcelPackage(
                "Company.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Company"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Type"),
                        L("Currency"),
                        L("Country"));

                    AddObjects(
                        sheet, 2, NewCompanyListDtos,
                        _ => _.CompanyName,
                        _ => _.CustomerTypeName,
                        _ => _.CurrencyCode,
                        _ => _.CountryCode);


                    for (var i = 1; i <= 4; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

    }
}
