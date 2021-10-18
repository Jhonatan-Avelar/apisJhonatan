using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using apisJhonatan.Models;
using Newtonsoft.Json.Linq;

namespace apisJhonatan.Controllers
{
    // classe responsavel por receber os requests e enviar responses
    // bearer token deve ser obtido no endpoint "/login/"

    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EndpointsController : Controller
    {
        private readonly apisjhonatanContext _contextapisjhonatan;
        public EmployeeMethods EmployeeMethods;

        public EndpointsController(apisjhonatanContext context)
        {
            _contextapisjhonatan = context;
            EmployeeMethods = new EmployeeMethods(_contextapisjhonatan);
        }

        [HttpGet]
        [Route("employees")]
        public IActionResult getEmployee()
        {
            return Json(EmployeeMethods.Get());
        }

        [HttpGet]
        [Route("employees/{id}")]
        public IActionResult getEmployeebyID(int id)
        {
            return Json(EmployeeMethods.GetById(id));
        }

        [HttpPost]
        [Route("employees")]
        public IActionResult inserEmployee([FromBody] dynamic dados)
        {
            JObject objeto = JObject.Parse(dados.ToString());

            var criar = EmployeeMethods.Create(objeto);
            if (criar == 201)
                return StatusCode(201);
            else
                return Json("algo deu errado");
        }

        [HttpPut]
        [Route("employees")]
        public IActionResult updateEmployee([FromBody] dynamic dados)
        {
            JObject objeto = JObject.Parse(dados.ToString());

            var atualizar = EmployeeMethods.Update(objeto);
            if (atualizar == 204)
                return StatusCode(204);
            else
                return Json("algo deu errado");
        }

        [HttpDelete]
        [Route("employees/{id}")]
        public IActionResult deleteEmployee(int id)
        {
            var deletar = EmployeeMethods.Delete(id);
            if (deletar == 204)
                return StatusCode(204);
            else
                return Json("algo deu errado");
        }

        [HttpGet]
        [Route("reports/employees/salary")]
        public IActionResult getSalaryRange()
        {
            return Json(EmployeeMethods.SalaryRange());
        }

        [HttpGet]
        [Route("reports/employees/age")]
        public IActionResult getAgeRange()
        {
            return Json(EmployeeMethods.AgeRange());
        }
    }
}
