using ApiAula01.Filters;
using Cliente.Core.Interface;
using Cliente.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiAula01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")] // -> limita a entrada
    [Produces("application/json")] // -> limita a saida
    [TypeFilter(typeof(LogResourceFilterTimer))]
    public class ValuesController : ControllerBase
    {
        public IClienteService _clienteService;

        public ValuesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        #region GET'S
        [HttpGet("/cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Clientes>> GetClientesController()
        {
            return Ok(_clienteService.GetClientesService());
        }


        [HttpGet("/cliente/nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Clientes> GetClienteNome(string nome)
        {
            return Ok(_clienteService.GetClienteNome(nome));
        }

        [HttpGet("/cliente/cpf/{cpf}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Clientes> GetClienteCpf(string cpf)
        {
            return Ok(_clienteService.GetClienteCpf(cpf));
        }
        #endregion

        #region POST
        [HttpPost("/cliente/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(LogActionFilterCpfNaBase))]
        public ActionResult<Clientes> PostCliente(string cpf, Clientes cliente)
        {
            if (!_clienteService.PostCliente(cpf, cliente))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(PostCliente), cliente);
        }
        #endregion

        #region PUT
        [HttpPut("/cliente/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(LogActionFilterCpfExiste))]
        public IActionResult PutCliente(string cpf, Clientes cliente)
        {
            if (!_clienteService.PutCliente(cpf, cliente))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
        #endregion

        #region DELETE
        [HttpDelete("/cliente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ServiceFilter(typeof(LogActionFilterCpfExiste))]
        public ActionResult<List<Clientes>> DeleteCliente(string cpf)
        {
            if (!_clienteService.DeleteCliente(cpf))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete("/cliente/Id/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ServiceFilter(typeof(LogActionFilterCpfExiste))]
        public ActionResult<List<Clientes>> DeleteClienteId(long id)
        {
            if (!_clienteService.DeleteClienteId(id))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
        #endregion
    }
}
