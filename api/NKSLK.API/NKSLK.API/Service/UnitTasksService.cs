using NKSLK.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Service
{
    public class UnitTasksService
    {
        public bool ValidateDataDelelte(int id)
        {
            if (id > 0)
            {
                return true;
            }
            return false;
        }
        public bool CheckExistsUnitTask(int id)
        {
            var unitRepo = new UnitTasksRepository();
            return unitRepo.CheckExistsUnitTasks(id);
        }
    }
}
