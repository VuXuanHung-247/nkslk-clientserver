using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using NKSLK.API.Models;
using Microsoft.Extensions.Configuration;

namespace NKSLK.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select ma_sanpham, tensanpham, sodangky, hansudung, quycach, ngaydangky from SANPHAM";

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

        [HttpGet("{id}")]
        public JsonResult Get([FromRoute] int id)
        {
            string query = @"select ma_sanpham, tensanpham from SANPHAM where ma_sanpham = @ma_sanpham";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnectionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ma_sanpham", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Product pd)
        {
            string query = @"insert into dbo.SANPHAM
                           values (@tensanpham, @sodangky, @hansudung, @quycach, @ngaydangky)";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnectionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@tensanpham", pd.tensanpham);
                    myCommand.Parameters.AddWithValue("@sodangky", pd.sodangky);
                    myCommand.Parameters.AddWithValue("@hansudung", pd.hansudung);
                    myCommand.Parameters.AddWithValue("@quycach", pd.quycach);
                    myCommand.Parameters.AddWithValue("@ngaydangky", pd.ngaydangky);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added product successfully!");
        }

        [HttpPut]
        public JsonResult Put(Product pd)
        {
            string query = @"update dbo.SANPHAM
                           set tensanpham = @tensanpham, sodangky = @sodangky, hansudung = @hansudung, quycach = @quycach, ngaydangky = @ngaydangky
                            where ma_sanpham = @ma_sanpham";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnectionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ma_sanpham", pd.ma_sanpham);
                    myCommand.Parameters.AddWithValue("@tensanpham", pd.tensanpham);
                    myCommand.Parameters.AddWithValue("@sodangky", pd.sodangky);
                    myCommand.Parameters.AddWithValue("@hansudung", pd.hansudung);
                    myCommand.Parameters.AddWithValue("@quycach", pd.quycach);
                    myCommand.Parameters.AddWithValue("@ngaydangky", pd.ngaydangky);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated product successfully!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.SANPHAM
                            where ma_sanpham= @ma_sanpham";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnectionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ma_sanpham", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted product successfully!");
        }
    }
}