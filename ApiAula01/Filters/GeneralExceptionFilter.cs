﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;

namespace ApiAula01.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails()
            {
                Status = 500,
                Title = "Erro inesperado, tente Novamente",
                Detail = "Ocorreu um erro inesperado na solitação",
                Type = context.Exception.GetType().Name
            };


            switch (context.Exception)
            {
                case SqlException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    problem.Status = 503;
                    problem.Title = "Erro inesperado ao se comunicar com o banco de dados";
                    problem.Detail = "Falha não reconhecida ao se conectar com o Banco de Dados";
                    context.Result = new ObjectResult(problem);
                    break;
                case NullReferenceException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status417ExpectationFailed;
                    problem.Status = 417;
                    problem.Title = "Erro inesperado no sistema";
                    problem.Detail = "Falha não reconhecida no sistema";
                    context.Result = new ObjectResult(problem);
                    break;
                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(problem);
                    break;
            }

            Console.WriteLine($"Tipo da exceção {context.Exception.GetType().Name}, mensagem {context.Exception.Message}, stack trace {context.Exception.StackTrace}");

        }
    }
}
