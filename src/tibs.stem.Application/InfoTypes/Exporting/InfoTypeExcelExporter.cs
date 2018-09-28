using System;
using System.Collections.Generic;
using System.Text;
using tibs.NewInfoTypes.Dto;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;

namespace tibs.stem.InfoTypes.Exporting
{
    public class InfoTypeExcelExporter : EpPlusExcelExporterBase, IInfoTypeExcelExporter
    {
        public FileDto ExportToFile(List<NewInfoTypeListDto> NewInfoTypeListDtos)
        {
            return CreateExcelPackage(
                "InfoType.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("InfoType"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                         L("TypeName"),
                        L("CreationTime"));

                    AddObjects(
                        sheet, 2, NewInfoTypeListDtos,
                        _ => _.ContactName,
                        _ => _.Info);


                    for (var i = 1; i <= 2; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
