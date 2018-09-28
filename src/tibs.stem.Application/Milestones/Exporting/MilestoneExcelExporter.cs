using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.MileStones.Dto;

namespace tibs.stem.Milestones.Exporting
{
    public class MilestoneExcelExporter : EpPlusExcelExporterBase, IMilestoneExcelExporter
    {
        public FileDto ExportToFile(List<MileStoneListDto> MileStoneListDtos)
        {
            return CreateExcelPackage(
                "Milestone.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Milestone"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("MileStoneCode"),
                        L("MileStoneName"));

                    AddObjects(
                        sheet, 2, MileStoneListDtos,
                        _ => _.Code,
                        _ => _.Name);


                    for (var i = 1; i <= 4; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

    }
}
