using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Currencyy.Dto;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;

namespace tibs.stem.Currencyy.Exporting
{
    public class CurrencyExcelExporter : EpPlusExcelExporterBase, ICurrencyExcelExporter
    {
        public FileDto ExportToFile(List<CurrencyListDto> CurrencyListDtos)
        {
            return CreateExcelPackage(
                "Currency.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Currency"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("CurrencyCode"),
                        L("CurrencyName"));

                    AddObjects(
                        sheet, 2, CurrencyListDtos,
                        _ => _.Code,
                        _ => _.Name);


                    for (var i = 1; i <= 2; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
