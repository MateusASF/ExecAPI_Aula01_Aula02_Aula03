using Cliente.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiAula01.Filters
{
    public class LogActionFilterCpfNaBase : ActionFilterAttribute
    {
        IClienteService _clienteService;

        public LogActionFilterCpfNaBase(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string cpfCliente = (string)context.ActionArguments["cpf"];

            var cpfBase = _clienteService.GetClientesService().Find(x => x.Cpf == cpfCliente);

            if (cpfBase != null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }


        }
    }
}
