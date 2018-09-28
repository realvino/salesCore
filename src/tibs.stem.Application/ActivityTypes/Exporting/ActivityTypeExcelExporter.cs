using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.ActivityTypes.Dto;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;

namespace tibs.stem.ActivityTypes.Exporting
{
    public class ActivityTypeExcelExporter : EpPlusExcelExporterBase, IActivityTypeExcelExporter
    {
        public FileDto ExportToFile(List<ActivityTypeListDto> ActivityTypeListDtos)
        {
            return CreateExcelPackage(
                "ActivityType.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ActivityType"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ActivityCode"),
                        L("ActivityName"));

                    AddObjects(
                        sheet, 2, ActivityTypeListDtos,
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
