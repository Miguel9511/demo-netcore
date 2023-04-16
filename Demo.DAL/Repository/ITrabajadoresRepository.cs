using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repository
{
    public interface ITrabajadoresRepository : IGenericRepository<Trabajadores>
    {
        Task<IQueryable<Departamento>> Departamento();
        Task<IQueryable<Provincia>> Provincia();
        Task<IQueryable<Distrito>> Distrito();
    }
}
