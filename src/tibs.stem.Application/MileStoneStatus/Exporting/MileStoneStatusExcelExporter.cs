using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.MileStoneStatuss.Dto;

namespace tibs.stem.MileStoneStatuss.Exporting
{
    public class MileStoneStatusExcelExporter : EpPlusExcelExporterBase, IMileStoneStatusExcelExporter
    {
        public FileDto ExportToFile(List<MileStoneStatusListDto> MileStoneStatusListDtos)
        {
            return CreateExcelPackage(
                "<MileStoneStatus.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("MileStoneStatus"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("StatusCode"),
                        L("StatusName"));

                    AddObjects(
                        sheet, 2, MileStoneStatusListDtos,
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
