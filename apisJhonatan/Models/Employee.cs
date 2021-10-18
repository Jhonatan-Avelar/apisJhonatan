using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apisJhonatan.Models
{
    // model de interacao com a tabela "employee" do banco
    [Table("employee")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idemployee { get; set; }

        public string name { get; set; }
        public string email { get; set; }
        public string department { get; set; }
        public int salary { get; set; }
        public DateTime birthdate { get; set; }
    }
}
