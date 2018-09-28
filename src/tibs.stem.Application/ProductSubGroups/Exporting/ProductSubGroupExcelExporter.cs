using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.ProductSubGroups.Dto;

namespace tibs.stem.ProductSubGroups.Exporting
{
    public class ProductSubGroupExcelExporter : EpPlusExcelExporterBase, IProductSubGroupExcelExporter
    {
        public FileDto ExportToFile(List<ProductSubGpListDto> ProductSubGpListDtos)
        {
            return CreateExcelPackage(
                "ProductSubGroup.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ProductSubGroup"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ProductSubGroupCode"),
                        L("ProductSubGroupName"),
                        L("ProductGroupName"));

                    AddObjects(
                        sheet, 2, ProductSubGpListDtos,
                        _ => _.ProductSubGroupCode,
                        _ => _.ProductSubGroupName,
                        _ => _.ProductGroupName);


                    for (var i = 1; i <= 3; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

    }



}

