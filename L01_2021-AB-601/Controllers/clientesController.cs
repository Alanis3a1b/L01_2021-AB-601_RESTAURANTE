using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021_AB_601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2021_AB_601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clientesController : ControllerBase
    {

        private readonly restauranteContext _clientesContexto;

        public clientesController(restauranteContext restauranteContexto)
        {
            _clientesContexto = restauranteContexto;

        }

        //Método para leer todos los clientes
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            //No se porque da error el listado de pedidos :(
            List<clientes> listadoClientes = (from e in _clientesContexto.clientes
                                          select e).ToList();

            if (listadoClientes.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoClientes);
        }

        //Buscar por la direccion
        [HttpGet]
        [Route("Find/{direccion}")]

        public IActionResult Get(string direccion)
        {
            clientes? cliente = (from e in _clientesContexto.clientes
                             where e.direccion.Contains(direccion)
                             select e).FirstOrDefault();

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        //Método para crear o insertar clientes
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarCliente([FromBody] clientes cliente)
        {
            try
            {
                _clientesContexto.clientes.Add(cliente);
                _clientesContexto.SaveChanges();
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para modificar los clientes de la tabla
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarCliente(int id, [FromBody] clientes clienteModificar)
        {
            //Sigo sin saber por qué me da error esta parte, será el álias?
            //Ya lo probé y sigo sin saber que puede ser :(
            clientes? clienteActual = (from e in _clientesContexto.clientes
                                   where e.clienteId == id
                                   select e).FirstOrDefault();

            if (clienteActual == null)
            { return NotFound(); }

            clienteActual.nombreCliente = clienteModificar.nombreCliente;
            clienteActual.direccion = clienteModificar.direccion;



            _clientesContexto.Entry(clienteActual).State = EntityState.Modified;
            _clientesContexto.SaveChanges();

            return Ok(clienteModificar);

        }

        //Eliminar cliente
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult EliminarCliente(int id)
        {
            clientes? cliente = (from e in _clientesContexto.clientes
                                       where e.clienteId == id
                                       select e).FirstOrDefault();

            if (cliente == null)
            { return NotFound(); }

            _clientesContexto.clientes.Attach(cliente);
            _clientesContexto.clientes.Remove(cliente);
            _clientesContexto.SaveChanges();

            return Ok(cliente);

        }
    }
}
