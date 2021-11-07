using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        [Route("ReportBySelf")]
        [HttpGet]
        public JsonResult ReportBySelf()
        {
            string query = @"select n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.ngaybatdau, n.thoigian_batdau, n.thoigian_ketthuc
	                        from NKSLK n, NKSLK_CONGNHAN nc, CONGNHAN cn
	                        where n.ma_nkslk = nc.ma_nkslk
	                             and nc.ma_congnhan = cn.ma_congnhan
		                         and nc.ma_nkslk in (select nc.ma_nkslk
							                         from NKSLK_CONGNHAN nc
							                         group by  nc.ma_nkslk
							                         having COUNT(ma_congnhan) = 1)
	                        group by n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.ngaybatdau, n.thoigian_batdau, n.thoigian_ketthuc
	                        order by n.ngaybatdau desc
                            ";

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
            string query = @"select n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.ngaybatdau, n.thoigian_batdau, n.thoigian_ketthuc
	                        from NKSLK n, NKSLK_CONGNHAN nc, CONGNHAN cn
	                        where n.ma_nkslk = nc.ma_nkslk
	                             and nc.ma_congnhan = cn.ma_congnhan
		                         and nc.ma_nkslk in (select nc.ma_nkslk
							                         from NKSLK_CONGNHAN nc
							                         group by  nc.ma_nkslk
							                         having COUNT(ma_congnhan) > 1)
	                        group by n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.ngaybatdau, n.thoigian_batdau, n.thoigian_ketthuc
	                        order by n.ngaybatdau desc
                            ";

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
        /// Danh sách Nhật ký sản lượng khoán làm muộn
        /// </summary>
        /// <returns></returns>
        [Route("ReportByLate")]
        [HttpGet]
        public JsonResult ReportByLate()
        {
            string query = @"select n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.thoigian_batdau, nc.thoigian_batdau, n.thoigian_ketthuc, DATEDIFF(minute, n.thoigian_batdau, nc.thoigian_batdau) as sophutdimuon
	                        from NKSLK n, NKSLK_CONGNHAN nc, CONGNHAN cn
	                        where n.ma_nkslk = nc.ma_nkslk
	                             and nc.ma_congnhan = cn.ma_congnhan
		                         and DATEDIFF(minute, n.thoigian_batdau, nc.thoigian_batdau) > 0
		                         and nc.ma_nkslk in (select nc.ma_nkslk
							                         from NKSLK_CONGNHAN nc
							                         group by  nc.ma_nkslk
							                         having COUNT(ma_congnhan) >= 1)
	                        group by  n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.thoigian_batdau, nc.thoigian_batdau, n.thoigian_ketthuc
	                        order by  n.thoigian_batdau desc
                            ";

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
    }
}
