using Microsoft.VisualStudio.TestTools.UnitTesting;
using Employee_Payroll_REST;
using RestSharp;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

namespace EmployeePayrollTest
{
    [TestClass]
    public class PayrollUnitTests
    {
        RestClient restClient = new RestClient("http://localhost:3000");

        private IRestResponse GetEmployeeList()
        {
            //Arrange
            RestRequest request = new RestRequest("/posts", Method.GET);
            //Act
            IRestResponse response = restClient.Execute(request);
            return response;
        }
        [TestMethod]
        public void GivenCorrectLocalHostAndGETMethod_InRestRequest_ShouldReturnListofEmployeesAPI()
        {
            IRestResponse response = GetEmployeeList();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(8, dataResponse.Count);
            foreach (Employee emp in dataResponse)
            {
                Console.WriteLine(emp.ToString());
            }
        }
        [TestMethod]
        public void GivenCorrectLocalHostAndPOSTMethod_InRestRequest_ShouldReturnListofEmployeesAddedRecently()
        {
            //Arrange
            RestRequest restRequest = new RestRequest("/posts",Method.POST);
            JObject jobjBody = new JObject();
            jobjBody.Add("name", "bhushan");
            jobjBody.Add("salary", "92000");
            restRequest.AddParameter("application/json",jobjBody,ParameterType.RequestBody);
            //Act
            IRestResponse restResponse = restClient.Execute(restRequest);
            //Assert
            Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.Created);
            Employee employee = JsonConvert.DeserializeObject<Employee>(restResponse.Content);
            Assert.AreEqual("bhushan",employee.name);
            Assert.AreEqual("92000",employee.salary);
            Console.WriteLine(restResponse.Content);
        }
    }
}