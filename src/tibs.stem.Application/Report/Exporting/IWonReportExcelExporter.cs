﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Report.Dto;

namespace tibs.stem.Report.Exporting
{
    public interface IWonReportExcelExporter
    {
        FileDto ExportToFile(List<ReportList> WonReportLists);
    }
}
