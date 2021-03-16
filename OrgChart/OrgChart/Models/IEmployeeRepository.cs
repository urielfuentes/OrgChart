﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrgChart.Models
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> GetSubordinates(int managerId);

        Employee GetEmployeeInfo(int employeeId);
    }
}