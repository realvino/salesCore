using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.NewCustomerTypes.Dto;

namespace tibs.stem.CustomerTypes.Exporting
{
    public class CustomerTypeExcelExporter : EpPlusExcelExporterBase, ICustomerTypeExcelExporter
    {
        public FileDto ExportToFile(List<NewCustomerTypeListDto> CustomerTypeListDtos)
        {
            return CreateExcelPackage(
                "CustomerType.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("CustomerType"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Type"),
                        L("Company"));

                    AddObjects(
                        sheet, 2, CustomerTypeListDtos,
                        _ => _.Title,
                        _ => _.Company);


                    for (var i = 1; i <= 2; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
