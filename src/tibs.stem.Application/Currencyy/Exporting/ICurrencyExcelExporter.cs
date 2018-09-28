using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Currencyy.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Currencyy.Exporting
{
    public interface ICurrencyExcelExporter
    {
        FileDto ExportToFile(List<CurrencyListDto> CurrencyListDtos);
    }
}
