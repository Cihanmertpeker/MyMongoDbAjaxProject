﻿using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyMongoDbAjaxProject.Dal.Entities;
using MyMongoDbAjaxProject.Dal.Settings;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace MyMongoDbAjaxProject.Controllers
{
    public class EmployeeController : Controller
    {
      private readonly IMongoCollection<Employee> _employeeCollection;

        public EmployeeController(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _employeeCollection = database.GetCollection<Employee>(_databaseSettings.EmployeeCollectionName);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> EmployeeList()
        {
            var values = await _employeeCollection.Find(x=>true).ToListAsync();
            var jsonEmployees = JsonConvert.SerializeObject(values);
            return Json(jsonEmployees);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            await _employeeCollection.InsertOneAsync(employee);
            var values = JsonConvert.SerializeObject(employee);
            return Json(values);
        }


        public async Task<IActionResult> GetEmployee(string EmployeeId)
        {
             var values = await _employeeCollection.Find(x => x.EmployeeID == EmployeeId).FirstOrDefaultAsync();
            var jsonValues = JsonConvert.SerializeObject(values);
            return Json(jsonValues);

        }

        public async Task<IActionResult> DeleteEmployee(string id)
        {
            await _employeeCollection.DeleteOneAsync(x=> x.EmployeeID == id);
            return NoContent();
        }

        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            var values = await _employeeCollection.FindOneAndReplaceAsync(x=>x.EmployeeID==employee.EmployeeID, employee);

            return NoContent();
        }

    }
}

