using Microsoft.AspNetCore.Mvc;
using NKSLK.API.Models;
using NKSLK.API.Repository;
using NKSLK.API.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NKSLK.API.Controllers
{
    /// <summary>
    /// API Đơn vị khoán
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UnitTasksController : ControllerBase
    {
        // GET: api/<UnitTasksController>
        [Route("GetAllUnitTasks")]
        [HttpGet]
        public IActionResult GetAllUnitTasks()
        {
            try
            {
                var unitTasksRepository = new UnitTasksRepository();
                var data = unitTasksRepository.GetAllUnitTasks();
                if (data.Count() > 0)
                {
                    return Ok(data);
                }
                else
                {
                    return NoContent();
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Route("Insert")]
        [HttpPost]
        public IActionResult InsertUnitTasks([FromBody] UnitTasks param)
        {
            try
            {
                var unitTasksRepository = new UnitTasksRepository();
                var rowEffect = unitTasksRepository.InsertUnitTasks(param.unittasks_name);
                if (rowEffect > 0)
                {
                    return Created("Oke",rowEffect);
                }
                else
                {
                    return NoContent() ;
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpPut]
        public IActionResult UpdateUnitTasks([FromBody] UnitTasks param)
        {
            try
            {
                var unitTasksRepository = new UnitTasksRepository();
                var rowEffect = unitTasksRepository.UpdateUnitTasks(param.unittasks_id,param.unittasks_name);
                if (rowEffect > 0)
                {
                    return Created("Oke", rowEffect);
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Route("Delete")]
        [HttpPost]
        public IActionResult DeleteUnitTask([FromBody] UnitTasks param)
        {
            try
            {
                var unitTaskService = new UnitTasksService();
                if (unitTaskService.CheckExistsUnitTask(param.unittasks_id))
                {
                    return Ok(param);
                }
                var unitTasksRepository = new UnitTasksRepository();
                var rowEffect = unitTasksRepository.DeleteUnitTasks(param.unittasks_id);
                if (rowEffect > 0)
                {
                    return Created("Oke", rowEffect);
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

    }
}
