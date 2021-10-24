using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NKSLK.API.Models;

namespace NKSLK.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongBanController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PhongBanController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ma_phongban, ten_phongban from PHONGBAN
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
        [HttpPost]
        public JsonResult Post(PhongBan pb)
        {
            string query = @"
                           insert into dbo.PHONGBAN
                           values (@ten_phongban)
                            ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnecttionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ten_phongban", pb.ten_phongban);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(PhongBan pb)
        {
            string query = @"
                           update dbo.PHONGBAN
                           set ten_phongban= @ten_phongban
                            where ma_phongban=@ma_phongban
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnecttionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ma_phongban", pb.ma_phongban);
                    myCommand.Parameters.AddWithValue("@ten_phongban", pb.ten_phongban);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.PHONGBAN
                            where ma_phongban=@ma_phongban
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NKSLKConnecttionString");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ma_phongban", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

    }
}
