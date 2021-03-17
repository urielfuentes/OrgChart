﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrgChart.Models;
using OrgChart.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OrgChart.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            this.employeeRepository = employeeRepository;
        }

        public IActionResult Chart(int empId)
        {
            IEnumerable<Employee> employeesGroup;
            Employee manager;

            if (empId == 0)
            {
                employeesGroup = employeeRepository.GetSubordinates(1);
                manager = employeeRepository.GetEmployeeInfo(1);
            }
            else
            {
                employeesGroup = employeeRepository.GetSubordinates(empId);
                manager = employeeRepository.GetEmployeeInfo(empId);
            }

            var employeesView = new ChartList(employeesGroup, manager);

            return View(employeesView);
        }

        public IActionResult Detail(int empId)
        {
            var employee = employeeRepository.GetEmployeeInfo(empId);

            return View(employee);
        }

        public IActionResult Edit(int empId)
        {
            if (empId > 0)
            {
                var employee = employeeRepository.GetEmployeeInfo(empId);
                return View(employee);
            }
            else
            {
                var employee = new Employee();
                return View(employee);
            }
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            var actualEmployee = employeeRepository.GetEmployeeInfo(employee.EmployeeId);
            employeeRepository.UpdateEmployee(employee);
            return RedirectToAction("Chart");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
