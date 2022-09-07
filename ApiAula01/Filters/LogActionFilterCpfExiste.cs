using Cliente.Core.Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ApiAula01.Filters
{
    public class LogActionFilterCpfExiste : ActionFilterAttribute
    {
        IClienteService _clienteService;

        public LogActionFilterCpfExiste(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string cpfCliente = (string)context.ActionArguments["cpf"];

            if (_clienteService.GetClienteCpf(cpfCliente) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}

