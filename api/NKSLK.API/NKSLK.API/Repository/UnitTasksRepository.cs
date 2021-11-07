using Dapper;
using NKSLK.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Repository
{
    public class UnitTasksRepository
    {
        private readonly string connectionString = @"Data Source=VMQUANG1\SQLEXPRESS;Initial Catalog=Nhom6_NKSLK;Integrated Security=True";
        /// <summary>
        /// Hết lấy toàn bộ sản lượng khoán
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UnitTasks> GetAllUnitTasks()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                var obj = connection.Query<UnitTasks>(sql: "Proc_GetAllUnitTasks", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        /// <summary>
        /// Xóa 1 đơn vị khoán
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUnitTasks(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("id", id);
                var rowEffot = connection.Execute(sql: "Proc_DeleteUnitTasks", param: dataParam, commandType: CommandType.StoredProcedure);
                return rowEffot;
            }
        }
        public bool CheckExistsUnitTasks(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("id", id);
                var data = connection.Query<object>(sql: "Proc_GetTasksWithUnitId", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                if (data.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public int InsertUnitTasks(string name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("name", name);
                var rowEffect = connection.Execute(sql: "Proc_InsertUnitTasks", param: dataParam, commandType: CommandType.StoredProcedure);
                return rowEffect;
            }
        }
        public int UpdateUnitTasks(int id,string name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("id", id);
                dataParam.Add("name", name);
                var rowEffect = connection.Execute(sql: "[Proc_UpdateUnitTasks]", param: dataParam, commandType: CommandType.StoredProcedure);
                return rowEffect;
            }
        }
    }
}
