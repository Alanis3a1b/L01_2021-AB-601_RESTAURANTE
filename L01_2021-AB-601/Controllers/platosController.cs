using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021_AB_601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2021_AB_601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restauranteContext _platosContexto;

        public platosController(restauranteContext restauranteContexto)
        {
            _platosContexto = restauranteContexto;


        }

        //Método para leer todos los platos
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            //No se porque da error el listado de pedidos :(
            List<platos> listadoPlatos = (from e in _platosContexto.platos
                                            select e).ToList();

            if (listadoPlatos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoPlatos);
        }

        //Buscar por nombre del plato
        [HttpGet]
        [Route("Find/{nombrePlato}")]

        public IActionResult Get(string nombrePlato)
        {
            platos? plato = (from e in _platosContexto.platos
                               where e.nombrePlato.Contains(nombrePlato)
                               select e).FirstOrDefault();
            if (plato == null)
            {
                return NotFound();
            }

            return Ok(plato);
        }

        //Método para crear o insertar platos
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPlato([FromBody] platos plato)
        {
            try
            {
                _platosContexto.platos.Add(plato);
                _platosContexto.SaveChanges();
                return Ok(plato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para modificar los platos de la tabla
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarPlato(int id, [FromBody] platos platoModificar)
        {
            //Sigo sin saber por qué me da error esta parte, será el álias?
            //Ya lo probé y sigo sin saber que puede ser :(
            platos? platoActual = (from e in _platosContexto.platos
                                     where e.platoId == id
                                     select e).FirstOrDefault();

            if (platoActual == null)
            { return NotFound(); }

            platoActual.nombrePlato = platoModificar.nombrePlato;
            platoActual.precio = platoModificar.precio;


            _platosContexto.Entry(platoActual).State = EntityState.Modified;
            _platosContexto.SaveChanges();

            return Ok(platoModificar);

        }

        //Eliminar plato
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult EliminarPlato(int id)
        {
            platos? plato = (from e in _platosContexto.platos
                                where e.platoId == id
                                select e).FirstOrDefault();

            if (plato == null)
            { return NotFound(); }

            _platosContexto.platos.Attach(plato);
            _platosContexto.platos.Remove(plato);
            _platosContexto.SaveChanges();

            return Ok(plato);

        }
    }
}
