using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;
using tibs.stem.NewCompanyContacts.Dto;

namespace tibs.stem.CompanyContacts.Exporting
{
    public interface ICompanyExcelExporter
    {
        FileDto ExportToFile(List<NewCompanyListDto> NewCompanyListDtos);

    }
}
