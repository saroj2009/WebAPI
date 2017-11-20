using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyApi.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MyApi.Controllers
{
    public class EmployeesController : ApiController
    {
        private sarojwebappdbEntities db = new sarojwebappdbEntities();

       // Product[] products = new Product[]
       //{
       //     new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
       //     new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
       //     new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
       //};

       // public IEnumerable<Product> GetAllProducts()
       // {
       //     return products;
       // }

       // public IHttpActionResult GetProduct(int id)
       // {
       //     var product = products.FirstOrDefault((p) => p.Id == id);
       //     if (product == null)
       //     {
       //         return NotFound();
       //     }
       //     return Ok(product);
       // }

        // GET: api/Employees  
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }
        // PUT: api/Employees/5  

        public HttpResponseMessage PutEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }


            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST: api/Employees  

        public HttpResponseMessage PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, employee);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = employee.EmployeeID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

        }

        // DELETE: api/Employees/5  

        public HttpResponseMessage DeleteEmployee(Employee employee)
        {
            Employee remove_employee = db.Employees.Find(employee.EmployeeID);
            if (remove_employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Employees.Remove(remove_employee);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}
