using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apisJhonatan.Models;

namespace apisJhonatan.Controllers
{
    // classe responsavel por conter os metodos com as regras de negocio da aplicacao
    public class EmployeeMethods
    {
        private readonly apisjhonatanContext _contextapisjhonatan;

        public EmployeeMethods(apisjhonatanContext context)
        {
            _contextapisjhonatan = context;
        }

        public List<Employee> Get()
        {
            return _contextapisjhonatan.Employee.Where(c => c.idemployee > 0).ToList();
        }

        public Employee GetById(int id)
        {
            return _contextapisjhonatan.Employee.Where(c => c.idemployee == id).Single();
        }

        public int Create(dynamic dados)
        {
            var novoEmployee = new Employee()
            {
                name = dados.name,
                email = dados.email,
                department = dados.department,
                salary = dados.salary,
                birthdate = dados.birthdate
            };

            _contextapisjhonatan.Add<Employee>(novoEmployee);
            _contextapisjhonatan.SaveChanges();

            return (201);
        }

        public int Update(dynamic dados)
        {
            int id = dados.id;
            Employee updateModel = _contextapisjhonatan.Employee.Where(c => c.idemployee == id).Single();

            updateModel.name = dados.name;
            updateModel.email = dados.email;
            updateModel.department = dados.department;
            updateModel.salary = dados.salary;
            updateModel.birthdate = dados.birthdate;

            _contextapisjhonatan.SaveChanges();

           return (204);
        }

        public int Delete(int id)
        {
            Employee deleteModel = _contextapisjhonatan.Employee.Where(c => c.idemployee == id).Single();

            _contextapisjhonatan.Remove(deleteModel);
            _contextapisjhonatan.SaveChanges();

            return (204);
        }

        public dynamic SalaryRange()
        {
            var lowest = _contextapisjhonatan.Employee.OrderBy(c => c.salary).First();
            var highest = _contextapisjhonatan.Employee.OrderByDescending(c => c.salary).First();

            return new
            {
                lowest = lowest,
                highest = highest
            };
        }

        public dynamic AgeRange()
        {
            var lowest = _contextapisjhonatan.Employee.OrderBy(c => c.birthdate).First();
            var highest = _contextapisjhonatan.Employee.OrderByDescending(c => c.birthdate).First();

            return new
            {
                lowest = lowest,
                highest = highest
            };
        }
    }
}
