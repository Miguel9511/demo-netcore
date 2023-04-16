using Demo.DAL.Repository;
using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Service
{
    public class TrabajadoresService : ITrabajadoresService
    {
        private readonly ITrabajadoresRepository _model;
        public TrabajadoresService(ITrabajadoresRepository model)
        {
            _model = model;
        }
        public async Task<bool> Actualizar(Trabajadores modelo)
        {
            return await _model.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _model.Eliminar(id);
        }

        public async Task<bool> Insertar(Trabajadores modelo)
        {
            return await _model.Insertar(modelo);
        }

        public async Task<Trabajadores> Obtener(int id)
        {
            return await _model.Obtener(id);
        }

        public async Task<IQueryable<Trabajadores>> ObtenerTodos()
        {
            return await _model.ObtenerTodos();
        }

        public async Task<IQueryable<Departamento>> Departamento()
        {
            return await _model.Departamento();
        }
        public async Task<IQueryable<Provincia>> Provincia()
        {
            return await _model.Provincia();
        }
        public async Task<IQueryable<Distrito>> Distrito()
        {
            return await _model.Distrito();
        }
    }
}
