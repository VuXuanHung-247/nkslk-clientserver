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
    public class TasksRepository
    {
        private readonly string connectionString = @"Data Source=VMQUANG1\SQLEXPRESS;Initial Catalog=Nhom6_NKSLK;Integrated Security=True";
        public IEnumerable<Tasks> GetAllTasks()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                var obj = connection.Query<Tasks>(sql: "Proc_GetAllTasks", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public int InsertTasks(Tasks param)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("name", param.tasks_name);
                dataParam.Add("litmit_unit", param.litmit_unit);
                dataParam.Add("unittasks_id", param.unittasks_id);
                dataParam.Add("limit_rate", param.limit_rate);
                dataParam.Add("litmit_work", param.litmit_work);
                var rowEffect = connection.Execute(sql: "Proc_InsertTasks", param: dataParam, commandType: CommandType.StoredProcedure);
                return rowEffect;
            }
        }
        public int UpdateTask(Tasks param)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("id", param.tasks_id);
                dataParam.Add("name", param.tasks_name);
                dataParam.Add("litmit_unit", param.litmit_unit);
                dataParam.Add("unittasks_id", param.unittasks_id);
                dataParam.Add("limit_rate", param.limit_rate);
                dataParam.Add("litmit_work", param.litmit_work);
                var rowEffect = connection.Execute(sql: "Proc_UpdateTasks", param: dataParam, commandType: CommandType.StoredProcedure);
                return rowEffect;
            }
        }
        public IEnumerable<Tasks> GetMinJobByNKSLK()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                var obj = connection.Query<Tasks>(sql: "[Proc_GetMinJobByNKSLK]", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public IEnumerable<Tasks> GetMaxJobByNKSLK()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                var obj = connection.Query<Tasks>(sql: "[Proc_GetMaxJobByNKSLK]", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public bool CheckExistsTask(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("id",id);
                var obj = connection.Query<Tasks>(sql: "Proc_GetTaskForDelete", param: dataParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (obj == null)
                {
                    return false;
                }
                return true;
            }
        }
        public int Delete(int id )
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                dataParam.Add("id", id);
                var rowEffect = connection.Execute(sql: "Proc_DeleteTasks", param: dataParam, commandType: CommandType.StoredProcedure);
                return rowEffect;
            }
        }
        public IEnumerable<Tasks> GetTasksMinPrice()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                var obj = connection.Query<Tasks>(sql: "[Proc_GetTasksMinPrice]", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public IEnumerable<Tasks> GetTasksMaxPrice()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                var obj = connection.Query<Tasks>(sql: "[Proc_GetTasksMaxPrice]", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public IEnumerable<Tasks> GetTasksLessAVG()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                var obj = connection.Query<Tasks>(sql: "[Proc_GetTasksLessAVG]", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public IEnumerable<Tasks> GetTasksBiggerAVG()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataParam = new DynamicParameters();
                var obj = connection.Query<Tasks>(sql: "[Proc_GetTasksBiggerAVG]", param: dataParam, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
    }
}
