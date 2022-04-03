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
            RestRequest restRequest = new RestRequest("/posts", Method.POST);
            JObject jobjBody = new JObject();
            jobjBody.Add("name", "bhushan");
            jobjBody.Add("salary", "92000");
            restRequest.AddParameter("application/json", jobjBody, ParameterType.RequestBody);
            //Act
            IRestResponse restResponse = restClient.Execute(restRequest);
            //Assert
            Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.Created);
            Employee employee = JsonConvert.DeserializeObject<Employee>(restResponse.Content);
            Assert.AreEqual("bhushan", employee.name);
            Assert.AreEqual("92000", employee.salary);
            Console.WriteLine(restResponse.Content);
        }
        [TestMethod]
        public void GivenCorrectLocalHostAndPOSTMethod_InRestRequestAddingListofEmployee_ShouldReturnListofEmployeesAddedRecently()
        {
            List<Employee> employeesListRetrievedFromJsonServer = new List<Employee>();
            List<Employee> employeeListToBeadded = new List<Employee>();
            employeeListToBeadded.Add(new Employee { name = "Pranav", salary = "85000" });
            employeeListToBeadded.Add(new Employee { name = "TaranDeep", salary = "95000" });
            employeeListToBeadded.Add(new Employee { name = "Vipul", salary = "75000" });
            employeeListToBeadded.Add(new Employee { name = "Vipul", salary = "65000" });
            employeeListToBeadded.Add(new Employee { name = "Deepak", salary = "95000" });
            foreach (Employee emp in employeeListToBeadded)
            {
                //Arrange
                RestRequest restRequest = new RestRequest("/posts", Method.POST);
                //Act
                JObject jobjBody = new JObject();
                jobjBody.Add("name", emp.name);
                jobjBody.Add("salary", emp.salary);
                restRequest.AddParameter("application/json", jobjBody, ParameterType.RequestBody);
                IRestResponse restResponse = restClient.Execute(restRequest);
                //Assert
                Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.Created);
                Employee employee = JsonConvert.DeserializeObject<Employee>(restResponse.Content);
                employeesListRetrievedFromJsonServer.Add(employee);
            }
            Assert.AreEqual(employeeListToBeadded.Count, employeesListRetrievedFromJsonServer.Count);
        }
        [TestMethod]
        public void GivenCorrectLocalHostAndEmployeeID_InRestRequestUpdatingListofEmployee_ShouldReturnupdatedListofEmployeesAddedRecently()
        {
            RestRequest restRequest = new RestRequest("/posts/1", Method.PUT);
            JObject jsonBody = new JObject();
            jsonBody.Add("name", "viney");
            jsonBody.Add("salary", "78000");
            restRequest.AddOrUpdateParameter("application/json", jsonBody, ParameterType.RequestBody);
            IRestResponse restResponse = restClient.Execute(restRequest);
            Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.OK);
            Employee employee = JsonConvert.DeserializeObject<Employee>(restResponse.Content);
            Assert.AreEqual("78000", employee.salary);
        }
    }
}