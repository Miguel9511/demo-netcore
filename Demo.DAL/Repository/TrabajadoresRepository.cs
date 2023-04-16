using Demo.DAL.DataContext;
using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repository
{
    public class TrabajadoresRepository : ITrabajadoresRepository
    {
        private readonly TrabajadoresPruebaContext _dbContext;

        public TrabajadoresRepository(TrabajadoresPruebaContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> Actualizar(Trabajadores modelo)
        {
            _dbContext.Trabajadores.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Trabajadores modelo = _dbContext.Trabajadores.First(x => x.Id == id);
            _dbContext.Trabajadores.Remove(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Trabajadores modelo)
        {
            _dbContext.Trabajadores.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Trabajadores> Obtener(int id)
        {
            return await _dbContext.Trabajadores.FindAsync(id);
        }

        public async Task<IQueryable<Trabajadores>> ObtenerTodos()
        {
            IQueryable<Trabajadores> query = _dbContext.Trabajadores;
            return query;
        }

        public async Task<IQueryable<Departamento>> Departamento()
        {
            IQueryable<Departamento> query = _dbContext.Departamento;
            return query;
        }

        public async Task<IQueryable<Provincia>> Provincia()
        {
            IQueryable<Provincia> query = _dbContext.Provincia;
            return query;
        }

        public async Task<IQueryable<Distrito>> Distrito()
        {
            IQueryable<Distrito> query = _dbContext.Distrito;
            return query;
        }
    }
}
