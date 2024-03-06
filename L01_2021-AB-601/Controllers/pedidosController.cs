using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021_AB_601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2021_AB_601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly pedidosContext _pedidosContexto;

        public pedidosController(pedidosContext pedidosContexto) 
        {
            _pedidosContexto = pedidosContexto;

        }

        //Método para leer todos los pedidosS
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            //No se porque da error el listado de pedidos :(
            List<pedidos> listadoPedidos = (from e in _pedidosContexto.pedidos
                                           select e).ToList();

            if (listadoPedidos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoPedidos);
        }


        //Buscar por clienteId
        [HttpGet]
        [Route("Find/{cliente}")]

        public IActionResult GetCliente(string cliente)
        {
            pedidos? pedido = (from e in _pedidosContexto.pedidos
                               where e.clienteId.Equals(cliente)
                               select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        //Buscar por motoristaId
        [HttpGet]
        [Route("Find/{motorista}")]

        public IActionResult GetMotorista(string motorista)
        {
            pedidos? pedido = (from e in _pedidosContexto.pedidos
                               where e.motoristaId.Equals(motorista)
                               select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        //Método para crear o insertar pedidos
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPedido([FromBody] pedidos pedido)
        {
            try
            {
                _pedidosContexto.pedidos.Add(pedido);
                _pedidosContexto.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para modificar los pedidos de la tabla
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarPedido(int id, [FromBody] pedidos pedidoModificar)
        {
            //Sigo sin saber por qué me da error esta parte, será el álias?
            //Ya lo probé y sigo sin saber que puede ser :(
            pedidos? pedidoActual = (from e in _pedidosContexto.pedidos
                                     where e.pedidoId == id
                                     select e).FirstOrDefault();

            if (pedidoActual == null)
            { return NotFound(); }

            pedidoActual.motoristaId = pedidoModificar.motoristaId;
            pedidoActual.clienteId = pedidoModificar.clienteId;
            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;


            _pedidosContexto.Entry(pedidoActual).State = EntityState.Modified;
            _pedidosContexto.SaveChanges();

            return Ok(pedidoModificar);

        }

        //Eliminar pedido
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult EliminarPedido(int id)
        {
            pedidos? pedido = (from e in _pedidosContexto.pedidos
                                     where e.pedidoId == id
                                     select e).FirstOrDefault();

            if (pedido == null)
            { return NotFound(); }

            _pedidosContexto.pedidos.Attach(pedido);
            _pedidosContexto.pedidos.Remove(pedido);
            _pedidosContexto.SaveChanges();

            return Ok(pedido);


        }






    }
}
