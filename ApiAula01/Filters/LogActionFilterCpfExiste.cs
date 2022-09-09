using Cliente.Core.Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Cliente.Core.Model;

namespace ApiAula01.Filters
{
    public class LogActionFilterCpfExiste : ActionFilterAttribute
    {
        IClienteService _clienteService;

        public LogActionFilterCpfExiste(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpMethods.IsDelete(context.HttpContext.Request.Method) || HttpMethods.IsGet(context.HttpContext.Request.Method))
            {
                return;
            }

            var teste = context.ActionArguments["cliente"] as Clientes;

            if (HttpMethods.IsPut(context.HttpContext.Request.Method) &&
                _clienteService.GetClienteCpf(teste.Cpf) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            else if (HttpMethods.IsPost(context.HttpContext.Request.Method) &&
                _clienteService.GetClienteCpf(teste.Cpf) != null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status409Conflict);
            }

        }
    }
}

