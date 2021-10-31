using Dapper;
using Microsoft.Extensions.Configuration;
using NKSLK.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Repository
{
    /// <summary>
    /// Tương tác trực tiếp với db cho Công việc
    /// vmquang
    /// </summary>
    public class JobRepository
    {
        private readonly string connectionString = @"Data Source=VMQUANG1\SQLEXPRESS;Initial Catalog=Nhom6_NKSLK;Integrated Security=True";
        /// <summary>
        /// Hết lấy toàn bộ Công việc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CONGVIEC> GetAllJob()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                var obj = connection.Query<CONGVIEC>(sql: "Proc_GetAllJob", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        /// <summary>
        /// Lấy công việc theo id
        /// </summary>
        /// <param name="id">id cần lấy</param>
        /// <returns>1 công việc duy nhất theo id , ko có trả về null</returns>
        public CONGVIEC GetJobById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("Id", id);
                var obj = connection.Query<CONGVIEC>(sql: "Proc_GetJobById", param: dataParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        /// <summary>
        /// Xóa Công việc theo 
        /// </summary>
        /// <param name="id">id cần xóa</param>
        /// <returns>số dòng xóa được</returns>
        public int DeleteJobById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("Id", id);
                var row = connection.Execute(sql: "Proc_DeleteJobById", param: dataParam, commandType: CommandType.StoredProcedure);
                return row;
            }
        }
        /// <summary>
        /// lấy danh sách công việc theo giá
        /// </summary>
        /// <param name="id">id cần xóa</param>
        /// <returns>số dòng xóa được</returns>
        public IEnumerable<CONGVIEC> GetJobBySalary(decimal salary)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("salary", salary);
                var obj = connection.Query<CONGVIEC>(sql: "Proc_GetJobBySalary", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        /// <summary>
        /// lấy danh sách công việc theo khoảng giá
        /// </summary>
        /// <param name="id">id cần xóa</param>
        /// <returns>số dòng xóa được</returns>
        public IEnumerable<CONGVIEC> GetJobBySalary(decimal salaryMin, decimal salaryMax)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("salaryMin", salaryMin);
                dataParam.Add("salaryMax", salaryMax);
                var obj = connection.Query<CONGVIEC>(sql: "Proc_GetJobByMutilSalary", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
    }
}
