using ApiAula01.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAula01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")] // -> limita a entrada
    [Produces("application/json")] // -> limita a saida
    public class ValuesController : ControllerBase
    {
        public ClienteRepository _clienteRepository;

        public ValuesController(IConfiguration configuration)
        {
            _clienteRepository = new ClienteRepository(configuration);
        }

        #region GET'S
        [HttpGet("/cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Cliente>> GetClientes()
        {
            return Ok(_clienteRepository.GetClientes());
        }


        [HttpGet("/cliente/nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Cliente> GetClienteNome(string nome)
        {
            return Ok(_clienteRepository.GetClienteNome(nome));
        }

        [HttpGet("/cliente/cpf/{cpf}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Cliente> GetClienteCpf(string cpf)
        {
            return Ok(_clienteRepository.GetClienteCpf(cpf));
        }
        #endregion

        #region POST
        [HttpPost("/cliente/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Cliente> PostCliente(Cliente cliente)
        {
            if (!_clienteRepository.PostCliente(cliente))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(PostCliente), cliente);
        }
        #endregion

        #region PUT
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PutCliente(string cpf, Cliente cliente)
        {
            if (!_clienteRepository.PutCliente(cpf, cliente))
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region DELETE
        [HttpDelete("/cliente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Cliente>> DeleteCliente(string cpf)
        {
            if (!_clienteRepository.DeleteCliente(cpf))
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion
    }
}
