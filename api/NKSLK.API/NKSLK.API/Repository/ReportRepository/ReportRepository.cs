using Dapper;
using Microsoft.Extensions.Configuration;
using NKSLK.API.Models.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Repository.ReportRepository
{
    public class ReportRepository
    {
        //private readonly IConfiguration _configuration;
        //public ReportRepository(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        public IEnumerable<ReportsWorkingDay> GetWorkingDays(ReportsWorkingDay filter,string connectionString)
        {

            string str = connectionString;
            using (var connection = new SqlConnection(str))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("@macongnhan", filter.EmployeeId);
                dataParam.Add("@month", filter.Month);
                var obj = connection.Query<ReportsWorkingDay>(sql: "Proc_GetWorkingDays", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
    }
}
