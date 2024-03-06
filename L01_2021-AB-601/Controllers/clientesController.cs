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

        private readonly clientesContext _clientesContexto;

        public clientesController(clientesContext clientesContexto)
        {
            _clientesContexto = clientesContexto;

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
        [Route("Actualizar/{id}")]

        public IActionResult ActualizarCliente(int id, [FromBody] clientes clienteModificar)
        {

            clientes? cliente = (from e in _clientesContexto.clientes
                                   where e.clienteId == id
                                   select e).FirstOrDefault();

            if (cliente == null)
            { return NotFound(); }

            cliente.nombreCliente = clienteModificar.nombreCliente;
            cliente.direccion = clienteModificar.direccion;



            _clientesContexto.Entry(cliente).State = EntityState.Modified;
            _clientesContexto.SaveChanges();

            return Ok(clienteModificar);

        }

        //Eliminar cliente
        [HttpPut]
        [Route("Eliminar/{id}")]
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
