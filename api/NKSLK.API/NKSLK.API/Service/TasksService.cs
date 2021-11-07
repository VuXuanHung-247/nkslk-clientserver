using NKSLK.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Service
{
    public class TasksService
    {
        public bool CheckExistsTask(int id)
        {
            var taskRepo = new TasksRepository();
            return taskRepo.CheckExistsTask(id);
        }
    }
}
