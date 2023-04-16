using Demo.BLL.Service;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebTrabajador.Models;
using WebTrabajador.Models.ViewModels;

namespace WebTrabajador.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITrabajadoresService _service;

        public HomeController(ITrabajadoresService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            IQueryable<Trabajadores> query = await _service.ObtenerTodos();
            List<VMTrabajadores> lista = query.Select(x => new VMTrabajadores()
                                            {
                Id=x.Id,
                Nombres=x.Nombres,
                TipoDocumento=x.TipoDocumento,
                NumeroDocumento=x.NumeroDocumento,
                Sexo=x.Sexo,
                Departamento= new VMSelect(){Id=(int)x.IdDepartamento,Descripcion= x.IdDepartamentoNavigation.NombreDepartamento ?? "" },
                Provincia= new VMSelect() { Id = (int)x.IdProvincia, Descripcion = x.IdProvinciaNavigation.NombreProvincia ?? "" },
                Distrito= new VMSelect() { Id = (int)x.IdDistrito, Descripcion = x.IdDistritoNavigation.NombreDistrito ?? "" }

            }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        public async Task<IActionResult> Lista2(string filtro)
        {
            IQueryable<Trabajadores> query = await _service.ObtenerTodos();
            List<VMTrabajadores> lista = query.Where(x=>x.Sexo==filtro).Select(x => new VMTrabajadores()
            {
                Id = x.Id,
                Nombres = x.Nombres,
                TipoDocumento = x.TipoDocumento,
                NumeroDocumento = x.NumeroDocumento,
                Sexo = x.Sexo,
                Departamento = new VMSelect() { Id = (int)x.IdDepartamento, Descripcion = x.IdDepartamentoNavigation.NombreDepartamento ?? "" },
                Provincia = new VMSelect() { Id = (int)x.IdProvincia, Descripcion = x.IdProvinciaNavigation.NombreProvincia ?? "" },
                Distrito = new VMSelect() { Id = (int)x.IdDistrito, Descripcion = x.IdDistritoNavigation.NombreDistrito ?? "" }

            }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] VMTrabajadores modelo)
        {
            try
            {
                Trabajadores NuevoModelo = new Trabajadores()
                {
                    TipoDocumento = modelo.TipoDocumento,
                    NumeroDocumento = modelo.NumeroDocumento,
                    Nombres = modelo.Nombres,
                    Sexo = modelo.Sexo,
                    IdDepartamento = modelo.Departamento.Id,
                    IdProvincia = modelo.Provincia.Id,
                    IdDistrito = modelo.Distrito.Id
                };

                bool respuesta = await _service.Insertar(NuevoModelo);

                return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = false });
            }
            

        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] VMTrabajadores modelo)
        {
            try
            {
                Trabajadores NuevoModelo = new Trabajadores()
                {
                    Id = modelo.Id,
                    TipoDocumento = modelo.TipoDocumento,
                    NumeroDocumento = modelo.NumeroDocumento,
                    Nombres = modelo.Nombres,
                    Sexo = modelo.Sexo,
                    IdDepartamento = modelo.Departamento.Id,
                    IdProvincia = modelo.Provincia.Id,
                    IdDistrito = modelo.Distrito.Id
                };

                bool respuesta = await _service.Actualizar(NuevoModelo);

                return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = false });
            }

            

        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {

            bool respuesta = await _service.Eliminar(id);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }


        [HttpGet]
        public async Task<IActionResult> Departamentos()
        {
            IQueryable<Departamento> query = await _service.Departamento();
            var lista = query.Select(x => new VMSelect()
            {
                Id = x.Id,
                Descripcion = x.NombreDepartamento
            }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        public async Task<IActionResult> Provincias(int id)
        {
            IQueryable<Provincia> query = await _service.Provincia();
            var lista = query.Where(x=>x.IdDepartamento==id).Select(x => new VMSelect()
            {
                Id = x.Id,
                Descripcion = x.NombreProvincia
            }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        public async Task<IActionResult> Distritos(int id)
        {
            IQueryable<Distrito> query = await _service.Distrito();
            var lista = query.Where(x => x.IdProvincia == id).Select(x => new VMSelect()
            {
                Id = x.Id,
                Descripcion = x.NombreDistrito
            }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}