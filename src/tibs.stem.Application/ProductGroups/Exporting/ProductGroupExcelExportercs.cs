using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.ProductGroups.Dto;

namespace tibs.stem.ProductGroups.Exporting
{
    public class ProductGroupExcelExportercs : EpPlusExcelExporterBase, IProductGroupExcelExporter
    {
        public FileDto ExportToFile(List<ProductGroupListDto> ProductGroupListDtos)
        {
            return CreateExcelPackage(
                "ProductGroup.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ProductGroup"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ProductGroupCode"),
                        L("ProducGrouptName"));

                    AddObjects(
                        sheet, 2, ProductGroupListDtos,
                        _ => _.ProductGroupCode,
                        _ => _.ProductGroupName);


                    for (var i = 1; i <= 2; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

    }


}

