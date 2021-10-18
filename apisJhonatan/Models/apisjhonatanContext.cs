using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apisJhonatan.Models
{
    // ligando o model com a tabela do banco
    public class apisjhonatanContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public apisjhonatanContext(DbContextOptions<apisjhonatanContext> options) :
            base(options)
        {
        }
    }
}
