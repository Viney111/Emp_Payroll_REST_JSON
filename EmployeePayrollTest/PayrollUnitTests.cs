using Microsoft.VisualStudio.TestTools.UnitTesting;
using Employee_Payroll_REST;
using RestSharp;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace EmployeePayrollTest
{
    [TestClass]
    public class PayrollUnitTests
    {
        RestClient restClient = new RestClient("http://localhost:3000");

        private IRestResponse GetEmployeeList()
        {
            //Arrange
            RestRequest request = new RestRequest("/posts",Method.GET);
            //Act
            IRestResponse response = restClient.Execute(request);
            return response;
        }
        [TestMethod]
        public void GivenCorrectLocalHost_InRestRequest_ShouldReturnListofEmployeesAPI()
        {
            IRestResponse response = GetEmployeeList();
            Assert.AreEqual(response.StatusCode,HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(7,dataResponse.Count);
            foreach(Employee emp in dataResponse)
            {
                Console.WriteLine(emp.ToString());
            }
        }
    }
}