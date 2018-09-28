using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Productss.Dto;

namespace tibs.stem.Productss.Exporting
{
    public class ProductListExcelExporter : EpPlusExcelExporterBase, IProductListExcelExporter
    {
        public FileDto ExportToFile(List<ProductListDto> ProductListDtos)
        {
            return CreateExcelPackage(
                "Product.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Product"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ProductName"),
                        L("ProductCode"),
                        L("ProductSubGroupName"),
                        L("Discontinue"));

                    AddObjects(
                        sheet, 2, ProductListDtos,
                        _ => _.ProductName,
                        _ => _.ProductCode,
                        _ => _.ProductSubGroupName,
                        _ => _.Discontinued);


                    for (var i = 1; i <= 4; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

    }


}

