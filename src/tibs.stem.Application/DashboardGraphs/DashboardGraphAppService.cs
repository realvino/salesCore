using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DashboardGraphs.Dto;
using tibs.stem.EntityFrameworkCore;
using tibs.stem.MultiTenancy;
//using tibs.stem.EntityFrameworkCore;
//using tibs.stem.EntityFrameworkCore;
using tibs.stem.Quotations;

namespace tibs.stem.DashboardGraphs
{
    public class DashboardGraphAppService : stemAppServiceBase, IDashboardGraphAppService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpSession _session;
        private readonly IRepository<Quotation> _QuotationRepository;
        private readonly stemDbContext _stemDbContext;

        public DashboardGraphAppService(IUnitOfWorkManager unitOfWorkManager, IAbpSession session, IRepository<Quotation> QuotationRepository,
            stemDbContext stemDbContext
            )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _session = session;
            _QuotationRepository = QuotationRepository;
            _stemDbContext = stemDbContext;
        }
        public async Task<Array> WonTargetDevelopment(NullableIdDto input)
        {
            List<QuotationYearList> results = new List<QuotationYearList>();
            ConnectionAppService db = new ConnectionAppService(); ;
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("sp_WonTargetGraph", conn);
                sqlComm.Parameters.AddWithValue("@TenantId", AbpSession.TenantId);
                sqlComm.Parameters.AddWithValue("@UserId", input.Id);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (SqlDataReader reader = sqlComm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        QuotationYearList newItem = new QuotationYearList();
                        newItem.Year = Convert.ToInt32(reader["Wyear"]);
                        newItem.Month = Convert.ToInt32(reader["Wmonth"]);
                        newItem.MonthString = Convert.ToString(reader["WDate"]);
                        newItem.Value = Convert.ToDouble(reader["Wvalue"]);
                        results.Add(newItem);
                    }
                }
                conn.Close();
            }

            return results.ToArray();
        }

        public async Task<Array> GetActivepotential(NullableIdDto input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            List<SpResultActivepotential> Activepotentiallist = new List<SpResultActivepotential>();
            ConnectionAppService db = new ConnectionAppService();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("sp_activepotential", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@TenantId", TenantId);
                sqlComm.Parameters.AddWithValue("@UserId", input.Id);
                conn.Open();
                using (var reader = sqlComm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SpResultActivepotential actpotential = new SpResultActivepotential();
                        actpotential.createmonth = Convert.ToString(reader["create_month"]);
                        actpotential.totalcreatedvalue = Convert.ToDecimal(reader["total_created_value"]);
                        actpotential.totalwonvalue = Convert.ToDecimal(reader["total_won_value"]);
                        actpotential.optimalactive = Convert.ToDecimal(reader["optimal_active"]);
                        Activepotentiallist.Add(actpotential);
                    }
                }
                conn.Close();
            }
            return (Activepotentiallist.ToArray());
        }

    }
}
