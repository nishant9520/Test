using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Custom_Exception;
using Test.DateBase;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("~/Views/Index.cshtml");
        }

        TestEntities testEntities = new TestEntities();

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Test.Custom_Exception.NotFoundException">Data not found.</exception>
        public ActionResult Get_Country()
        {
            try
            {

                var result = (from x in testEntities.Countries
                              select new
                              {
                                  x.Country_id,
                                  x.Name
                              }).ToList();

                // Check if data is not found, and throw a NotFoundException.
                if (result.Count == 0)
                {
                    throw new NotFoundException("Data not found.");
                }

                // Return the contract types as JSON using JsonRequestBehavior.AllowGet to allow GET requests.
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (NotFoundException notFoundEx)
            {
                // Handle the "Not Found" exception.
                return Json(new { error = "Data not found: " + notFoundEx.Message });
            }
            catch (System.InvalidCastException icEx)
            {
                // Handle InvalidCastException if a type conversion fails.
                return Json(new { error = "Type conversion failed" });
            }
            catch (Exception ex)
            {
                // Handle any general exceptions here.
                return Json(new { error = "An error occurred while fetching contract types." });
            }

            // Add more catch blocks for specific exceptions as needed.
        }


        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Test.Custom_Exception.NotFoundException">Data not found.</exception>
        public ActionResult Get_State(int Country_id)
        {
            try
            {

                var result = (from x in testEntities.States
                              where x.Country_id == Country_id
                              select new
                              {
                                  x.State_id,
                                  x.Name
                              }).ToList();

                // Check if data is not found, and throw a NotFoundException.
                if (result.Count == 0)
                {
                    throw new NotFoundException("Data not found.");
                }

                // Return the contract types as JSON using JsonRequestBehavior.AllowGet to allow GET requests.
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (NotFoundException notFoundEx)
            {
                // Handle the "Not Found" exception.
                return Json(new { error = "Data not found: " + notFoundEx.Message });
            }
            catch (System.InvalidCastException icEx)
            {
                // Handle InvalidCastException if a type conversion fails.
                return Json(new { error = "Type conversion failed" });
            }
            catch (Exception ex)
            {
                // Handle any general exceptions here.
                return Json(new { error = "An error occurred while fetching contract types." });
            }

            // Add more catch blocks for specific exceptions as needed.
        }


        /// <summary>
        /// Saves the employee detail.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Save_Employee_Detail()
        {
            try
            {
                int Emp_id = Convert.ToInt32(Request.Form["Emp_id"]);
                string Emp_Name = Request.Form["Emp_Name"];
                string Age = Request.Form["Age"];
                string gender = Request.Form["gender"];
                string Email = Request.Form["Email"];
                string Mobile = Request.Form["Mobile"];
                string Country = Request.Form["ddl_Country"];
                string State = Request.Form["ddl_State"];

                using (var testEntities = new TestEntities()) // Replace YourDbContext with your actual DbContext.
                {
                    if (Emp_id == 0)
                    {
                        // Insert a new employee
                        Employee employee = new Employee
                        {
                            Name = Emp_Name,
                            Age = Convert.ToInt32(Age),
                            Gender = gender,
                            Email = Email,
                            Phone_Number = Mobile,
                            Country_id = Convert.ToInt32(Country),
                            State_id = Convert.ToInt32(State),
                            DBdateTime = DateTime.Now
                        };

                        testEntities.Employees.Add(employee);
                        testEntities.SaveChanges();

                        // Return the newly inserted employee details
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                    else if (Emp_id > 0)
                    {
                        // Update an existing employee
                        Employee employee = testEntities.Employees.Find(Emp_id);
                        if (employee != null)
                        {
                            employee.Name = Emp_Name;
                            employee.Age = Convert.ToInt32(Age);
                            employee.Gender = gender;
                            employee.Email = Email;
                            employee.Phone_Number = Mobile;
                            employee.Country_id = Convert.ToInt32(Country);
                            employee.State_id = Convert.ToInt32(State);
                            employee.DBdateTime = DateTime.Now;

                            testEntities.SaveChanges();

                            // Return the updated employee details
                            return Json("2", JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                // Handle the case when no employee is found or there's an error
                return Json(new { error = "Employee not found or an error occurred while saving." });


            }
            catch (FormatException iex)
            {
                return Json(new { error = iex.Message });

            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });

            }
        }




        /// <summary>
        /// Gets the employee details.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetEmployeeDetails()
        {
            try
            {
                using (var testEntities = new TestEntities())
                {
                    var result = testEntities.Get_Employee().ToList();

                    if (result.Count == 0)
                    {
                        return Json(new { error = "Data not found" });
                    }

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = "An error occurred while fetching employee details: " + ex.Message });
            }
        }



        /// <summary>
        /// Gets the employee details.
        /// </summary>
        /// <param name="Emp_id">The emp identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete_Employee(string Emp_id)
        {
            try
            {
                if (int.TryParse(Emp_id, out int empId))
                {
                    var recordsToDelete = testEntities.Employees.Where(a => a.Emp_id == empId).ToList();

                    foreach (var recordToDelete in recordsToDelete)
                    {
                        testEntities.Employees.Remove(recordToDelete);
                    }

                    testEntities.SaveChanges(); // Save changes to the database

                    return Json(new { mess = "1" });
                }
                else
                {
                    return Json(new { error = "Invalid Emp_id format" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }


    }
}