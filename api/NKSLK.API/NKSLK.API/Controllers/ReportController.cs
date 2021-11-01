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
            string query =@"select n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.ngaybatdau, n.thoigian_batdau, n.thoigian_ketthuc
	                        from NKSLK n, NKSLK_CONGNHAN nc, CONGNHAN cn
	                        where n.ma_nkslk = nc.ma_nkslk
	                             and nc.ma_congnhan = cn.ma_congnhan
		                         and nc.ma_nkslk in (select nc.ma_nkslk
							                         from NKSLK_CONGNHAN nc
							                         group by  nc.ma_nkslk
							                         having COUNT(ma_congnhan) = 1)
	                        group by n.ma_nkslk,cn.ma_congnhan,cn.hoten, n.ngaybatdau, n.thoigian_batdau, n.thoigian_ketthuc
	                        order by n.ma_nkslk
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnecttionString");
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
	                        order by n.ma_nkslk
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnecttionString");
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
