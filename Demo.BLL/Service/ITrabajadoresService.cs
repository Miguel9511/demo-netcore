using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Service
{
    public interface ITrabajadoresService
    {
        Task<bool> Insertar(Trabajadores modelo);
        Task<bool> Actualizar(Trabajadores modelo);
        Task<bool> Eliminar(int id);
        Task<Trabajadores> Obtener(int id);
        Task<IQueryable<Trabajadores>> ObtenerTodos();
        Task<IQueryable<Departamento>> Departamento();
        Task<IQueryable<Provincia>> Provincia();
        Task<IQueryable<Distrito>> Distrito();
    }
}
