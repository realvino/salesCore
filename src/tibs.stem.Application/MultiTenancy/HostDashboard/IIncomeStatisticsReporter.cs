using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tibs.stem.MultiTenancy.HostDashboard.Dto;

namespace tibs.stem.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}