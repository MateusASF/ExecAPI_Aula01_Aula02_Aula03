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

        //public static async Task<string> FormatRequestBody(HttpRequest request)
        //{
            //request.EnableRewind();

         //   var buffer = new byte[Convert.ToInt32(request.ContentLength)];
       // }

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            //StringBuilder sb = new StringBuilder();
            //foreach (var arg in context.ActionArguments)
            //{
            //    sb.Append(arg.Key.ToString() + ":" + System.Text.Json.JsonSerializer.Serialize(arg.Value) + "\n");
            //}
            //var conteudo = sb.ToString();

            var teste = context.ActionArguments["cliente"] as Clientes;

            if (!HttpMethods.IsPost(context.HttpContext.Request.Method) &&
                _clienteService.GetClienteCpf(teste.Cpf) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            else if (HttpMethods.IsPost(context.HttpContext.Request.Method) &&
                _clienteService.GetClienteCpf(teste.Cpf) != null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status409Conflict);
            }

        }
    }
}

