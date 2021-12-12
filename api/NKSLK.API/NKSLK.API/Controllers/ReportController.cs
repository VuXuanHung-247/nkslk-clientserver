using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NKSLK.API.Common;
using NKSLK.API.Models.Reports;
using NKSLK.API.Repository.ReportRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ReportController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Nhật ký sản lượng khoán làm riêng
        /// </summary>
        /// <returns></returns>
        [Route("ReportBySelf")]
        [HttpGet]
        public JsonResult ReportBySelf()
        {
            string query = ReportSQL.queryReportBySelf;

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnectionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        /// <summary>
        /// Danh sách Nhật ký sản lượng khoán làm chung
        /// </summary>
        /// <returns></returns>
        [Route("ReportByTogether")]
        [HttpGet]
        public JsonResult ReportByTogether()
        {
            DataTable table = new DataTable();
            string query = ReportSQL.queryReportByTogether;
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnectionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        /// <summary>
        /// Danh sách Nhật ký sản lượng khoán làm muộn
        /// </summary>
        /// <returns></returns>
        [Route("ReportByLate")]
        [HttpGet]
        public JsonResult ReportByLate()
        {
            DataTable table = new DataTable();
            string query = ReportSQL.queryReportByLate;
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnectionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        /// <summary>
        /// Số ngày làm việc của công nhân
        /// </summary>
        /// <returns></returns>
        //[Route("GetWorkingDays")]
        //[HttpGet]
        //public IActionResult GetWorkingDays(int employeeId, int month)
        //{
        //    string sqlDataSource = _configuration.GetConnectionString("NKSLKConnectionString");
        //    try
        //    {
        //        var report = new ReportRepository();
        //        var filter = new ReportsWorkingDay();
        //        filter.EmployeeId = employeeId;
        //        filter.Month = month;
        //        var data = report.GetWorkingDays(filter,sqlDataSource);
        //        if (data.Count() > 0)
        //        {
        //            return Ok(data);
        //        }
        //        else
        //        {
        //            return NoContent();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return StatusCode(500);
        //    }
        //}
        [Route("GetWorkingDays")]
        [HttpGet]
        public JsonResult GetWorkingDays(int? employeeId, int? month)
        {
            string query = ReportSQL.queryGetWorkingDays;

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnectionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@macongnhan", employeeId ?? (object)DBNull.Value);
                    myCommand.Parameters.AddWithValue("@month", month ?? (object)DBNull.Value);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [Route("GetExpiredProducts")]
        [HttpGet]
        public JsonResult GetExpiredProducts()
        {
            string query = ReportSQL.queryGetExpiredProducts;

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnectionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [Route("UpdateReportSelf")]
        [HttpPut]
        public JsonResult UpdateReportSelf(ReportEmployee re)
        {
            string query = @"
                            update NKSLK_CONGNHAN
                            set thoigian_batdau= @thoigian_batdau,
		                        thoigian_ketthuc = @thoigian_ketthuc
                            where ma_congnhan = @ma_congnhan and ma_nkslk = @ma_nkslk  

	                        update NKSLK
                            set ngaybatdau= @ngaybatdau
                            where ma_nkslk = @ma_nkslk 

	                        update CONGNHAN
                            set hoten= @hoten
                            where ma_congnhan = @ma_congnhan
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnectionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ma_nkslk", re.ma_nkslk);
                    myCommand.Parameters.AddWithValue("@ma_congnhan", re.ma_congnhan);
                    myCommand.Parameters.AddWithValue("@hoten", re.hoten);
                    myCommand.Parameters.AddWithValue("@ngaybatdau", re.ngaybatdau);
                    myCommand.Parameters.AddWithValue("@thoigian_batdau", re.thoigian_batdau);
                    myCommand.Parameters.AddWithValue("@thoigian_ketthuc", re.thoigian_ketthuc);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Cập nhật thành công!!");
        }
    }
}
