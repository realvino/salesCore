using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;
using tibs.stem.NewCompanyContacts.Dto;

namespace tibs.stem.CompanyContacts.Exporting
{
   public interface IContactExcelExporter
    {
        FileDto ExportToFile(List<NewContactListDto> NewContactListDtos);

    }
}
