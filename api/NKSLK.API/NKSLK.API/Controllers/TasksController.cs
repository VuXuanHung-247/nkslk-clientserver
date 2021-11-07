using Microsoft.AspNetCore.Mvc;
using NKSLK.API.Models;
using NKSLK.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NKSLK.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            try
            {
                var tasksRepo = new TasksRepository();
                var data = tasksRepo.GetAllTasks();
                if (data.Count()>0)
                {
                    return Ok(data);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        // POST api/<TasksController>
        [HttpPost]
        public IActionResult Post([FromBody] Tasks param)
        {
            try
            {
                var tasksRepo = new TasksRepository();
                var rowEffect = tasksRepo.InsertTasks(param);
                if (rowEffect > 0)
                {
                    return Created("Thêm mới thành công",param);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Tasks param)
        {
            try
            {
                var tasksRepo = new TasksRepository();
                var rowEffect = tasksRepo.UpdateTask(param);
                if (rowEffect > 0)
                {
                    return Created("Sửa thành công", param);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [Route("GetMaxJobByNKSLK")]
        [HttpGet]
        public IActionResult GetMaxJobByNKSLK()
        {
            try
            {
                var tasksRepo = new TasksRepository();
                var data = tasksRepo.GetMaxJobByNKSLK();
                if (data.Count() > 0)
                {
                    return Ok(data);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
        [Route("GetMinJobByNKSLK")]
        [HttpGet]
        public IActionResult GetMinJobByNKSLK()
        {
            try
            {
                var tasksRepo = new TasksRepository();
                var data = tasksRepo.GetMinJobByNKSLK();
                if (data.Count() > 0)
                {
                    return Ok(data);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}
